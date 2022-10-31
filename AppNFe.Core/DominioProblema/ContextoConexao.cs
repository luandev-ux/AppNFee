using System.Data.Common;

namespace NeoXBI.Core.DominioProblema
{
    public abstract class ContextoConexao
    {
        public static DbConnection Conexao { get; set; }
    }
}
