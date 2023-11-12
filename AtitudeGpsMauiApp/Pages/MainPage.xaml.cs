using AtitudeGpsMauiApp.Core.Models;
using AtitudeGpsMauiApp.Domain.Constantes;
using AtitudeGpsMauiApp.Domain.Extensions;
using AtitudeGpsMauiApp.Infrastructure.Interfaces;
using AtitudeGpsMauiApp.Services.Interfaces;
using AtitudeGpsMauiApp.Domain.Enumeradores;
using AtitudeGpsMauiApp.Support;

namespace AtitudeGpsMauiApp.Pages;

public partial class MainPage : ContentPage
{
    #region Initialize
    private int _ticks;
    private bool _botaoMonitorOcupado;
    private static DateTime _inicioDoMonitoramento;

    private readonly IMessageBoxService _msgBox;
    private readonly ILeitorDeCoordenadas _leitorDeCoordenadas;
    private readonly IOperadorDeDiretorios _operadorDeDiretorios;
    private readonly ISequencerDeEntidades _sequencerDeEntidades;
    private readonly IColetorDeCoordenadasServiceManager _coletorManager;

    public MainPage()
    {
        InitializeComponent();

        try
        {
            _ticks = 0;
            _msgBox = App.Services.GetService<IMessageBoxService>();
            _leitorDeCoordenadas = App.Services.GetService<ILeitorDeCoordenadas>();
            _operadorDeDiretorios = App.Services.GetService<IOperadorDeDiretorios>();
            _sequencerDeEntidades = App.Services.GetService<ISequencerDeEntidades>();
            _coletorManager = App.Services.GetService<IColetorDeCoordenadasServiceManager>();

            MessagingCenter.Subscribe<Application>(App.Current, "ledOn", this.AtivaLed);
            MessagingCenter.Subscribe<Application>(App.Current, "ledOff", this.DesativaLed);
            MessagingCenter.Subscribe<Application, string>(App.Current, "ticks", this.AdicionaUmTick);

            // Enquanto o serviço estiver em execução, todo o aplicativo também estará. A linha abaixo
            // não faz muito sentido já que, ao iniciar o aplicativo, o serviço nunca estará em execução e,
            // se o aplicativo for fechado com o serviço estando ativo, esta linha não será executada.
            if (_coletorManager.IsServicoEmExecucao())
                AtivaLed(null);
        }
        catch (Exception ex)
        {
            DisplayAlert("Erro", ex.Message, "Ok");
            Application.Current.Quit();
        }
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        var locationAlwaysStatusDaPermissao = await Permissions.CheckStatusAsync<Permissions.LocationAlways>();
        var locationWhenInUseStatusDaPermissao = await Permissions.CheckStatusAsync<Permissions.LocationWhenInUse>();
        var StorageWriteStatusDaPermissao = await Permissions.CheckStatusAsync<Permissions.StorageWrite>();

        if ((locationAlwaysStatusDaPermissao == PermissionStatus.Denied && locationWhenInUseStatusDaPermissao == PermissionStatus.Denied) ||
            StorageWriteStatusDaPermissao == PermissionStatus.Denied)
        {
            await _msgBox.ShowAsync("O aplicativo precisa de permissão para utilizar Localização, Armazenamento e Notificações. Tchau, obrigado.");
            Application.Current.Quit();
        }

        IEnumerable<ConnectionProfile> profiles = Connectivity.Current.ConnectionProfiles;
        if (!profiles.Contains(ConnectionProfile.Cellular))
        {
            await _msgBox.ShowAsync("Sinal de telefonia ausente ou muito fraco. Verifique as configurações ou desative o modo Avião.");
            Application.Current.Quit();
        }

        //PermissionStatus locationSemprePermitido = await Permissions.RequestAsync<Permissions.LocationAlways>();
        //PermissionStatus storageSemprePermitido = await Permissions.RequestAsync<Permissions.StorageWrite>();

        //#if ANDROID
        //        if (Permissions.ShouldShowRationale<Permissions.LocationAlways>() || Permissions.ShouldShowRationale<Permissions.StorageWrite>())
        //            Application.Current.Quit();
        //#endif
    }
    #endregion

