using Microsoft.AspNetCore.Http;
using AppNFe.Persistencia.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace AppNFe.Api.Middlewares
{
    public class ContratanteMappingMiddleware
    {
        private readonly RequestDelegate next;

        public ContratanteMappingMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            string[] urlParts = null;

            #if DEBUG
                        urlParts = httpContext.Request.Path.Value.Split(new char[] { '/' },
                                  StringSplitOptions.RemoveEmptyEntries);
            #else
                    urlParts = httpContext.Request.Host.Host.Split(new char[] { '.' },
                              StringSplitOptions.RemoveEmptyEntries);
            #endif

            if (urlParts != null && urlParts.Any())
            {                
                if (urlParts.Count() >= 2)
                {
                    if (urlParts[0] != "swagger" && urlParts[0] != "hub-comunicacao" && urlParts[1] != "utilitarios")
                    {
                        var gerenteConexao = (IGerenteConexao)httpContext.RequestServices.GetService(typeof(IGerenteConexao));
                        gerenteConexao.Contratante = urlParts[1]; // Obtem o identificador do contratante
                    }
                }                                
            }

            await this.next(httpContext);
        }
    }
}
