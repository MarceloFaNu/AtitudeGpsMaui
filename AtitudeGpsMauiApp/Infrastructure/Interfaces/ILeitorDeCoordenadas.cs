namespace AtitudeGpsMauiApp.Infrastructure.Interfaces
{
    public interface ILeitorDeCoordenadas
    {
        void DefineParametrosDeGeolocationRequest(GeolocationAccuracy accuracy, int timeout);
        Task<Location> TentaObterLocalizacaoUmaUnicaVezAsync();
        Location TentaObterLocalizacaoComContagemRegressiva(int segundos);
        Location TentaObterLocalizacaoPorQuantidadeLimitadaDeTentativas(int quantidadeDeTentativas);
    }
}
