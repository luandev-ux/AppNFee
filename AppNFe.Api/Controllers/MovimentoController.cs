using AppNFe.Api.Controllers.Base;
using AppNFe.Core.DominioProblema;
using AppNFe.Core.Utilitarios;
using AppNFe.Dominio.DTO.Integracoes.Jobs;
using AppNFe.Dominio.Entidades.Movimentos;
using AppNFe.Persistencia.Interfaces.Repositorios;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Pipelines.Sockets.Unofficial.Arenas;
using Serilog;
using System;
using System.Threading.Tasks;

namespace API.Controllers
{
    [ApiController, Route("v1/{contratante}/movimento")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(RetornoRequisicao), 403)]
    [ProducesResponseType(typeof(RetornoRequisicao), 500)]
    public class MovimentoController : BaseController
    {
        private IMovimentoRepositorio MovimentoRepositorio;

        public MovimentoController(IConfiguration configuracao,
                                  IMovimentoRepositorio movimentoRepositorio,
                                  ILogger logger)
        : base(configuracao, logger)
        {
            MovimentoRepositorio = movimentoRepositorio;
            Logger = logger;
            IdentificadorPermissao = "PER_CADASTRO_USUARIOS";
            IdentificadorRecurso = "CADASTRO_USUARIOS";
        }

        #region Inclusão novo Movimento
        /// <summary>
        /// Incluir novo Movimento
        /// </summary>
        /// <param name="movimento">Objeto - Movimento</param>
        /// <response code="200">Usuário cadastrado com sucesso.</response>
        /// <response code="203">Informações inválidas.</response>
        [HttpPost]
        [Route("incluir")]
        [ProducesResponseType(typeof(Movimento), 200)]
        [ProducesResponseType(typeof(RetornoRequisicao), 203)]

        public async Task<IActionResult> InserirAsync(Movimento movimento)
        {
            try
            {
                var retornoValidacao = await ValidarInformacoes(movimento);
                if (!retornoValidacao.VerificarSucesso()) return RetornoRequisicaoInformacoesInvalidas(retornoValidacao);

                var retorno = await MovimentoRepositorio.InserirAsync(movimento);
                if (retorno.Status)
                    return Ok(UtilitarioRetornoRequisicao.GerarRetornoSucesso(retorno.CodigoRegistro, "Usuário cadastrado com sucesso."));
            }
            catch (Exception e)
            {
                GravarLogErro("MovimentoController", "InserirAsync", e.Message);
            }
            return BadRequest(UtilitarioRetornoRequisicao.GerarRetornoErro("Erro ao cadastrar usuário."));
        }
        #endregion
        #region Alteração de Movimento
        /// <summary>
        /// Alterar um Movimento
        /// </summary>
        /// <param name="movimento">Objeto - Movimento</param>
        /// <response code="200">Usuário cadastrado com sucesso.</response>
        /// <response code="203">Informações inválidas.</response>
        [HttpPost]
        [Route("alterar")]
        public async Task<IActionResult> AtualizarAsync(Movimento movimento)
        {
            var result = await MovimentoRepositorio.AtualizarAsync(movimento);
            return Ok(result);
        }
        #endregion
        #region Exclusão de Movimento
        /// <summary>
        /// Excluir um Movimento
        /// </summary>
        /// <param name="movimento">Objeto - Movimento</param>
        /// <response code="200">Usuário cadastrado com sucesso.</response>
        /// <response code="203">Informações inválidas.</response>
        [HttpDelete]
        [Route("excluir")]
        [ProducesResponseType(typeof(Movimento), 200)]
        [ProducesResponseType(typeof(RetornoRequisicao), 203)]
        public async Task<IActionResult> ExcluirAsync(long movimento)
        {
            try
            {
                var retorno = await MovimentoRepositorio.ExcluirAsync(movimento);
                if (retorno.Status)
                    return Ok(UtilitarioRetornoRequisicao.GerarRetornoSucesso(retorno.CodigoRegistro, "Usuário excluído com sucesso."));
            }
            catch (Exception e)
            {
                GravarLogErro("MovimentoController", "ExcluirAsync", e.Message);
            }
            return BadRequest(UtilitarioRetornoRequisicao.GerarRetornoErro("Erro ao excluir usuário."));
        }
        #endregion
    }
}