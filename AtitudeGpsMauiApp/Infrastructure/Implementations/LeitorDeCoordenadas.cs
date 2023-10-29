using AtitudeGpsMauiApp.Domain.Constantes;
using AtitudeGpsMauiApp.Infrastructure.Interfaces;

namespace AtitudeGpsMauiApp.Infrastructure.Implementations
{
    public class LeitorDeCoordenadas : ILeitorDeCoordenadas
    {
        private readonly GeolocationRequest _locationRequest =
            new GeolocationRequest(PropriedadesDaAplicacao.PrecisaoDeGeolocalizacao, TimeSpan.FromSeconds(PropriedadesDaAplicacao.GeolocationRequestTimeout));

        public void DefineParametrosDeGeolocationRequest(GeolocationAccuracy accuracy, int timeout)
        {
            _locationRequest.DesiredAccuracy = accuracy;
            _locationRequest.Timeout = TimeSpan.FromSeconds(timeout);
        }

        public async Task<Location> TentaObterLocalizacaoUmaUnicaVezAsync()
        {
            try
            {
                var loc = await Geolocation.Default.GetLocationAsync(_locationRequest);

                if (loc == null)
                    throw new NullReferenceException("Não foi possível obter coordenadas GPS. Tente novamente am alguns instantes.");

                return loc;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Location TentaObterLocalizacaoComContagemRegressiva(int segundos)
        {
            try
            {
                Location loc = null;
                bool tempoDisponivel = true;
                var cancelationToken = new CancellationTokenSource();

                Action contagemRegressivaasync = async () =>
                {
                    try
                    {
                        await Task.Delay(segundos, cancelationToken.Token);
                        tempoDisponivel = false;

                    }
                    catch (Exception)
                    {
                        return;
                    }
                };

                // Tenta a primeira vez
                loc = Geolocation.GetLocationAsync(_locationRequest).GetAwaiter().GetResult();

                if (loc == null)
                {
                    // Ativa a contagem regressiva sem await (fire-and-forget)
                    new TaskFactory().StartNew(contagemRegressivaasync);
                }

                // Tenta até conseguir ou até o tempo expirar
                while (loc == null && tempoDisponivel)
                {
                    loc = Geolocation.GetLocationAsync(_locationRequest).GetAwaiter().GetResult();
                }

                // Independente de ter terminado o tempo, cancela o delay de thread
                cancelationToken.Cancel();

                if (loc == null)
                {
                    throw new NullReferenceException("Tempo esgotado na tentativa de obter coordenadas GPS.");
                }

                return loc;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Location TentaObterLocalizacaoPorQuantidadeLimitadaDeTentativas(int quantidadeDeTentativas)
        {
            try
            {
                Location loc = null;

                for (int i = 0; i < quantidadeDeTentativas; i++)
                {
                    loc = Geolocation.GetLocationAsync(_locationRequest).GetAwaiter().GetResult();
                    if (loc != null) return loc;
                    Thread.Sleep(1000);
                }

                throw new NullReferenceException("Esgotado o número de tentativas de se obter coordenadas GPS.");
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
