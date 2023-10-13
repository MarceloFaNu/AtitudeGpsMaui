namespace AtitudeGpsMauiApp.Pages;

public partial class MainPage : ContentPage
{
	public MainPage()
	{
		InitializeComponent();
        DesativaLed(null);
	}

    void AtivaLed(object sender)
    {
        MainThread.BeginInvokeOnMainThread(() =>
        {
            this.btnMonitorar.IsEnabled = false;
            this.btnParar.IsEnabled = true;
            this.btnEnviar.IsEnabled = false;
            this.btnLimparLog.IsEnabled = false;
            this.lblStatus.Background = Colors.Green;
            this.lblStatus.Text = "ONLINE";
        });
    }

    void DesativaLed(object sender)
    {
        MainThread.BeginInvokeOnMainThread(() =>
        {
            this.btnMonitorar.IsEnabled = true;
            this.btnParar.IsEnabled = false;
            this.btnEnviar.IsEnabled = true;
            this.btnLimparLog.IsEnabled = true;
            this.lblStatus.Background = Colors.Red;
            this.lblStatus.Text = "OFFLINE";
        });
    }

    private void btnMonitorar_Clicked(object sender, EventArgs e)
    {
        AtivaLed(sender);
    }

    private void btnParar_Clicked(object sender, EventArgs e)
    {
        DesativaLed(sender);
    }
}

