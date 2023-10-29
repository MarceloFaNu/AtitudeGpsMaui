using AtitudeGpsMauiApp.Domain.Constantes;
using AtitudeGpsMauiApp.Infrastructure.Interfaces;
using AtitudeGpsMauiApp.Services.Interfaces;

namespace AtitudeGpsMauiApp.Pages;

public partial class ConfigPage : ContentPage
{
    private readonly IMessageBoxService _msgBox;
    private readonly ILeitorDeCoordenadas _leitorDeCoordenadas;
    private readonly IColetorDeCoordenadasServiceManager _coletorManager;

    public ConfigPage()
    {
        InitializeComponent();

        _msgBox = App.Services.GetService<IMessageBoxService>();
        _leitorDeCoordenadas = App.Services.GetService<ILeitorDeCoordenadas>();
        _coletorManager = App.Services.GetService<IColetorDeCoordenadasServiceManager>();

        MessagingCenter.Subscribe<Application>(App.Current, "ledOn", this.BloqueiaFormulario);
        MessagingCenter.Subscribe<Application>(App.Current, "ledOff", this.DesbloqueiaFormulario);

        AtualizaControles();

        if (_coletorManager.IsServicoEmExecucao()) this.BloqueiaFormulario(null);
    }

    private void AtualizaControles()
    {
        this.stpTickInterval.Value = Preferences.Get(PropriedadesDaAplicacao.PROP_INTERVALO_MINIMO, 5);
        this.stpCasasDecimais.Value = Preferences.Get(PropriedadesDaAplicacao.PROP_FATOR_CASAS_DECIMAIS, 5);
        this.stpDistanciaMinima.Value = Preferences.Get(PropriedadesDaAplicacao.PROP_DISTANCIA_MINIMA_VALIDA, 10.0D);
        this.pkrNivelPrecisao.SelectedItem = (GeolocationAccuracy)Preferences.Get(PropriedadesDaAplicacao.PROP_PRECISAO_DE_LOCALIZACAO, 3);
        this.sptLocationRequestTimeout.Value = Preferences.Get(PropriedadesDaAplicacao.PROP_GEOLOCATION_REQUEST_TIMEOUT, 5);
    }

    private void BloqueiaFormulario(object sender)
    {
        MainThread.BeginInvokeOnMainThread(() =>
        {
            this.stkPrincipal.IsVisible = false;
            this.lblMonitorEmAtividade.IsVisible = true;
        });
    }

    private void DesbloqueiaFormulario(object sender)
    {
        MainThread.BeginInvokeOnMainThread(() =>
        {
            this.stkPrincipal.IsVisible = true;
            this.lblMonitorEmAtividade.IsVisible = false;
        });
    }

    private void btnAplicarAlteracoes_Clicked(object sender, EventArgs e)
    {
        Preferences.Set(PropriedadesDaAplicacao.PROP_INTERVALO_MINIMO, (int)this.stpTickInterval.Value);
        Preferences.Set(PropriedadesDaAplicacao.PROP_FATOR_CASAS_DECIMAIS, (int)this.stpCasasDecimais.Value);
        Preferences.Set(PropriedadesDaAplicacao.PROP_DISTANCIA_MINIMA_VALIDA, (double)this.stpDistanciaMinima.Value);
        Preferences.Set(PropriedadesDaAplicacao.PROP_PRECISAO_DE_LOCALIZACAO, (int)this.pkrNivelPrecisao.SelectedItem);
        Preferences.Set(PropriedadesDaAplicacao.PROP_GEOLOCATION_REQUEST_TIMEOUT, (int)this.sptLocationRequestTimeout.Value);

        PropriedadesDaAplicacao.IntervaloMinimo = (int)this.stpTickInterval.Value;
        PropriedadesDaAplicacao.FatorDeCasasDecimais = (int)this.stpCasasDecimais.Value;
        PropriedadesDaAplicacao.DistanciaMinimaValida = (double)this.stpDistanciaMinima.Value;
        PropriedadesDaAplicacao.PrecisaoDeGeolocalizacao = (GeolocationAccuracy)this.pkrNivelPrecisao.SelectedItem;
        PropriedadesDaAplicacao.GeolocationRequestTimeout = (int)this.sptLocationRequestTimeout.Value;

        _leitorDeCoordenadas.DefineParametrosDeGeolocationRequest(
            PropriedadesDaAplicacao.PrecisaoDeGeolocalizacao,
            PropriedadesDaAplicacao.GeolocationRequestTimeout);

        _msgBox.ShowAsync("As novas configurações foram aplicadas.");
    }
}