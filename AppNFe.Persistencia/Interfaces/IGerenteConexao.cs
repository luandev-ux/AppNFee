using System.Data;

namespace AppNFe.Persistencia.Interfaces
{
    public interface IGerenteConexao
    {
        string Contratante { get; set; }

        IDbConnection CriarConexao();
    }
}
