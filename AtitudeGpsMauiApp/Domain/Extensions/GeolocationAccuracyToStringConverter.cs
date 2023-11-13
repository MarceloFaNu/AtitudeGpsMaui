using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtitudeGpsMauiApp.Domain.Extensions
{
    public class GeolocationAccuracyToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            GeolocationAccuracy geolocAcc = GeolocationAccuracy.Default;

            if (value != null)
                geolocAcc = (GeolocationAccuracy)value;

            return geolocAcc.ToAccuracyString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string geolocString = (string)value;
            return geolocString.ToGeolocationAccuracy();
        }
    }
}
