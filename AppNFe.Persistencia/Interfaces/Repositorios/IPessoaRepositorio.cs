using AppNFe.Dominio.Entidades.Pessoas;
using System.Threading.Tasks;

namespace AppNFe.Persistencia.Interfaces.Repositorios
{
    public interface IPessoaRepositorio : IRepositorioBase<Pessoa>
    {
       
    }
    public interface IClienteRepositorio : IRepositorioBase<Cliente>
    {
    }
    public interface IFornecedorRepositorio : IRepositorioBase<Fornecedor>
    {
    }
}