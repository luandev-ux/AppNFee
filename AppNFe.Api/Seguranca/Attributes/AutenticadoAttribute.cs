using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace AppNFe.Api.Seguranca.Attributes
{
    /// <summary>
    /// Verifica se o usuário está autenticado e com os dados do contratante e Token válidos.
    /// </summary>
    public class AutenticadoAttribute : AuthorizeAttribute, IAuthorizationFilter
    {
        readonly string[] _requiredClaims;

        public AutenticadoAttribute(params string[] claims)
        {
            _requiredClaims = claims;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            bool autenticado = context.HttpContext.User.Identity.IsAuthenticated;
            if (!autenticado)
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            string contratanteRequisicao = context.HttpContext.Request.RouteValues["contratante"].ToString();
            string contratanteAutenticado = context.HttpContext.User.FindFirstValue(ClaimTypes.GroupSid);

            if (string.IsNullOrEmpty(contratanteRequisicao) || (string.IsNullOrEmpty(contratanteAutenticado)))
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            if (contratanteRequisicao != contratanteAutenticado)
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            // Verifica se o token e dados da autenticação ainda são válidos
            string statusAutenticacao = context.HttpContext.User.FindFirstValue(ClaimTypes.Authentication);
            if (string.IsNullOrEmpty(statusAutenticacao))
            {
                context.Result = new UnauthorizedResult();
                return;
            }            
        }
    }
}