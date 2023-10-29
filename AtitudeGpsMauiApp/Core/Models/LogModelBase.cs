using AtitudeGpsMauiApp.Domain.Enumeradores;

namespace AtitudeGpsMauiApp.Core.Models
{
    public abstract class LogModelBase
    {
        public int Id { get; set; }
        public TipoDeLogEnum TipoDeLog { get; set; }
        public long MomentumInicial { get; set; }
        public long MomentumFinal { get; set; }
    }
}
