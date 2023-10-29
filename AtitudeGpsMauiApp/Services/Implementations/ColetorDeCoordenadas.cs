using AtitudeGpsMauiApp.Core.Models;
using AtitudeGpsMauiApp.Domain.Constantes;
using AtitudeGpsMauiApp.Domain.Enumeradores;
using AtitudeGpsMauiApp.Domain.Extensions;
using AtitudeGpsMauiApp.Infrastructure.Interfaces;
using AtitudeGpsMauiApp.Services.Interfaces;
using AtitudeGpsMauiApp.Support;
using System.Text.Json;

namespace AtitudeGpsMauiApp.Services.Implementations
{
    public class ColetorDeCoordenadas : IColetorDeCoordenadas
    {
        #region Initialize
        private bool _online;
        private int _nivelDaAmostra;
        private bool _coletorInicializado;
        private Snapshot[] _snapshots;
        private readonly IMessageBoxService _msgBox;
        private readonly ILeitorDeCoordenadas _leitorDeCoordenadas;
        private readonly IOperadorDeDiretorios _operadorDeDiretorios;
        private readonly ConversorDeCoordenadas _conversorDeCoordenadas;

        public ColetorDeCoordenadas()
        {
            _online = false;
            _nivelDaAmostra = 1;
            _coletorInicializado = false;
            _snapshots = new Snapshot[5];
            _msgBox = App.Services.GetService<IMessageBoxService>();
            _leitorDeCoordenadas = App.Services.GetService<ILeitorDeCoordenadas>();
            _operadorDeDiretorios = App.Services.GetService<IOperadorDeDiretorios>();
            _conversorDeCoordenadas = new();
        }
        #endregion

        public async Task<bool> InicializaColetorAsync()
        {
            return await ExecutaEmTryCatchAsync(new Func<Task>(async () =>
            {
                Location loc = await _leitorDeCoordenadas.TentaObterLocalizacaoUmaUnicaVezAsync();
                loc = _conversorDeCoordenadas.AplicaLimiteDeDigitosComArredondamento(loc, PropriedadesDaAplicacao.FatorDeCasasDecimais);

                if (_snapshots[0] is null)
                {
                    for (int i = 0; i < _snapshots.Length; i++)
                        _snapshots[i] = new Snapshot()
                        {
                            Latitude = loc.Latitude,
                            Longitude = loc.Longitude,
                            Intervalo = PropriedadesDaAplicacao.IntervaloMinimo
                        };
                }

                _online = true;
                _coletorInicializado = true;
            }));
        }

