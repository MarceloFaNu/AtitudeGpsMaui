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

                // Independente de ter terminado o tempo, cancela o delay de thread por garantia
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

        public async Task<Location> TentaObterLocalizacaoPorMediaAritmeticaAsync()
        {
            List<double> latitudes = new List<double>();
            List<double> longitudes = new List<double>();
            List<double> speeds = new List<double>();
            List<double> courses = new List<double>();

            try
            {
                for (int i = 0; i < PropriedadesDaAplicacao.MediaAritmeticaPadrao; i++)
                {
                    var loc = await Geolocation.GetLocationAsync(_locationRequest);

                    if (loc == null)
                        throw new NullReferenceException("Não foi possível acessar o serviço de GPS. Se o problema persistir, considere reiniciar o dispositivo.");

                    latitudes.Add(loc.Latitude);
                    longitudes.Add(loc.Longitude);
                    if (loc.Speed != null) speeds.Add(loc.Speed.Value);
                    if (loc.Course != null) courses.Add(loc.Course.Value);
                }

                Location locMediana = new Location
                {
                    Latitude = this.CalculaMediaAritmetica(latitudes),
                    Longitude = this.CalculaMediaAritmetica(longitudes),
                    Speed = this.CalculaMediaAritmetica(speeds),
                    Course = this.CalculaMediaAritmetica(courses)
                };

                return locMediana;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<Location> TentaObterLocalizacaoPorMediaAritmeticaAsync(Location[] locations)
        {
            List<double> latitudes = new List<double>();
            List<double> longitudes = new List<double>();
            List<double> speeds = new List<double>();
            List<double> courses = new List<double>();

            var novaLocation = await this.TentaObterLocalizacaoUmaUnicaVezAsync();

            try
            {
                if (novaLocation == null)
                    throw new NullReferenceException("Não foi possível obter as coordenadas GPS. Caso o problema persista, considere reiniciar o dispositivo.");

                locations[locations.Length - 1] = novaLocation;

                foreach (var loc in locations)
                {
                    latitudes.Add(loc.Latitude);
                    longitudes.Add(loc.Longitude);
                    if (loc.Speed != null) speeds.Add(loc.Speed.Value);
                    if (loc.Course != null) courses.Add(loc.Course.Value);
                }

                Location locMediana = new Location
                {
                    Latitude = this.CalculaMediaAritmetica(latitudes),
                    Longitude = this.CalculaMediaAritmetica(longitudes),
                    Speed = this.CalculaMediaAritmetica(speeds),
                    Course = this.CalculaMediaAritmetica(courses)
                };

                return locMediana;
            }
            catch (Exception)
            {

                throw;
            }
        }

        private double CalculaMediaAritmetica(List<double> medidas)
        {
            if (medidas == null || !medidas.Any())
                return 0.0;

            double somaDasMedidas = 0.0;
            foreach (var medida in medidas)
            {
                somaDasMedidas += medida;
            }

            double media = somaDasMedidas / medidas.Count;

            return Math.Round(media, PropriedadesDaAplicacao.FatorDeCasasDecimais);
        }
    }
}
