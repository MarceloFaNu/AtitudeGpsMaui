namespace AtitudeGpsMauiApp.Domain.Extensions
{
    public static class GeolocationAccuracyExtensions
    {
        public static string ToAccuracyString(this GeolocationAccuracy geolocationAccuracy)
        {
            return geolocationAccuracy switch
            {
                GeolocationAccuracy.Low    => "Low",
                GeolocationAccuracy.Medium => "Medium",
                GeolocationAccuracy.High   => "High",
                GeolocationAccuracy.Best   => "Best",
                _ => string.Empty
            };
        }
    }
}
