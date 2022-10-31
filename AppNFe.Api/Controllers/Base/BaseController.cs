using AppNFe.Dominio.Entidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using AppNFe.Core.DominioProblema;
using AppNFe.Core.Enumeradores;
using AppNFe.Core.Persistencia.Consulta;
using AppNFe.Core.Utilitarios;
using AppNFe.Dominio.Consulta;
using AppNFe.Dominio.Relatorios;
using Serilog;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;

namespace AppNFe.Api.Controllers.Base
{
    public abstract class BaseController : Controller
    {
        protected IConfiguration Configuracao;
        protected ILogger Logger;        
        protected string IdentificadorPermissao;
        protected string IdentificadorRecurso;

        public BaseController(IConfiguration configuracao, ILogger logger)
        {
            Logger = logger;
            Configuracao = configuracao;           
        }

        [NonAction]
        protected void PreencherCabecalhoPaginacao(ListaPaginadaMetaData metaDataLista)
        {
            try
            {
                var metadata = new
                {
                    totalRegistros = metaDataLista.TotalRegistros,
                    qtdeRegistrosPagina = metaDataLista.QtdeRegistrosPagina,
                    paginaAtual = metaDataLista.PaginaAtual,
                    totalPaginas = metaDataLista.TotalPaginas,
                    possuiProximaPagina = metaDataLista.PossuiProximaPagina,
                    possuiPaginaAnterior = metaDataLista.PossuiPaginaAnterior
                };

                Response.Headers.Add("X-Paginacao", JsonSerializer.Serialize(metadata));
            }
            catch (Exception e)
            {
                GravarLogErro("BaseController", "PreencherCabecalhoPaginacao", e.Message);
            }
        }

        [NonAction]
        protected string ObterContratanteRota()
        {
            string contratante = "";
            try
            {
                contratante = HttpContext.Request.RouteValues["contratante"].ToString();
            }
            catch (Exception e)
            {
                GravarLogErro("BaseController", "ObterContratante", e.Message);
            }

            return contratante;
        }

        [NonAction]
        protected string ObterUrlBaseApi()
        {
            string urlBase = "";
            try
            {
                urlBase = $"https://{HttpContext.Request.Host}{HttpContext.Request.PathBase}";
            }
            catch (Exception e)
            {
                GravarLogErro("BaseController", "ObterUrlBaseApi", e.Message);
            }

            return urlBase;
        }

        [NonAction]
        protected string ObterContratanteAutenticado()
        {
            string contratante = "";
            try
            {
                contratante = User.FindFirstValue(ClaimTypes.GroupSid);
            }
            catch (Exception e)
            {
                GravarLogErro("BaseController", "ObterContratanteAutenticado", e.Message);
            }

            return contratante;
        }

        [NonAction]
        protected long ObterCodigoUsuario()
        {
            return long.Parse(User.FindFirstValue(ClaimTypes.PrimarySid));
        }

        [NonAction]
        protected ParametrosConsulta OtimizaParametrosConsultaRelatorio(ParametrosConsulta parametrosConsulta, List<long> codigoSelecionados)
        {
            parametrosConsulta.CodigosSelecionados = codigoSelecionados; // Preenche os códigos selecionados pelo usuário
            parametrosConsulta.QtdeRegistrosPagina = 0; // O relatório passa controlar a paginação
            parametrosConsulta.QtdeRegistrosTotal = 0; // Permite o relatório exibir todos os registros no banco de dados

            return parametrosConsulta;
        }

        [NonAction]
        protected async Task<ConfiguracaoRelatorio> MontaConfiguracaoRelatorio(long empresa, string titulo, ETipoArquivo tipoArquivo)
        {
            return await MontaConfiguracaoRelatorio(new List<long> {empresa}, titulo,tipoArquivo);
        }
        protected async Task<ConfiguracaoRelatorio> MontaConfiguracaoRelatorio(List<long> empresas, string titulo, ETipoArquivo tipoArquivo)
        {
            ConfiguracaoRelatorio configuracaoRelatorio = new ConfiguracaoRelatorio();
            try
            {
                /*var dadosEmpresas = await ServicoSistema.ObterDadosEmpresasCache(empresas);
                configuracaoRelatorio.VariasEmpresas = dadosEmpresas.Count() > 0;
                configuracaoRelatorio.Empresa = ServicoSistema.ObtemDadosExibicaoEmpresaRelatorio(dadosEmpresas);
                configuracaoRelatorio.Titulo = titulo;
                configuracaoRelatorio.TipoArquivo = tipoArquivo;
                var gerenciadorArquivo = new GerenciadorArquivo();
                configuracaoRelatorio.NomeArquivo = gerenciadorArquivo.CriaNomeUnicoDeArquivoPorUsuario(titulo, ObterCodigoUsuario());
                configuracaoRelatorio.ExtensaoArquivo = gerenciadorArquivo.ObtemExtensaoTipoArquivo(tipoArquivo);
                configuracaoRelatorio.DiretorioContratante = gerenciadorArquivo.MontaDiretorioTemporarioContratante(ObterContratanteAutenticado(), ERelacaoArquivo.Relatorios);
                configuracaoRelatorio.DiretorioServidor = gerenciadorArquivo.ObtemDiretorioServidor(configuracaoRelatorio.DiretorioContratante);
                configuracaoRelatorio.DiretorioServidorRelatorios = gerenciadorArquivo.ObtemDiretorioServidor(gerenciadorArquivo.ObtemNomeDiretorioEspecifico(ERelacaoArquivo.Relatorios));
                configuracaoRelatorio.UrlBaseApi = ObterUrlBaseApi();  */              
            }
            catch (Exception e)
            {
                GravarLogErro("BaseController", "MontaConfiguracaoRelatorio", e.Message);
            }
            return configuracaoRelatorio;
        }        