    private async void btnMonitorar_Clicked(object sender, EventArgs e)
    {
        try
        {
            if (_botaoMonitorOcupado)
            {
                await ExibeMsgDesativeMonitorAsync();
                return;
            }

            _botaoMonitorOcupado = true;

            _operadorDeDiretorios.ApagaZipDeEntrega(_inicioDoMonitoramento.Ticks.ToString());
            _ = _sequencerDeEntidades.ObtemProximoIdParaResumo();
            _ = _sequencerDeEntidades.ObtemProximoIdParaCopiloto();
            this.LogaCopilotoInicial();

            _ = await _leitorDeCoordenadas.TentaObterLocalizacaoUmaUnicaVezAsync();
            _inicioDoMonitoramento = DateTime.Now;

            MessagingCenter.Send(App.Current, "ledOn");

            if (_coletorManager.IsServicoEmExecucao() == false)
            {
                await Task.Factory.StartNew(async () =>
                {
                    // Sem o delay, o processador dá preferência para esta nova thread
                    // e faz a thread da ui esperar. Com o delay na nova thread, o processador é
                    // obrigado a passar a thread da ui na frente
                    await Task.Delay(100);
                    _coletorManager.IniciaServico();
                });
            }
            else
            {
                await _msgBox.ShowAsync("Serviço coletor de Gps já está em execução.");
            }
        }
        catch (FeatureNotEnabledException)
        {
            _botaoMonitorOcupado = false;
            MessagingCenter.Send(App.Current, "ledOff");
            await _msgBox.ShowAsync("Por favor, ative o serviço de localização do dispositivo.");
        }
        catch (Exception ex)
        {
            btnParar_Clicked(null, null);
            _botaoMonitorOcupado = false;
            MessagingCenter.Send(App.Current, "ledOff");
            await _msgBox.ShowAsync(ex.Message);
        }
    }

    private void btnParar_Clicked(object sender, EventArgs e)
    {
        if (!_botaoMonitorOcupado) return;

        _coletorManager.EncerraServico();

        var resumo = CriaResumo();
        _operadorDeDiretorios.AdicionaLinhaAoLogDoResumo(resumo);

        _botaoMonitorOcupado = false;
    }

    private async void btnEnviar_Clicked(object sender, EventArgs e)
    {
        try
        {
            if (_coletorManager.IsServicoEmExecucao())
            {
                await ExibeMsgDesativeMonitorAsync();
                return;
            }

            if ((!_operadorDeDiretorios.ExisteArquivoDeLogDoMonitor()) ||
                (_operadorDeDiretorios.ExisteArquivoDeLogDoMonitor() &&
                _operadorDeDiretorios.ObtemTamanhoDoLogDoMonitorEmBytes() < 1024L))
            {
                await _msgBox.ShowAsync(
                    "Tamanho mínimo de log: 1024 bytes. " +
                    $"Tamanho atual: {_operadorDeDiretorios.ObtemTamanhoDoLogDoMonitorEmBytes()} bytes. Ative o Monitor por mais alguns instantes.");

                return;
            }

            var compartilhamento = new ShareMultipleFilesRequest() { Title = "Atitude GPS Logs" };

            if (_operadorDeDiretorios.ExisteArquivoDeLogDoResumo() &&
                _operadorDeDiretorios.ExisteArquivoDeLogDoMonitor() &&
                _operadorDeDiretorios.ExisteArquivoDeLogDoCopiloto())
            {
                string caminhoDoArquivoDeEntrega = _operadorDeDiretorios.CriaZipParaEntrega(_inicioDoMonitoramento.Ticks.ToString());
                compartilhamento.Files.Add(new ShareFile(caminhoDoArquivoDeEntrega));
            }

            if (compartilhamento.Files.Count > 0)
                await Share.RequestAsync(compartilhamento);
        }
        catch (Exception ex)
        {
            await _msgBox.ShowAsync(ex.Message);
        }
    }

