namespace AtitudeGpsMauiApp.Infrastructure.Interfaces
{
    public interface ILeitorDeCoordenadas
    {
        void DefineParametrosDeGeolocationRequest(GeolocationAccuracy accuracy, int timeout);
        Task<Location> TentaObterLocalizacaoUmaUnicaVezAsync();
        Task<Location> TentaObterLocalizacaoPorMediaAritmeticaAsync();
        Task<Location> TentaObterLocalizacaoPorMediaAritmeticaAsync(Location[] locations);
        Location TentaObterLocalizacaoComContagemRegressiva(int segundos);
        Location TentaObterLocalizacaoPorQuantidadeLimitadaDeTentativas(int quantidadeDeTentativas);
    }
}
