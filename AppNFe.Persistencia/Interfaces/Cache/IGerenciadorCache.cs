using System.Threading.Tasks;

namespace AppNFe.Persistencia.Interfaces.Cache
{
    public interface IGerenciadorCache
    {
        Task<T> Obter<T>(string chave);
        Task<T> Salvar<T>(string chave, T valor);
        Task<bool> Excluir(string chave);
    }
}
