using AppNFe.Dominio.Entidades.Movimentos;
using AppNFe.Dominio.Entidades.Pessoas;

namespace AppNFe.Persistencia.Interfaces.Repositorios
{
    public interface IMovimentoRepositorio : IRepositorioBase<Movimento> { }
    public interface IPessoaRepositorios : IRepositorioBase<Pessoa> { }
}
