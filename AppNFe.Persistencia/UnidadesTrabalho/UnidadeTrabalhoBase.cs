using System;
using System.Transactions;
using Serilog;

namespace AppNFe.Persistencia.UnidadesTrabalho
{
    public abstract class UnidadeTrabalhoBase
    {
        public readonly ILogger logger;

        public UnidadeTrabalhoBase(ILogger logger)
        {
            try
            {                
                this.logger = logger;                
            }
            catch (Exception e)
            {
                GravarLogErro("UnidadeTrabalhoBase", "Construtor", e);
            }
        }

        public TransactionScope CriarTransacaoAsync()
        {
            return new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
        }

        public void GravarLogErro(string repositorio, string metodo, Exception e)
        {
            logger.Error("Erro: " + repositorio + " > Método: " + metodo + " Detalhes: " + e.Message);
        }
    }
}
