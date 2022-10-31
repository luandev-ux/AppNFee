using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace AppNFe.Api.Seguranca.Attributes
{
    public class TokenIntegracaoAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            string tokenIntegracaoServidor = "1AN6N94fxyUgFwrLvEDxWRiCjPWkBRAhT4";
            string tokenIntegracaoRequisicao = context.HttpContext.Request.Headers["TokenIntegracao"].ToString();
            if (string.IsNullOrEmpty(tokenIntegracaoRequisicao) || tokenIntegracaoRequisicao != tokenIntegracaoServidor)
            {
                context.Result = new UnauthorizedResult();
                return;
            }                                                                
        }
    }
}
