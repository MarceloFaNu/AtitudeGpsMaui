using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtitudeGpsMauiApp.Services.Interfaces
{
    internal interface ISequencerDeEntidades
    {
        int ObtemIdAtualParaResumo();
        int ObtemIdAtualParaCopiloto();
        int ObtemIdAtualParaMonitor();
        int ObtemProximoIdParaResumo();
        int ObtemProximoIdParaCopiloto();
        int ObtemProximoIdParaMonitor();
        void ReiniciaTodasSequencias();
    }
}
