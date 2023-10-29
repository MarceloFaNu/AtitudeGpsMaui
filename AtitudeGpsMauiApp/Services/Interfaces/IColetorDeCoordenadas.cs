namespace AtitudeGpsMauiApp.Services.Interfaces
{
    public interface IColetorDeCoordenadas
    {
        Task<bool> InicializaColetorAsync();
        Task<bool> ColetaGpsAsync();
        void LimpaCoordenadas();
        void FinalizaColetor();
    }
}
