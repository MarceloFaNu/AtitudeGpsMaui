using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using AtitudeGpsMauiApp.Domain.Constantes;
using AtitudeGpsMauiApp.Platforms.Android.Interfaces;
using AtitudeGpsMauiApp.Services.Interfaces;

namespace AtitudeGpsMauiApp.Platforms.Android.Implementations
{
    [Service(ForegroundServiceType = ForegroundService.TypeLocation)]
    public class ColetorDeCoordenadasService : Service
    {
        #region Initialize
        // O Context é uma referência à aplicação em que a notificação
        // está sendo gerada. Esta referência também é passada como
        // parâmetro para o builder da notificação
        private Context context = global::Android.App.Application.Context;

        // Id único e obrigatório para identificar o serviço sendo executado
        public const int ID_DO_SERVICO_EM_EXECUCAO = 9000;

        private IColetorDeCoordenadasServiceNotification _serviceNotification;
        private IColetorDeCoordenadas _coletorDeCoordenadas;

        public static bool IsServicoEmExecucao { get; private set; }
        #endregion

        public override IBinder OnBind(Intent intent)
        {
            throw new NotImplementedException();
        }

        [return: GeneratedEnum]
        public override StartCommandResult OnStartCommand(Intent intent, [GeneratedEnum] StartCommandFlags flags, int startId)
        {
            StartForeground(ID_DO_SERVICO_EM_EXECUCAO, _serviceNotification.ObtemNotification());

            bool sucesso = true;

            Task.Run(async () =>
            {
                sucesso = await _coletorDeCoordenadas.InicializaColetorAsync();
                if (!sucesso)
                {
                    this.StopSelf();
                }

                while (IsServicoEmExecucao)
                {
                    sucesso = await _coletorDeCoordenadas.ColetaGpsAsync();
                    await Task.Delay(PropriedadesDaAplicacao.IntervaloMinimo * 1000);

                    // A variável sucesso só é false quando um erro é lançado na task.
                    // Já a variável IsServicoEmExecucao só é false
                    // ao se encerrar o serviço no método OnDestroy. Desse modo, se o serviço estiver
                    // em execução e ocorrer um erro (sucesso == false), a igualdade abaixo força
                    // a interrupção do while. Por outro lado, se o serviço for interrompido
                    // intencionalmente, ainda que sucesso seja true em virtude da assincronicidade,
                    // o while será interrompido.
                    if (IsServicoEmExecucao) IsServicoEmExecucao = sucesso;

                    if (!sucesso)
                        this.StopSelf();
                }

                MessagingCenter.Send(App.Current, "ledOff");
            });

            if (sucesso)
                return StartCommandResult.Sticky;
            else
                return StartCommandResult.NotSticky;
        }

        public override void OnCreate()
        {
            base.OnCreate();
            _serviceNotification = App.Services.GetService<IColetorDeCoordenadasServiceNotification>();
            _coletorDeCoordenadas = App.Services.GetService<IColetorDeCoordenadas>();
            IsServicoEmExecucao = true;
        }

        public override void OnDestroy()
        {
            base.OnDestroy();
            IsServicoEmExecucao = false;
            _coletorDeCoordenadas.FinalizaColetor();
        }
    }
}
