using System.Drawing;

namespace AtitudeGpsMauiApp.Pages;

public partial class Copiloto : ContentPage
{
	private Guid _ibtnId;
	private readonly Guid _ibtnDefaultId;

	public Copiloto()
	{
		InitializeComponent();
	}

	public void DesligaTodosBotoes()
	{
		MainThread.BeginInvokeOnMainThread(() =>
		{
			this.ibtnDesembarcadoParado.BackgroundColor = Colors.LightGray;
			this.ibtnDesembarcadoMovimento.BackgroundColor = Colors.LightGray;
			this.ibtnEmbarcadoParado.BackgroundColor = Colors.LightGray;
			this.ibtnEmbarcadoMovimento.BackgroundColor = Colors.LightGray;
		});
	}

	private void ibtn_Pressed(object sender, EventArgs e)
	{
        var ibtn = sender as ImageButton;

        DesligaTodosBotoes();

        MainThread.BeginInvokeOnMainThread(() =>
        {
            ibtn.BackgroundColor = Colors.White;
        });
    }

	private void ibtn_Released(object sender, EventArgs e)
	{
        var ibtn = sender as ImageButton;

        DesligaTodosBotoes();

        MainThread.BeginInvokeOnMainThread(() =>
        {
            if (ibtn.Id == _ibtnId)
			{
                ibtn.BackgroundColor = Colors.LightGray;
				_ibtnId = _ibtnDefaultId;
			}
			else
			{
                ibtn.BackgroundColor = Colors.LightGreen;
				_ibtnId = ibtn.Id;
			}
        });
    }
}