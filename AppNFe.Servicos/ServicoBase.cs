using Serilog;
using System;

namespace AppNFe.Servicos
{
    public abstract class ServicoBase
    {
        protected ILogger Logger;

        public ServicoBase(ILogger logger)
        {
            Logger = logger;
        }

        public void GravarLogErro(string servico, string metodo, Exception e)
        {
            Logger.Error("Erro: " + servico + " > Método: " + metodo + " Detalhes: " + e.Message);
        }
        public void GravarLogErro(string servico, string metodo, string mensagem)
        {
            Logger.Error("Erro: " + servico + " > Método: " + metodo + " Detalhes: " + mensagem);
        }
    }
}
