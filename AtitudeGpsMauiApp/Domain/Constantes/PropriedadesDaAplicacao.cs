namespace AtitudeGpsMauiApp.Domain.Constantes
{
    public class PropriedadesDaAplicacao
    {
        #region config
        public const string PROP_INTERVALO_MINIMO = "intervalo_minimo";
        public const string PROP_FATOR_CASAS_DECIMAIS = "fator_casas_decimais";
        public const string PROP_DISTANCIA_MINIMA_VALIDA = "distancia_minima_valida";
        public const string PROP_PRECISAO_DE_LOCALIZACAO = "precisao_de_localizacao";
        public const string PROP_GEOLOCATION_REQUEST_TIMEOUT = "location_request_timeout";
        public const string PROP_MEDIA_ARITMETICA_PADRAO = "media_aritmetcia_padrao";

        public static int IntervaloMinimo;
        public static int FatorDeCasasDecimais;
        public static int MediaAritmeticaPadrao;
        public static int GeolocationRequestTimeout;
        public static double DistanciaMinimaValida;
        public static GeolocationAccuracy PrecisaoDeGeolocalizacao;
        #endregion

        #region ids
        public const string PROP_ID_RESUMO = "id_resumo";
        public const string PROP_ID_COPILOTO = "id_copiloto";
        public const string PROP_ID_MONITOR = "id_monitor";

        public static int IdResumo;
        public static int IdCopiloto;
        public static int IdMonitor;
        #endregion
    }
}