        public async Task<bool> ColetaGpsAsync()
        {
            return await ExecutaEmTryCatchAsync(new Func<Task>(async () =>
            {
                DateTime agora = DateTime.Now;

                if (!_coletorInicializado) throw new Exception("Array de snapshots vazio. Execute o método InicializaColetor.");
                if (!_online) return;

                Location loc = await _leitorDeCoordenadas.TentaObterLocalizacaoUmaUnicaVezAsync();
                loc = _conversorDeCoordenadas.AplicaLimiteDeDigitosComArredondamento(loc, PropriedadesDaAplicacao.FatorDeCasasDecimais);

                var novoSnapshot = new Snapshot
                {
                    TipoDeLog = TipoDeLogEnum.Monitor,
                    Latitude = loc.Latitude,
                    Longitude = loc.Longitude,
                    MomentumInicial = agora.Ticks,
                    MomentumFinal = agora.Ticks,
                    Intervalo = PropriedadesDaAplicacao.IntervaloMinimo
                };

                novoSnapshot.CalculaDistanciaEVelocidadeMedia(_snapshots[4]);

                _snapshots.ShiftUpSnapshotArrayItems();

                if (novoSnapshot.DistanciaEmMetros < PropriedadesDaAplicacao.DistanciaMinimaValida)
                {
                    novoSnapshot.Latitude = _snapshots[3].Latitude;
                    novoSnapshot.Longitude = _snapshots[3].Longitude;
                    novoSnapshot.RedefineValoresDinamicos();
                }

                _snapshots[4] = novoSnapshot;

                // Eu poderia tirar a comparação do item 4 com o item 3 dos if's para evitar e repetição de código,
                // porém esta forma mais verbosa melhora o entendimento aumentando a aderência aos princípios do
                // Clean Code.
                if (_nivelDaAmostra == 1)
                {
                    LogaGps(_snapshots[4]);
                }
                else if (_nivelDaAmostra == 2)
                {
                    LogaGps(_snapshots[4]);

                    _snapshots[4].CalculaDistanciaEVelocidadeMedia(_snapshots[2]);
                    _snapshots[4].Intervalo = PropriedadesDaAplicacao.IntervaloMinimo * 2;
                    LogaGps(_snapshots[4]);
                }
                else if (_nivelDaAmostra == 3)
                {
                    LogaGps(_snapshots[4]);

                    _snapshots[4].CalculaDistanciaEVelocidadeMedia(_snapshots[1]);
                    _snapshots[4].Intervalo = PropriedadesDaAplicacao.IntervaloMinimo * 3;
                    LogaGps(_snapshots[4]);
                }
                else if (_nivelDaAmostra == 4)
                {
                    LogaGps(_snapshots[4]);

                    _snapshots[4].CalculaDistanciaEVelocidadeMedia(_snapshots[2]);
                    _snapshots[4].Intervalo = PropriedadesDaAplicacao.IntervaloMinimo * 2;
                    LogaGps(_snapshots[4]);

                    _snapshots[4].CalculaDistanciaEVelocidadeMedia(_snapshots[0]);
                    _snapshots[4].Intervalo = PropriedadesDaAplicacao.IntervaloMinimo * 4;
                    LogaGps(_snapshots[4]);
                }

                if (_nivelDaAmostra == 4)
                    _nivelDaAmostra = 1;
                else
                    _nivelDaAmostra++;

                MessagingCenter.Send(
                    App.Current,
                    "ticks",
                    string.Format("{0}m {1}Km/h", _snapshots[4].DistanciaEmMetros, _snapshots[4].KilometrosPorHora));

                GC.Collect();
            }));
        }

        public void FinalizaColetor()
        {
            _online = false;
        }

        public void LimpaCoordenadas()
        {
            for (int i = 0; i < _snapshots.Length; i++)
            {
                _snapshots[i] = null;
            }
        }

        private async Task<bool> ExecutaEmTryCatchAsync(Func<Task> functionAsync)
        {
            if (functionAsync == null) return false;
            string msgDeErro = string.Empty;

            try
            {
                await functionAsync.Invoke();
                return true;
            }
            catch (FeatureNotSupportedException)
            {
                // Handle not supported on device exception
                msgDeErro = "Este dispositivo não possui a funcionalidade de GPS.";
            }
            catch (FeatureNotEnabledException)
            {
                // Handle not enabled on device exception
                msgDeErro = "Ative o serviço de GPS do dispositivo.";
            }
            catch (PermissionException)
            {
                // Handle permission exception
                msgDeErro = "Conceda permissão para o aplicativo acessar as informações de GPS do dispositivo. Sua privacidade sempre será respeitada.";
            }
            catch (Exception ex)
            {
                // Unable to get location
                msgDeErro = ex.Message;
            }

            MainThread.BeginInvokeOnMainThread(async () =>
            {
                await _msgBox.ShowAsync(msgDeErro);
            });

            return false;
        }

        private void LogaGps(Snapshot snapshot)
        {
            string locAtualJsonObject = JsonSerializer.Serialize(snapshot);
            System.Diagnostics.Debug.WriteLine(snapshot);
            _operadorDeDiretorios.AdicionaLinhaAoLogDoMonitor(snapshot);
        }
    }
}
