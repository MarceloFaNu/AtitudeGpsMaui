namespace AtitudeGpsMauiApp.Domain.Constantes
{
    public class PropriedadesDaAplicacao
    {
        public const string PROP_INTERVALO_MINIMO = "intervalo_minimo";
        public const string PROP_FATOR_CASAS_DECIMAIS = "fator_casas_decimais";
        public const string PROP_DISTANCIA_MINIMA_VALIDA = "distancia_minima_valida";
        public const string PROP_PRECISAO_DE_LOCALIZACAO = "precisao_de_localizacao";
        public const string PROP_GEOLOCATION_REQUEST_TIMEOUT = "location_request_timeout";

        public static int IntervaloMinimo;
        public static int FatorDeCasasDecimais;
        public static int GeolocationRequestTimeout;
        public static double DistanciaMinimaValida;
        public static GeolocationAccuracy PrecisaoDeGeolocalizacao;
    }
}
