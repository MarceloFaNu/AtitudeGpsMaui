using AtitudeGpsMauiApp.Core.Models;
using AtitudeGpsMauiApp.Infrastructure.Interfaces;
using System.Text.Json;

namespace AtitudeGpsMauiApp.Infrastructure.Implementations
{
    public class OperadorDeDiretorios : IOperadorDeDiretorios
    {
        private readonly string _resumoLog = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "resumo.log");
        private readonly string _monitorLog = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "monitor.log");
        private readonly string _copilotoLog = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "copiloto.log");

        public bool ExisteArquivoDeLogDoResumo()
        {
            return File.Exists(_resumoLog) && ObtemTamanhoDoLogDoResumoEmBytes() > 0;
        }

        public bool ExisteArquivoDeLogDoMonitor()
        {
            return File.Exists(_monitorLog) && ObtemTamanhoDoLogDoMonitorEmBytes() > 0;
        }

        public bool ExisteArquivoDeLogDoCopiloto()
        {
            return File.Exists(_copilotoLog) && ObtemTamanhoDoLogDoCopilotoEmBytes() > 0;
        }

        public void AdicionaLinhaAoLogDoResumo(Resumo resumo)
        {
            string linha = JsonSerializer.Serialize(resumo);
            File.AppendAllText(_resumoLog, linha + "\n");
        }

        public void AdicionaLinhaAoLogDoMonitor(Snapshot snapshot)
        {
            string linha = JsonSerializer.Serialize(snapshot);
            File.AppendAllText(_monitorLog, linha + "\n");
        }

        public void AdicionaLinhaAoLogDoCopiloto(Copiloto copiloto)
        {
            string linha = JsonSerializer.Serialize(copiloto);
            File.AppendAllText(_copilotoLog, linha + "\n");
        }

        public string ObtemCaminhoDoArquivoLogDoResumo()
        {
            return _resumoLog;
        }

        public string ObtemCaminhoDoArquivoLogDoMonitor()
        {
            return _monitorLog;
        }

        public string ObtemCaminhoDoArquivoLogDoCopiloto()
        {
            return _copilotoLog;
        }

        public long ObtemTamanhoDoLogDoResumoEmBytes()
        {
            return new FileInfo(_resumoLog).Length;
        }

        public long ObtemTamanhoDoLogDoMonitorEmBytes()
        {
            return new FileInfo(_monitorLog).Length;
        }

        public long ObtemTamanhoDoLogDoCopilotoEmBytes()
        {
            return new FileInfo(_copilotoLog).Length;
        }

        public void LimpaLogs()
        {
            File.WriteAllText(_resumoLog, "");
            File.WriteAllText(_monitorLog, "");
            File.WriteAllText(_copilotoLog, "");
        }
    }
}
