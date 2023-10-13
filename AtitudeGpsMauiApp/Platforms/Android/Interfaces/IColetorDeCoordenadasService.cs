using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtitudeGpsMauiApp.Platforms.Android.Interfaces
{
    public interface IColetorDeCoordenadasService
    {
        void IniciaServico();
        void EncerraServico();
        bool IsServicoEmExecucao();
    }
}
