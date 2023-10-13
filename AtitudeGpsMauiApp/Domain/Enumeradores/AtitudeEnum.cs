using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtitudeGpsMauiApp.Domain.Enumeradores
{
    public enum AtitudeEnum
    {
        Nada, Parado, Caminhada, Veiculo
    }

    public static class AtitudeEnumExtensions
    {
        public static string ToAtitudeString(this AtitudeEnum value)
        {
            switch (value)
            {
                case AtitudeEnum.Parado:
                    return "Parado";
                case AtitudeEnum.Caminhada:
                    return "Caminhada";
                case AtitudeEnum.Veiculo:
                    return "Veículo";
                default:
                    return "Nada";
            }
        }
    }
}
