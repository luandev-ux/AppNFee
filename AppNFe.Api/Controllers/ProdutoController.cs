using AppNFe.Api.Controllers.Base;
using AppNFe.Core.DominioProblema;
using AppNFe.Core.Utilitarios;
using AppNFe.Dominio.DTO.Integracoes.Jobs;
using AppNFe.Dominio.Entidades.Pessoas;
using AppNFe.Persistencia.Interfaces.Repositorios;
using AppNFe.Persistencia.Repositorios;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Pipelines.Sockets.Unofficial.Arenas;
using Serilog;
using System;
using System.Threading.Tasks;
using AppNFe.Dominio.Entidades;

namespace AppNFe.Api.Controllers
{

    [ApiController, Route("v1/{contratante}/produto")]
    [Produces("application/json")]
    /// <param name="contratante">Insira o codigo do contratante</param>
    /// <response code="203">Informações inválidas.</response>
    /// <response code="500">Ocorreu um erro ao cadastrar o contratante.</response>
    [ProducesResponseType(typeof(RetornoRequisicao), 403)]
    [ProducesResponseType(typeof(RetornoRequisicao), 500)]
    [ProducesResponseType(typeof(RetornoRequisicao), 203)]
    [ProducesResponseType(typeof(RetornoRequisicao), 200)]
    [ProducesResponseType(typeof(RetornoRequisicao), 204)]
    [ProducesResponseType(typeof(RetornoRequisicao), 400)]
    [ProducesResponseType(typeof(RetornoRequisicao), 404)]
    [ProducesResponseType(typeof(RetornoRequisicao), 409)]
    [ProducesResponseType(typeof(RetornoRequisicao), 412)]
    [ProducesResponseType(typeof(RetornoRequisicao), 415)]
    public class ProdutoController : BaseController
    {
        private IProdutoRepositorio ProdutoRepositorio;

        public ProdutoController(IConfiguration configuracao,
                                  IProdutoRepositorio empresaRepositorio,
                                  ILogger logger)
        : base(configuracao, logger)
        {
            ProdutoRepositorio = empresaRepositorio;
            Logger = logger;
            IdentificadorPermissao = "PER_CADASTRO_USUARIOS";
            IdentificadorRecurso = "CADASTRO_USUARIOS";
        }
        #region Inclusão
        /// <summary>
        /// Incluir novo Produto.
        /// </summary>
        /// <param name="produto">Insira o codigo do produto</param>
        /// <response code="200">Usuário cadastrado com sucesso.</response>
        /// <response code="203">Informações inválidas.</response>
        /// <response code="500">Desculpe-nos ocorreu um erro ao cadastrar o usuário.</response>
        [HttpPost]
        [Route("incluir")]
        [ProducesResponseType(typeof(Produto), 200)]
        [ProducesResponseType(typeof(RetornoRequisicao), 203)]
        [ProducesResponseType(typeof(RetornoRequisicao), 403)]
        [ProducesResponseType(typeof(RetornoRequisicao), 500)]
        
        public async Task<IActionResult> InserirAsync(Produto produto)
        {
            try
            {
                var retornoValidacao = await ValidarInformacoes(produto);
                if (!retornoValidacao.VerificarSucesso()) return RetornoRequisicaoInformacoesInvalidas(retornoValidacao);

                var retorno = await ProdutoRepositorio.InserirAsync(produto);
                if (retorno.Status)
                    return Ok(UtilitarioRetornoRequisicao.GerarRetornoSucesso(retorno.CodigoRegistro, "Produto cadastrado com sucesso."));
            }
            catch (Exception e)
            {
                GravarLogErro("ProdutoController", "InserirAsync", e.Message);
            }
            return BadRequest(UtilitarioRetornoRequisicao.GerarRetornoErro("Erro ao cadastrar o Produto."));
        }
        #endregion
        #region Alteração
        /// <summary>
        /// Alterar um Usuário
        /// </summary>
        /// <param name="produto">Insira o codigo do produto</param>
        /// <response code="200">Usuário cadastrado com sucesso.</response>
        /// <response code="203">Informações inválidas.</response>
        /// <response code="403">Usuário não possui permissão para executar essa operação.</response>
        /// <response code="500">Desculpe-nos ocorreu um erro ao cadastrar o usuário.</response>
        [HttpPost]
        [Route("alterar")]
        [ProducesResponseType(typeof(Produto), 200)]
        [ProducesResponseType(typeof(RetornoRequisicao), 203)]
        [ProducesResponseType(typeof(RetornoRequisicao), 403)]
        [ProducesResponseType(typeof(RetornoRequisicao), 500)]

        public async Task<IActionResult> AtualizarAsync(Produto produto)
        {
            try
            {
                var retornoValidacao = await ValidarInformacoes(produto);
                if (!retornoValidacao.VerificarSucesso()) return RetornoRequisicaoInformacoesInvalidas(retornoValidacao);

                var retorno = await ProdutoRepositorio.AtualizarAsync(produto);
                if (retorno.Status)
                    return Ok(UtilitarioRetornoRequisicao.GerarRetornoSucesso(retorno.CodigoRegistro, "Usuário alterado com sucesso."));
            }
            catch (Exception e)
            {
                GravarLogErro("ProdutoController", "AtualizarAsync", e.Message);
            }
            return BadRequest(UtilitarioRetornoRequisicao.GerarRetornoErro("Erro ao alterar usuário cadastrado."));
        }
        #endregion
        #region Exclusão
        /// <summary>
        /// Excluir um Usuário
        /// </summary>
        /// <param name="id">Digite o codigo do Produto</param>
        /// <response code="200">Usuário cadastrado com sucesso.</response>
        /// <response code="203">Informações inválidas.</response>
        /// <response code="403">Usuário não possui permissão para executar essa operação.</response>
        /// <response code="500">Desculpe-nos ocorreu um erro ao cadastrar o usuário.</response>
        [HttpDelete]
        [Route("excluir")]
        [ProducesResponseType(typeof(Produto), 200)]
        [ProducesResponseType(typeof(RetornoRequisicao), 203)]
        [ProducesResponseType(typeof(RetornoRequisicao), 403)]
        [ProducesResponseType(typeof(RetornoRequisicao), 500)]

        public async Task<IActionResult> ExcluirAsync(long id)
        {
            try
            {
                var retorno = await ProdutoRepositorio.ExcluirAsync(id);
                if (retorno.Status)
                    return Ok(UtilitarioRetornoRequisicao.GerarRetornoSucesso(retorno.CodigoRegistro, "Usuário excluído com sucesso."));
            }
            catch (Exception e)
            {
                GravarLogErro("ProdutoController", "ExcluirAsync", e.Message);
            }
            return BadRequest(UtilitarioRetornoRequisicao.GerarRetornoErro("Erro ao excluir usuário cadastrado."));
        }
        #endregion
    }
}

