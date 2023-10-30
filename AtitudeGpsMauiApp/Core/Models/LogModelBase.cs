using AtitudeGpsMauiApp.Domain.Enumeradores;

namespace AtitudeGpsMauiApp.Core.Models
{
    public abstract class LogModelBase
    {
        public int Id { get; set; }
        public TipoDeLogEnum TipoDeLog { get; set; }
    }
}
