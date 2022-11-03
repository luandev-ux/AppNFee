using AppNFe.Api.Controllers.Base;
using AppNFe.Core.DominioProblema;
using AppNFe.Core.Utilitarios;
using AppNFe.Dominio.DTO.Integracoes.Jobs;
using AppNFe.Dominio.Entidades.Pessoas;
using AppNFe.Persistencia.Interfaces.Repositorios;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Pipelines.Sockets.Unofficial.Arenas;
using Serilog;
using System;
using System.Threading.Tasks;

namespace API.Controllers
{
    [ApiController, Route("v1/{contratante}/pessoa")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(RetornoRequisicao), 403)]
    [ProducesResponseType(typeof(RetornoRequisicao), 500)]
    public class PessoaController : BaseController
    {
        private IPessoaRepositorio PessoaRepositorio;

        public PessoaController(IConfiguration configuracao,
                                  IPessoaRepositorio pessoaRepositorio,
                                  ILogger logger)
        : base(configuracao, logger)
        {
            PessoaRepositorio = pessoaRepositorio;
            Logger = logger;
            IdentificadorPermissao = "PER_CADASTRO_USUARIOS";
            IdentificadorRecurso = "CADASTRO_USUARIOS";
        }

        #region Inclusão novo usuário
        /// <summary>
        /// Incluir novo usuário.
        /// </summary>
        /// <param name="pessoa">Objeto - Pessoa</param>
        /// <response code="200">Usuário cadastrado com sucesso.</response>
        /// <response code="203">Informações inválidas.</response>
        /// <response code="403">Usuário não possui permissão para executar essa operação.</response>
        /// <response code="500">Desculpe-nos ocorreu um erro ao cadastrar o usuário.</response>
        [HttpPost]
        [Route("incluir")]
        [ProducesResponseType(typeof(Pessoa), 200)]
        [ProducesResponseType(typeof(RetornoRequisicao), 203)]

        public async Task<IActionResult> InserirAsync(Pessoa pessoa)
        {
            try
            {
                var retornoValidacao = await ValidarInformacoes(pessoa);
                if (!retornoValidacao.VerificarSucesso()) return RetornoRequisicaoInformacoesInvalidas(retornoValidacao);

                var retorno = await PessoaRepositorio.InserirAsync(pessoa);
                if (retorno.Status)
                    return Ok(UtilitarioRetornoRequisicao.GerarRetornoSucesso(retorno.CodigoRegistro, "Usuário cadastrado com sucesso."));
            }
            catch (Exception e)
            {
                GravarLogErro("PessoaController", "InserirAsync", e.Message);
            }
            return BadRequest(UtilitarioRetornoRequisicao.GerarRetornoErro("Erro ao cadastrar usuário."));
        }
        #endregion
        #region Alteração de usuário
        /// <summary>
        /// Alterar um Usuário
        /// </summary>
        /// <param name="pessoa">Objeto - Pessoa</param>
        /// <response code="200">Usuário cadastrado com sucesso.</response>
        /// <response code="203">Informações inválidas.</response>
        [HttpPost]
        [Route("alterar")]
        public async Task<IActionResult> AtualizarAsync(Pessoa pessoa)
        {
            var result = await PessoaRepositorio.AtualizarAsync(pessoa);
            return Ok(result);
        }
        #endregion
        #region Exclusão de usuário
        /// <summary>
        /// Excluir um Usuário
        /// </summary>
        /// <param name="pessoa">Objeto - Pessoa</param>
        /// <response code="200">Usuário cadastrado com sucesso.</response>
        /// <response code="203">Informações inválidas.</response>
        [HttpDelete]
        [Route("excluir")]
        [ProducesResponseType(typeof(Pessoa), 200)]
        [ProducesResponseType(typeof(RetornoRequisicao), 203)]
        public async Task<IActionResult> ExcluirAsync(long pessoa)
        {
            try
            {
                var retorno = await PessoaRepositorio.ExcluirAsync(pessoa);
                if (retorno.Status)
                    return Ok(UtilitarioRetornoRequisicao.GerarRetornoSucesso(retorno.CodigoRegistro, "Usuário excluído com sucesso."));
            }
            catch (Exception e)
            {
                GravarLogErro("PessoaController", "ExcluirAsync", e.Message);
            }
            return BadRequest(UtilitarioRetornoRequisicao.GerarRetornoErro("Erro ao excluir usuário."));
        }
        #endregion
    }
}