using AtitudeGpsMauiApp.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtitudeGpsMauiApp.Services.Implementations
{
    public class MessageBoxService : IMessageBoxService
    {
        public async Task ShowAsync(string mensagem)
        {
            await Application.Current.MainPage.DisplayAlert("Atitude Gps", mensagem, "OK");
        }
    }
}
