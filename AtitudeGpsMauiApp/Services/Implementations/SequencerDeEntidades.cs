using AtitudeGpsMauiApp.Services.Interfaces;

namespace AtitudeGpsMauiApp.Services.Implementations
{
    internal class SequencerDeEntidades : ISequencerDeEntidades
    {
        private static int _idResumo;
        private static int _idCopiloto;
        private static int _idMonitor;

        public int ObtemIdAtualParaResumo()
        {
            return _idResumo;
        }

        public int ObtemIdAtualParaCopiloto()
        {
            return _idCopiloto;
        }

        public int ObtemIdAtualParaMonitor()
        {
            return _idMonitor;
        }

        public int ObtemProximoIdParaResumo()
        {
            return ++_idResumo;
        }

        public int ObtemProximoIdParaCopiloto()
        {
            return ++_idCopiloto;
        }

        public int ObtemProximoIdParaMonitor()
        {
            return ++_idMonitor;
        }

        public void ReiniciaTodasSequencias()
        {
            _idResumo = 0;
            _idCopiloto = 0;
            _idMonitor = 0;
        }
    }
}