using AtitudeGpsMauiApp.Core.Models;

namespace AtitudeGpsMauiApp.Infrastructure.Interfaces
{
    public interface IOperadorDeDiretorios
    {
        bool ExisteArquivoDeLogDoResumo();
        bool ExisteArquivoDeLogDoMonitor();
        bool ExisteArquivoDeLogDoCopiloto();

        void AdicionaLinhaAoLogDoResumo(Resumo resumo);
        void AdicionaLinhaAoLogDoMonitor(Snapshot snapshot);
        void AdicionaLinhaAoLogDoCopiloto(Copiloto copiloto);

        string ObtemCaminhoDoArquivoLogDoResumo();
        string ObtemCaminhoDoArquivoLogDoMonitor();
        string ObtemCaminhoDoArquivoLogDoCopiloto();

        long ObtemTamanhoDoLogDoResumoEmBytes();
        long ObtemTamanhoDoLogDoMonitorEmBytes();
        long ObtemTamanhoDoLogDoCopilotoEmBytes();

        void LimpaLogs();
    }
}
