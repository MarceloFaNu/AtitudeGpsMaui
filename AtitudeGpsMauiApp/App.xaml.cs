namespace AtitudeGpsMauiApp;

public partial class App : Application
{
    public static IServiceProvider Services => Application.Current.Handler.MauiContext.Services;

    public App()
    {
        InitializeComponent();
        MainPage = new AppShell();
    }
}
