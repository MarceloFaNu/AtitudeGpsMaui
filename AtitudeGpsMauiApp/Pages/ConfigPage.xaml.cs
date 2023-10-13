using AtitudeGpsMauiApp.Domain.Constantes;

namespace AtitudeGpsMauiApp.Pages;

public partial class ConfigPage : ContentPage
{
	public ConfigPage()
	{
		InitializeComponent();
        AtualizaControles();
	}

	private void AtualizaControles()
	{
        this.stpTickInterval.Value = Preferences.Get(PropriedadesDaAplicacao.PROP_INTERVALO_MINIMO, 5);
        this.stpCasasDecimais.Value = Preferences.Get(PropriedadesDaAplicacao.PROP_FATOR_CASAS_DECIMAIS, 5);
        this.stpDistanciaMinima.Value = Preferences.Get(PropriedadesDaAplicacao.PROP_DISTANCIA_MINIMA_VALIDA, 10.0D);
        this.pkrNivelPrecisao.SelectedItem = (GeolocationAccuracy)Preferences.Get(PropriedadesDaAplicacao.PROP_PRECISAO_DE_LOCALIZACAO, 3);
        this.sptLocationRequestTimeout.Value = Preferences.Get(PropriedadesDaAplicacao.PROP_GEOLOCATION_REQUEST_TIMEOUT, 5);
    }
}