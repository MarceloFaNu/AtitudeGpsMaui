using System.Runtime.CompilerServices;

namespace AtitudeGpsMauiApp.Domain.Extensions
{
    public static class GeolocationAccuracyExtensions
    {
        public static string ToAccuracyString(this GeolocationAccuracy geolocationAccuracy)
        {
            return geolocationAccuracy switch
            {
                GeolocationAccuracy.Default => "Padrão",
                GeolocationAccuracy.Lowest  => "Mínimo",
                GeolocationAccuracy.Low     => "Baixo",
                GeolocationAccuracy.Medium  => "Médio",
                GeolocationAccuracy.High    => "Alto",
                GeolocationAccuracy.Best    => "Máximo",
                                          _ => string.Empty
            };
        }

        public static GeolocationAccuracy? ToGeolocationAccuracy(this string geolocString)
        {
            return geolocString switch
            {
                "Mínimo" => GeolocationAccuracy.Lowest,
                "Baixo" => GeolocationAccuracy.Low,
                "Médio" => GeolocationAccuracy.Medium,
                "Alto" => GeolocationAccuracy.High,
                "Máximo" => GeolocationAccuracy.Best,
                        _ => GeolocationAccuracy.Default
            };
        }
    }
}
