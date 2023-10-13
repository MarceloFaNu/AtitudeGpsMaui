using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtitudeGpsMauiApp.Infrastructure.Interfaces
{
    public interface ILeitorDeCoordenadas
    {
        void DefineParametrosDeGeolocationRequest(GeolocationAccuracy accuracy, int timeout);
        Location TentaObterLocalizacaoUmaUnicaVez();
        Location TentaObterLocalizacaoComContagemRegressiva(int segundos);
        Location TentaObterLocalizacaoPorQuantidadeLimitadaDeTentativas(int quantidadeDeTentativas);
    }
}