        [NonAction]
        protected virtual Task<RetornoRequisicao> ValidarInformacoes(object model)
        {
            if (model == null)
            {
                return Task.FromResult(UtilitarioRetornoRequisicao.GerarRetornoAlerta("Dados inválidos ou não preenchidos!"));
            }

            TryValidateModel(model);

            string mensagem = "<h3>As seguintes informações não foram preenchidas corretamente:</h3>";

            var erros = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);

            DisplayAttribute anotacaoDisplay = model.GetType().GetCustomAttributes(typeof(DisplayAttribute), true).Cast<DisplayAttribute>().SingleOrDefault();
            if (anotacaoDisplay != null)
                mensagem = "<h3>As informações referente: <b>" + anotacaoDisplay.Name + "</b> <br>Não foram preenchidas corretamente.</h3>";

            if (erros.Count() > 0)
            {
                erros = erros.Distinct();
                foreach (var erro in erros)
                {
                    mensagem += "<br/>" + erro;
                }
            }


            if (erros.Count() == 0)
                return Task.FromResult(UtilitarioRetornoRequisicao.GerarRetornoSucesso());
            else
                return Task.FromResult(UtilitarioRetornoRequisicao.GerarRetornoAlerta(mensagem));
        }

        [NonAction]
        protected ObjectResult RetornoRequisicaoInformacoesInvalidas(RetornoRequisicao retornoRequisicao)
        {
            return StatusCode(203, retornoRequisicao);
        }

        [NonAction]
        protected ObjectResult RetornoRequisicaoInformacoesInvalidas(Retorno retorno)
        {
            return RetornoRequisicaoInformacoesInvalidas(new RetornoRequisicao(retorno));
        }

        [NonAction]
        protected ObjectResult RetornoRequisicaoSemPermissao(RetornoRequisicao retornoRequisicao)
        {
            return StatusCode(403, retornoRequisicao);
        }

        [NonAction]
        protected ObjectResult RetornoRequisicaoSemPermissao(Retorno retorno)
        {
            return RetornoRequisicaoSemPermissao(new RetornoRequisicao(retorno));
        }

        [NonAction]
        protected ObjectResult RetornoArquivoSemPermissao(RetornoArquivo retornoArquivo)
        {
            return StatusCode(403, retornoArquivo);
        }

        [NonAction]
        public virtual ObjectResult RetornoRequisicaoNaoEncontrado(string mensagem)
        {
            return StatusCode(404, UtilitarioRetornoRequisicao.GerarRetornoAlerta(mensagem));
        }

        [NonAction]
        public virtual ObjectResult RetornoRequisicaoNaoEncontrado()
        {
            return RetornoRequisicaoNaoEncontrado("Registro não encontrado com código o informado.");
        }

        [NonAction]
        protected ObjectResult RetornoRequisicaoErro(string acao)
        {
            return StatusCode(500, UtilitarioRetornoRequisicao.GerarRetornoAlerta("Desculpe-nos ocorreu um erro ao " + acao + " ."));
        }

        [NonAction]
        protected ObjectResult RetornoArquivoErro(string acao)
        {
            RetornoArquivo retornoArquivo = new RetornoArquivo();
            retornoArquivo.Status = EStatusRetornoRequisicao.Alerta;
            retornoArquivo.Mensagem = "Desculpe-nos ocorreu um erro ao " + acao + " .";
            return StatusCode(500, retornoArquivo);
        }

        [NonAction]
        protected void GravarLogErro(string controlador, string metodo, string detalhes)
        {
            Logger.Error("Erro: " + controlador + " > Método: " + metodo + " Detalhes: " + detalhes);
        }

        [NonAction]
        protected ObjectResult RetornoRelatorioErro(string mensagem)
        {
            return Ok(new RetornoRelatorio("", EStatusRetornoRequisicao.Erro, mensagem));
        }

        [NonAction]
        protected UsuariosRegistroAtividade RegistrarAtividadeUsuario(List<long> empresas, string detalhe)
        {
            UsuariosRegistroAtividade registroAtividade = new(empresas, ObterCodigoUsuario(), IdentificadorRecurso, detalhe);
            return registroAtividade;
        }

        [NonAction]
        protected UsuariosRegistroAtividade RegistrarAtividadeUsuario(long empresa, string detalhe)
        {
            UsuariosRegistroAtividade registroAtividade = new(empresa, ObterCodigoUsuario(), IdentificadorRecurso, detalhe);
            return registroAtividade;
        }

        [NonAction]
        protected UsuariosRegistroAtividade RegistrarAtividadeUsuario(long empresa, string identificadorRecurso, string detalhe)
        {
            UsuariosRegistroAtividade registroAtividade = new(empresa, ObterCodigoUsuario(), identificadorRecurso, detalhe);
            return registroAtividade;
        }              
    }
}
