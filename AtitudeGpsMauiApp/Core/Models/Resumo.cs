namespace AtitudeGpsMauiApp.Core.Models
{
    public class Resumo : LogModelBase
    {
        public DateTime InicioDoMonitoramento { get; }
        public DateTime FinalDoMonitoramento { get; }
        public double TempoTotalDoMonitoramentoEmMinutos { get; }
        public int IntervaloMinimoEmSegundos { get; set; }
        public int FatorDeCasasDecimais { get; set; }
        public int GeolocationRequestTimeout { get; set; }
        public double DistanciaMinimaConsiderada { get; set; }
        public GeolocationAccuracy SensibilidadeGps { get; set; }

        public Resumo(DateTime inicioDoMonitoramento, DateTime finalDoMonitoramento)
        {
            InicioDoMonitoramento = inicioDoMonitoramento;
            FinalDoMonitoramento = finalDoMonitoramento;
            TempoTotalDoMonitoramentoEmMinutos = Math.Round((finalDoMonitoramento - inicioDoMonitoramento).TotalMinutes);
        }
    }
}
