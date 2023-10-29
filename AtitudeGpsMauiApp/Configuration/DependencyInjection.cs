#if ANDROID
using AtitudeGpsMauiApp.Platforms.Android.Implementations;
using AtitudeGpsMauiApp.Platforms.Android.Interfaces;
#endif

using AtitudeGpsMauiApp.Domain.Constantes;
using AtitudeGpsMauiApp.Infrastructure.Implementations;
using AtitudeGpsMauiApp.Infrastructure.Interfaces;
using AtitudeGpsMauiApp.Services.Implementations;
using AtitudeGpsMauiApp.Services.Interfaces;

namespace AtitudeGpsMauiApp.Configuration
{
    public static class DependencyInjection
    {
        public static void ConfigureApplication(this MauiAppBuilder mauiBuilder)
        {
            RegisterServices(mauiBuilder);
            ConfigureAppProperties();
        }

        private static void RegisterServices(MauiAppBuilder mauiBuilder)
        {
            mauiBuilder.Services.AddSingleton<IMessageBoxService, MessageBoxService>();
            mauiBuilder.Services.AddSingleton<ILeitorDeCoordenadas, LeitorDeCoordenadas>();
            mauiBuilder.Services.AddSingleton<IColetorDeCoordenadas, ColetorDeCoordenadas>();
            mauiBuilder.Services.AddSingleton<IOperadorDeDiretorios, OperadorDeDiretorios>();

#if ANDROID
            mauiBuilder.Services.AddSingleton<IColetorDeCoordenadasServiceManager, ColetorDeCoordenadasServiceManager>();
            mauiBuilder.Services.AddSingleton<IColetorDeCoordenadasServiceNotification, ColetorDeCoordenadasServiceNotification>();
#endif
        }

        private static void ConfigureAppProperties()
        {
            if (!Preferences.ContainsKey(PropriedadesDaAplicacao.PROP_INTERVALO_MINIMO))
                Preferences.Set(PropriedadesDaAplicacao.PROP_INTERVALO_MINIMO, 5);

            if (!Preferences.ContainsKey(PropriedadesDaAplicacao.PROP_FATOR_CASAS_DECIMAIS))
                Preferences.Set(PropriedadesDaAplicacao.PROP_FATOR_CASAS_DECIMAIS, 5);

            if (!Preferences.ContainsKey(PropriedadesDaAplicacao.PROP_DISTANCIA_MINIMA_VALIDA))
                Preferences.Set(PropriedadesDaAplicacao.PROP_DISTANCIA_MINIMA_VALIDA, 10.0D);

            if (!Preferences.ContainsKey(PropriedadesDaAplicacao.PROP_PRECISAO_DE_LOCALIZACAO))
                Preferences.Set(PropriedadesDaAplicacao.PROP_PRECISAO_DE_LOCALIZACAO, (int)GeolocationAccuracy.Medium);

            if (!Preferences.ContainsKey(PropriedadesDaAplicacao.PROP_GEOLOCATION_REQUEST_TIMEOUT))
                Preferences.Set(PropriedadesDaAplicacao.PROP_GEOLOCATION_REQUEST_TIMEOUT, 5);

            PropriedadesDaAplicacao.IntervaloMinimo = Preferences.Get(PropriedadesDaAplicacao.PROP_INTERVALO_MINIMO, 5);
            PropriedadesDaAplicacao.FatorDeCasasDecimais = Preferences.Get(PropriedadesDaAplicacao.PROP_FATOR_CASAS_DECIMAIS, 5);
            PropriedadesDaAplicacao.DistanciaMinimaValida = Preferences.Get(PropriedadesDaAplicacao.PROP_DISTANCIA_MINIMA_VALIDA, 10.0D);
            PropriedadesDaAplicacao.PrecisaoDeGeolocalizacao = (GeolocationAccuracy)Preferences.Get(PropriedadesDaAplicacao.PROP_PRECISAO_DE_LOCALIZACAO, 3);
            PropriedadesDaAplicacao.GeolocationRequestTimeout = Preferences.Get(PropriedadesDaAplicacao.PROP_GEOLOCATION_REQUEST_TIMEOUT, 5);
        }
    }
}
