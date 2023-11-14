using AtitudeGpsMauiApp.Domain.Constantes;
using AtitudeGpsMauiApp.Services.Interfaces;

namespace AtitudeGpsMauiApp.Services.Implementations
{
    internal class SequencerDeEntidades : ISequencerDeEntidades
    {
        public int ObtemIdAtualParaResumo()
        {
            return PropriedadesDaAplicacao.IdResumo;
        }

        public int ObtemIdAtualParaCopiloto()
        {
            return PropriedadesDaAplicacao.IdCopiloto;
        }

        public int ObtemIdAtualParaMonitor()
        {
            return PropriedadesDaAplicacao.IdMonitor;
        }

        public int ObtemProximoIdParaResumo()
        {
            Preferences.Set(PropriedadesDaAplicacao.PROP_ID_RESUMO, ++PropriedadesDaAplicacao.IdResumo);
            return PropriedadesDaAplicacao.IdResumo;
        }

        public int ObtemProximoIdParaCopiloto()
        {
            Preferences.Set(PropriedadesDaAplicacao.PROP_ID_COPILOTO, ++PropriedadesDaAplicacao.IdCopiloto);
            return PropriedadesDaAplicacao.IdCopiloto;
        }

        public int ObtemProximoIdParaMonitor()
        {
            Preferences.Set(PropriedadesDaAplicacao.PROP_ID_MONITOR, ++PropriedadesDaAplicacao.IdMonitor);
            return PropriedadesDaAplicacao.IdMonitor;
        }

        public void ReiniciaTodasSequencias()
        {
            Preferences.Set(PropriedadesDaAplicacao.PROP_ID_RESUMO, 0);
            Preferences.Set(PropriedadesDaAplicacao.PROP_ID_COPILOTO, 0);
            Preferences.Set(PropriedadesDaAplicacao.PROP_ID_MONITOR, 0);
            PropriedadesDaAplicacao.IdResumo = 0;
            PropriedadesDaAplicacao.IdCopiloto = 0;
            PropriedadesDaAplicacao.IdMonitor = 0;
        }
    }
}