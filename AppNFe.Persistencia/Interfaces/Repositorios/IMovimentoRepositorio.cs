using AppNFe.Dominio.Entidades.Pessoas;

namespace AppNFe.Persistencia.Interfaces.Repositorios
{
    public interface IMovimentoRepositorio : IRepositorioBase<Movimento> { }
    public interface IPessoaRepositorios : IRepositorioBase<Pessoa> { }
    public interface IClientesRepositorio : IRepositorioBase<Cliente> { }
    public interface IFornecedoresRepositorio : IRepositorioBase<Fornecedor> { }
}
