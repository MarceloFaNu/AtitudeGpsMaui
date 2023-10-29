namespace AtitudeGpsMauiApp.Services.Interfaces
{
    public interface IColetorDeCoordenadasServiceManager
    {
        void IniciaServico();
        void EncerraServico();
        bool IsServicoEmExecucao();
    }
}