    private async void btnLimparLog_Clicked(object sender, EventArgs e)
    {
        if (_coletorManager.IsServicoEmExecucao())
        {
            await ExibeMsgDesativeMonitorAsync();
            return;
        }

        _operadorDeDiretorios.LimpaLogs(_inicioDoMonitoramento.Ticks.ToString());
        _sequencerDeEntidades.ReiniciaTodasSequencias();
        _ticks = 0;

        this.lblTicks.Text = "Ticks: ";
        this.lblUltPing.Text = "Última Medição: ";
        btnParar.IsEnabled = true;

        if (e != null)
            await _msgBox.ShowAsync("Dados excluídos com sucesso.");
    }

    private void AtivaLedEBotoes(object sender)
    {
        AtivaLed(sender);
        ConfiguraBotoes(servicoAtivo: true);
    }

    private void DesativaLedEBotoes(object sender)
    {
        DesativaLed(sender);
        ConfiguraBotoes(servicoAtivo: false);
    }

    private void AtivaLed(object sender)
    {
        MainThread.BeginInvokeOnMainThread(() =>
        {
            this.lblStatus.Background = Colors.Green;
            this.lblStatus.Text = "ONLINE";
        });
    }

    private void DesativaLed(object sender)
    {
        MainThread.BeginInvokeOnMainThread(() =>
        {
            this.lblStatus.Background = Colors.Red;
            this.lblStatus.Text = "OFFLINE";
        });
    }

    private void ConfiguraBotoes(bool servicoAtivo)
    {
        MainThread.BeginInvokeOnMainThread(() =>
        {
            this.btnMonitorar.IsEnabled = !servicoAtivo;
            this.btnParar.IsEnabled = servicoAtivo;
            this.btnEnviar.IsEnabled = !servicoAtivo;
            this.btnLimparLog.IsEnabled = !servicoAtivo;
        });
    }

    private void AdicionaUmTick(object sender, string s)
    {
        _ticks++;

        MainThread.BeginInvokeOnMainThread(() =>
        {
            this.lblTicks.Text = "Ticks: " + _ticks;
            this.lblUltPing.Text = "Última Medição: " + s;
        });
    }

    private async Task ExibeMsgDesativeMonitorAsync()
    {
        await _msgBox.ShowAsync("Desative o monitor.");
    }

    private Resumo CriaResumo()
    {
        return new Resumo(_inicioDoMonitoramento, DateTime.Now)
        {
            Id = _sequencerDeEntidades.ObtemIdAtualParaResumo(),
            TipoDeLog = TipoDeLogEnum.Resumo,
            FatorDeCasasDecimais = PropriedadesDaAplicacao.FatorDeCasasDecimais,
            IntervaloMinimoEmSegundos = PropriedadesDaAplicacao.IntervaloMinimo,
            DistanciaMinimaConsiderada = PropriedadesDaAplicacao.DistanciaMinimaValida,
            SensibilidadeGps = PropriedadesDaAplicacao.PrecisaoDeGeolocalizacao,
            GeolocationRequestTimeout = PropriedadesDaAplicacao.GeolocationRequestTimeout
        };
    }

    private void LogaCopilotoInicial()
    {
        var copiloto = new Copiloto
        {
            Id = _sequencerDeEntidades.ObtemIdAtualParaCopiloto(),
            ResumoId = _sequencerDeEntidades.ObtemIdAtualParaResumo(),
            TipoDeLog = TipoDeLogEnum.Copiloto,
            Atitude = AtitudeEnum.Nada
        };

        _operadorDeDiretorios.AdicionaLinhaAoLogDoCopiloto(copiloto);
    }
}

