using AtitudeGpsMauiApp.Core.Models;
using AtitudeGpsMauiApp.Infrastructure.Interfaces;
using System.IO.Compression;
using System.Text.Json;

namespace AtitudeGpsMauiApp.Infrastructure.Implementations
{
    public class OperadorDeDiretorios : IOperadorDeDiretorios
    {
        private static readonly string _diretorioApp = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        private static readonly string _diretorioLogs = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "logs");
        private static readonly string _resumoLog = Path.Combine(_diretorioLogs, "resumo.txt");
        private static readonly string _monitorLog = Path.Combine(_diretorioLogs, "monitor.txt");
        private static readonly string _copilotoLog = Path.Combine(_diretorioLogs, "copiloto.txt");

        public OperadorDeDiretorios()
        {
            if (!Directory.Exists(_diretorioLogs))
            {
                Directory.CreateDirectory(_diretorioLogs);
            }
        }

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

        public string CriaZipParaEntrega(string idDoArquivo)
        {
            string caminhoDoArquivoZip = Path.Combine(_diretorioApp, "AtitudeGpsLogs_" +  idDoArquivo + ".zip");
            ZipFile.CreateFromDirectory(_diretorioLogs, caminhoDoArquivoZip);
            return caminhoDoArquivoZip;
        }

        public void ApagaZipDeEntrega(string idDoArquivo)
        {
            string caminhoDoArquivoZip = Path.Combine(_diretorioApp, "AtitudeGpsLogs_" + idDoArquivo + ".zip");
            if (File.Exists(caminhoDoArquivoZip))
            {
                File.Delete(caminhoDoArquivoZip);
            }
        }

        public void LimpaLogs(string idDoArquivoZip)
        {
            File.WriteAllText(_resumoLog, "");
            File.WriteAllText(_monitorLog, "");
            File.WriteAllText(_copilotoLog, "");

            ApagaZipDeEntrega(idDoArquivoZip);
        }
    }
}
