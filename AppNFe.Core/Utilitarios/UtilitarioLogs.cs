using Serilog;
using System;

namespace AppNFe.Core.Utilitarios
{
    public class UtilitarioLogs
    {
        ILogger Logger;

        public UtilitarioLogs(ILogger logger)
        {
            Logger = logger;
        }

        public void GravarLogErro(string classe, string metodo, Exception e)
        {
            Logger.Error("Erro: " + classe + " > Método: " + metodo + " Detalhes: " + e.Message);
        }

        public void GravarLogErro(string classe, string metodo, string mensagem)
        {
            Logger.Error("Erro: " + classe + " > Método: " + metodo + " Detalhes: " + mensagem);
        }
    }
}
