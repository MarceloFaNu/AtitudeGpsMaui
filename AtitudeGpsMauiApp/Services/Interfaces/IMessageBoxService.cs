using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtitudeGpsMauiApp.Services.Interfaces
{
    public interface IMessageBoxService
    {
        Task ShowAsync(string mensagem);
    }
}
