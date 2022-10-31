using Microsoft.Extensions.Caching.Distributed;
using AppNFe.Persistencia.Interfaces.Cache;
using Serilog;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace AppNFe.Persistencia.Cache
{
    public class GerenciadorCache : IGerenciadorCache
    {
        private readonly IDistributedCache CacheDistribuido;
        private readonly ILogger Logger;

        public GerenciadorCache(IDistributedCache cacheDistribuido, ILogger logger)
        {
            CacheDistribuido = cacheDistribuido;
            Logger = logger;
        }

        public async Task<T> Obter<T>(string chave)
        {
            try
            {                
                var valor = await CacheDistribuido.GetStringAsync(chave);

                if (valor != null)
                {
                    return JsonSerializer.Deserialize<T>(valor);
                }
            }
            catch (Exception e)
            {
                GravarLogErro("Obter", e);
                return default;
            }

            return default;
        }
        /// <summary>
        /// Salva cache com tempo padrão de 1 hora para expirar e 10 minutos de inatividade
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="chave"></param>
        /// <param name="valor"></param>
        /// <returns></returns>
        public async Task<T> Salvar<T>(string chave, T valor)
        {
            try
            {
                await Salvar(chave, valor, TimeSpan.FromHours(1), TimeSpan.FromMinutes(10));                
            }
            catch (Exception e)
            {
                GravarLogErro("Salvar", e);
                return default;
            }

            return valor;
        }

        /// <summary>
        /// Salva cache com tempo personalizado de expiração e inatividade
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="chave"></param>
        /// <param name="valor"></param>
        /// <returns></returns>
        public async Task<T> Salvar<T>(string chave, T valor, TimeSpan expiracao, TimeSpan inatividade)
        {
            try
            {
                var opcoesCacheDistribuido = new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = expiracao,
                    SlidingExpiration = inatividade
                };

                await CacheDistribuido.SetStringAsync(chave, JsonSerializer.Serialize(valor), opcoesCacheDistribuido);
            }
            catch (Exception e)
            {
                GravarLogErro("Salvar", e);
                return default;
            }

            return valor;
        }

        public async Task<bool> Excluir(string chave)
        {
            try
            {
                await CacheDistribuido.RemoveAsync(chave);
            }
            catch (Exception e)
            {
                GravarLogErro("Excluir", e);
                return false;
            }

            return true;
        }

        private void GravarLogErro(string acao, Exception e)
        {
            Logger.Error("Erro: GerenciadorCache > Método: " + acao + " Detalhes: " + e.Message);
        }
    }
}
