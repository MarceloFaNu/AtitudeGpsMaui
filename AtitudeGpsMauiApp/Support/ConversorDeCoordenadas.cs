using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtitudeGpsMauiApp.Support
{
    public class ConversorDeCoordenadas
    {
        public Location AplicaLimiteDeDigitosComArredondamento(Location loc, int qtdDigitos)
        {
            loc.Latitude = Math.Round(loc.Latitude, qtdDigitos);
            loc.Longitude = Math.Round(loc.Longitude, qtdDigitos);
            return loc;
        }

        public Location AplicaLimiteDeDigitosSemArredondamento(Location loc, int qtdDigitos)
        {
            loc.Latitude = Math.Truncate(loc.Latitude * qtdDigitos) / qtdDigitos;
            loc.Longitude = Math.Truncate(loc.Latitude * qtdDigitos) / qtdDigitos;
            return loc;
        }
    }
}
