using AtitudeGpsMauiApp.Domain.Enumeradores;

namespace AtitudeGpsMauiApp.Core.Models
{
    public class Snapshot : LogModelBase
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public double DistanciaEmMetros { get; set; }
        public double MetrosPorSegundo { get; set; }
        public double KilometrosPorHora { get; set; }
        public int Intervalo { get; set; }

        public Snapshot CloneShallow()
        {
            return (Snapshot)this.MemberwiseClone();
        }

        public void CalculaDistanciaEVelocidadeMedia(Snapshot snapshotAnterior)
        {
            // metros
            DistanciaEmMetros = Math.Round(1000 * (Location.CalculateDistance(
                snapshotAnterior.Latitude,
                snapshotAnterior.Longitude,
                this.Latitude,
                this.Longitude,
                DistanceUnits.Kilometers)), 2);

            // segundos
            long delta_T = (this.MomentumInicial - snapshotAnterior.MomentumInicial) / 10_000_000;

            // metros / segundo
            MetrosPorSegundo = Math.Round(DistanciaEmMetros / delta_T, 2);
            KilometrosPorHora = Math.Round(MetrosPorSegundo * 3.6, 2);
        }

        public Snapshot RedefineValoresDinamicos()
        {
            this.DistanciaEmMetros = 0;
            this.MetrosPorSegundo = 0;
            this.KilometrosPorHora = 0;

            return this;
        }

        public override string ToString()
        {
            return string.Format("{0,-5} {1,-3} {2,-5} {3,-25} {4,-5} {5,-25} {6,-6} {7,-8} {8,-5} {9,-8} | {10,-9}",
                "Ping: ", Intervalo + "s",
                "Lat: ", Latitude,
                "Lon: ", Longitude,
                "Dist: ", DistanciaEmMetros + "m",
                "Vel: ", MetrosPorSegundo + "m/s", KilometrosPorHora + "Km/h");
        }
    }
}
