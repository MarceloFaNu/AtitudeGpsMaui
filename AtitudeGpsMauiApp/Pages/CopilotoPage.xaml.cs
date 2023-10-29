using AtitudeGpsMauiApp.Core.Models;
using AtitudeGpsMauiApp.Domain.Enumeradores;
using AtitudeGpsMauiApp.Infrastructure.Interfaces;
using AtitudeGpsMauiApp.Services.Interfaces;

namespace AtitudeGpsMauiApp.Pages;

public partial class CopilotoPage : ContentPage
{
    private Guid _ibtnId;
    private AtitudeEnum _atitudeEnumAtual;
    private readonly Guid _ibtnDefaultId;
    private readonly IOperadorDeDiretorios _operadorDeDiretorios;
    private readonly IColetorDeCoordenadasServiceManager _coletorManager;

    public CopilotoPage()
    {
        InitializeComponent();
        _operadorDeDiretorios = App.Services.GetService<IOperadorDeDiretorios>();
        _coletorManager = App.Services.GetService<IColetorDeCoordenadasServiceManager>();
    }

    private void ibtnDesembarcadoParado_Clicked(object sender, EventArgs e)
    {
        this.ibtn_Cliked(sender, AtitudeEnum.DesembarcadoParado);
    }

    private void ibtnDesembarcadoMovimento_Clicked(object sender, EventArgs e)
    {
        this.ibtn_Cliked(sender, AtitudeEnum.DesembarcadoEmMovimento);
    }

    private void ibtnEmbarcadoParado_Clicked(object sender, EventArgs e)
    {
        this.ibtn_Cliked(sender, AtitudeEnum.EmbarcadoParado);
    }

    private void ibtnEmbarcadoMovimento_Clicked(object sender, EventArgs e)
    {
        this.ibtn_Cliked(sender, AtitudeEnum.EmbarcadoEmMovimento);
    }

    private void ibtn_Cliked(object sender, AtitudeEnum novaAtitudeEnum)
    {
        DesligaTodosBotoes();

        var ibtn = sender as ImageButton;
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

        if (!_coletorManager.IsServicoEmExecucao()) return;

        if (novaAtitudeEnum != _atitudeEnumAtual)
        {
            var copiloto = new Copiloto
            {
                TipoDeLog = TipoDeLogEnum.Copiloto,
                MomentumInicial = DateTime.Now.Ticks,
                Atitude = novaAtitudeEnum
            };

            _operadorDeDiretorios.AdicionaLinhaAoLogDoCopiloto(copiloto);
        }

        _atitudeEnumAtual = novaAtitudeEnum;
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
}