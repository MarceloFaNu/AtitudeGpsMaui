namespace AtitudeGpsMauiApp.Domain.Enumeradores
{
    public enum AtitudeEnum
    {
        Nada, DesembarcadoParado, DesembarcadoEmMovimento, EmbarcadoParado, EmbarcadoEmMovimento
    }

    public static class AtitudeEnumExtensions
    {
        public static string ToAtitudeString(this AtitudeEnum value)
        {
            return value switch
            {
                AtitudeEnum.DesembarcadoParado      => "Desembarcado Parado",
                AtitudeEnum.DesembarcadoEmMovimento => "Desembarcado em Movimento",
                AtitudeEnum.EmbarcadoParado         => "Embarcado Parado",
                AtitudeEnum.EmbarcadoEmMovimento    => "Embarcado em Movimento",
                _   => "Atitude Indefinida"
            };
        }
    }
}
