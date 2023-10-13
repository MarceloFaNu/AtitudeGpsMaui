using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtitudeGpsMauiApp.Services.Interfaces
{
    public interface IColetorDeCoordenadas
    {
        bool InicializaColetor();
        bool ColetaGps();
        void ReiniciaContadorDeTicks();
        void LimpaCoordenadas();
        void FinalizaColetor();
    }
}
