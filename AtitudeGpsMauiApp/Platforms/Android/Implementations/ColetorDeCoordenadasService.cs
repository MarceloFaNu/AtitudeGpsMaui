using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using AndroidX.Core.App;
using AtitudeGpsMauiApp.Platforms.Android.Interfaces;
using AtitudeGpsMauiApp.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtitudeGpsMauiApp.Platforms.Android.Implementations
{
    [Service(ForegroundServiceType = ForegroundService.TypeLocation)]
    public class ColetorDeCoordenadasService : Service, IColetorDeCoordenadasService
    {
        // O Context é uma referência à aplicação em que a notificação
        // está sendo gerada. Esta referência também é passada como
        // parâmetro para o builder da notificação
        private Context context = global::Android.App.Application.Context;

        // Id único e obrigatório para identificar o serviço sendo executado
        public const int ID_DO_SERVICO_EM_EXECUCAO = 9000;

        // Identificador único da notificação para que o Android saiba
        // como localizá-la em sua lista de notificações. Este valor
        // será passado para o builder da notificação
        private const string ID_DO_CANAL_DA_NOTIFICACAO = "9001";

        private bool _isServicoEmExecucao;

        public override IBinder OnBind(Intent intent)
        {
            throw new NotImplementedException();
        }

        [return: GeneratedEnum]
        public override StartCommandResult OnStartCommand(Intent intent, [GeneratedEnum] StartCommandFlags flags, int startId)
        {
            if (intent.Action == "START_SERVICE")
            {
                var notificacao = this.RegistraNotificacao();
                StartForeground(ID_DO_SERVICO_EM_EXECUCAO, notificacao);
            }
            else if (intent.Action == "STOP_SERVICE")
            {
                StopSelfResult(ID_DO_SERVICO_EM_EXECUCAO);
            }

            return StartCommandResult.Sticky;
        }

        public override void OnCreate()
        {
            base.OnCreate();
            _isServicoEmExecucao = true;
        }

        public override void OnDestroy()
        {
            base.OnDestroy();
            _isServicoEmExecucao = false;
        }

        public void IniciaServico()
        {
            Intent startService = new Intent(context, typeof(ColetorDeCoordenadasService));
            startService.SetAction("START_SERVICE");

            if (Build.VERSION.SdkInt >= BuildVersionCodes.O)
            {
#pragma warning disable CA1416 // Validate platform compatibility
                context.StartForegroundService(startService);
#pragma warning restore CA1416 // Validate platform compatibility
            }
            else
            {
                context.StartService(startService);
            }
        }

        public void EncerraServico()
        {
            Intent stopService = new Intent(context, this.Class);
            stopService.SetAction("STOP_SERVICE");
            context.StopService(stopService);
        }

        public bool IsServicoEmExecucao()
        {
            return _isServicoEmExecucao;
        }

        private Notification RegistraNotificacao()
        {
            // A classe Intent é uma descrição abstrata de uma operação que
            // se pretende solicitar ao Android que execute. No construtor são
            // passadas informações sobre qual a tarefa e quais os dados
            // necessários para que seja executada. Neste caso, a operação
            // é executar a Activity Main quando o usuário clicar na
            // notificação (by chatGPT)
            var operacao = new Intent(context, typeof(MainActivity));

            // usar a mesma instância da Main caso já esteja em execução
            operacao.AddFlags(ActivityFlags.SingleTop);

            // estes dados podem ser lidos pela Main Activity quando ela for instanciada ou reutilizada após o clique do usuário
            operacao.PutExtra("Algum título aqui", "Alguma mensagem aqui");

            // A classe PendingIntent informa ao Android que a Intent deverá
            // ser executada em um próximo momento quando certas condições
            // forem satisfeitas. O enumerador PendingIntentFlags indica
            // ao Android qual providência deverá ser tomada caso a PendingIntent
            // já tenha sido, ou não, iniciada anteriormente
            var condicoesDaOperacao = PendingIntent.GetActivity(context, 0, operacao, PendingIntentFlags.UpdateCurrent);

            var builderDaNotificacao = new NotificationCompat.Builder(context, ID_DO_CANAL_DA_NOTIFICACAO)
              .SetContentTitle("Titulo da Notificação")
              .SetContentText("Texto do corpo da notificaçao")
              //.SetSmallIcon(Resource.Drawable.MetroIcon) // ícone da notificação
              .SetOngoing(true) // a notificação não pode ser removida pelo usuário
              .SetContentIntent(condicoesDaOperacao); // a PendingIntent deve ser ativada sob o clique na notificação

            // Cria um canal de notificação que é um quesito obrigatório a partir
            // do Android 8.0 (API 26 ou superior)
            if (global::Android.OS.Build.VERSION.SdkInt >= BuildVersionCodes.O)
            {
#pragma warning disable CA1416 // Validate platform compatibility
                NotificationChannel canalDaNotificacao = new NotificationChannel(ID_DO_CANAL_DA_NOTIFICACAO, "Title", NotificationImportance.High);
                canalDaNotificacao.Importance = NotificationImportance.High; // ?
                canalDaNotificacao.EnableLights(true);
                canalDaNotificacao.EnableVibration(true);
                canalDaNotificacao.SetShowBadge(true);
                canalDaNotificacao.SetVibrationPattern(new long[]
                { 100, 200, 300, 400, 500, 400, 300, 200, 400 });

                var gerenciadorDeNotificacoes = context.GetSystemService(Context.NotificationService) as NotificationManager;
                if (gerenciadorDeNotificacoes != null)
                {
                    builderDaNotificacao.SetChannelId(ID_DO_CANAL_DA_NOTIFICACAO);
                    gerenciadorDeNotificacoes.CreateNotificationChannel(canalDaNotificacao);
                }
#pragma warning restore CA1416 // Validate platform compatibility
            }

            return builderDaNotificacao.Build();
        }
    }
}
