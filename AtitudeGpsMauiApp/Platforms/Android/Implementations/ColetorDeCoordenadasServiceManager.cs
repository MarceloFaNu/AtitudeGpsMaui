using Android.Content;
using Android.OS;
using AtitudeGpsMauiApp.Services.Interfaces;

namespace AtitudeGpsMauiApp.Platforms.Android.Implementations
{
    public class ColetorDeCoordenadasServiceManager : IColetorDeCoordenadasServiceManager
    {
        private Context _context = global::Android.App.Application.Context;

        public void IniciaServico()
        {
            Intent startServiceIntent = new Intent(_context, typeof(ColetorDeCoordenadasService));
            startServiceIntent.SetAction("START_SERVICE"); // pode ser usado na service para definir comportamentos

            if (Build.VERSION.SdkInt >= BuildVersionCodes.O)
            {
#pragma warning disable CA1416 // Validate platform compatibility
                _context.StartForegroundService(startServiceIntent);
#pragma warning restore CA1416 // Validate platform compatibility
            }
            else
            {
                _context.StartService(startServiceIntent);
            }
        }

        public void EncerraServico()
        {
            Intent stopServiceIntent = new Intent(_context, typeof(ColetorDeCoordenadasService));
            stopServiceIntent.SetAction("STOP_SERVICE"); // esta string pode ser usada na service para tomar decisões
            _context.StopService(stopServiceIntent);
        }

        public bool IsServicoEmExecucao()
        {
            return ColetorDeCoordenadasService.IsServicoEmExecucao;
        }
    }
}
