using AppNFe.Core.DominioProblema;
using AppNFe.Dominio.Entidades.Usuario;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppNFe.Persistencia.Interfaces.Repositorios
{
    public interface IRepositorioBase<TEntidade> : IDisposable where TEntidade : class
    {
        Retorno Inserir(TEntidade obj);
        Retorno Atualizar(TEntidade obj);
        Retorno Excluir(long codigo);
        TEntidade ObterPeloCodigo(long codigo);
        IEnumerable<TEntidade> ObterTodosRegistros();
        Task<TEntidade> ObterPeloCodigoAsync(long codigo);
        Task<TEntidade> ObterPeloCodigoLazyAsync(long codigo);
        Task<IEnumerable<TEntidade>> ObterTodosRegistrosAsync();
        Task<Retorno> InserirAsync(TEntidade obj, UsuarioRegistroAtividade registroAtividade = null);
        Task<Retorno> AtualizarAsync(TEntidade obj, UsuarioRegistroAtividade registroAtividade = null);
        Task<Retorno> ExcluirAsync(long codigo, UsuarioRegistroAtividade registroAtividade = null);
        Task<Retorno> InserirRegistroAtividadeAsync(UsuarioRegistroAtividade registroAtividade);
        Task<Retorno> ValidarEntidadesReferenciadas(TEntidade objeto);
    }
}
