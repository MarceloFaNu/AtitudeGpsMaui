using AtitudeGpsMauiApp.Core.Models;
using AtitudeGpsMauiApp.Services.Interfaces;
using System.Text.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtitudeGpsMauiApp.Services.Implementations
{
    public class ColetorDeCoordenadas : IColetorDeCoordenadas
    {
        private readonly IMessageBoxService _msgBox;

        public ColetorDeCoordenadas(IMessageBoxService msgBox)
        {
            _msgBox = msgBox;
        }

        public bool InicializaColetor()
        {
            throw new NotImplementedException();
        }

        public bool ColetaGps()
        {
            throw new NotImplementedException();
        }

        public void ReiniciaContadorDeTicks()
        {
            throw new NotImplementedException();
        }

        public void LimpaCoordenadas()
        {
            throw new NotImplementedException();
        }

        public void FinalizaColetor()
        {
            throw new NotImplementedException();
        }

        private bool ExecuteEmTryCatch(Action action)
        {
            if (action == null) return false;
            string msgDeErro = string.Empty;

            try
            {
                action.Invoke();
                return true;
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                // Handle not supported on device exception
                msgDeErro = "Este dispositivo não possui a funcionalidade de GPS.";
            }
            catch (FeatureNotEnabledException fneEx)
            {
                // Handle not enabled on device exception
                msgDeErro = "Ative o serviço de GPS do dispositivo.";
            }
            catch (PermissionException pEx)
            {
                // Handle permission exception
                msgDeErro = "Conceda permissão para o aplicativo acessar as informações de GPS do dispositivo. Sua privacidade sempre será respeitada.";
            }
            catch (Exception ex)
            {
                // Unable to get location
                msgDeErro = ex.Message;
            }

            MainThread.BeginInvokeOnMainThread(async () =>
            {
                await _msgBox.ShowAsync(msgDeErro);
            });

            return false;
        }

        private void LogaGps(Snapshot snapshot)
        {
            string locAtualJsonObject = JsonSerializer.Serialize(snapshot);
            System.Diagnostics.Debug.WriteLine(snapshot);
            //File.AppendAllText(DashboardPage.CoordsLog, locAtualJsonObject + "\n");
        }
    }
}
