﻿using AppNFe.Api.Controllers.Base;
using AppNFe.Core.DominioProblema;
using AppNFe.Core.Utilitarios;
using AppNFe.Dominio.Entidades;
using AppNFe.Persistencia.Interfaces.Repositorios;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Serilog;
using System;
using System.Threading.Tasks;

namespace AppNFe.Api.Controllers
{

    [ApiController, Route("v1/{contratante}/usuario")]
    [Produces("application/json")]
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
    public class UsuarioController : BaseController
    {
        private IUsuarioRepositorio UsuarioRepositorio;

        public UsuarioController(IConfiguration configuracao,
                                  IUsuarioRepositorio usuarioRepositorio,
                                  ILogger logger)
        : base(configuracao, logger)
        {
            UsuarioRepositorio = usuarioRepositorio;
            Logger = logger;
            IdentificadorPermissao = "PER_CADASTRO_USUARIOS";
            IdentificadorRecurso = "CADASTRO_USUARIOS";
        }
        #region Inclusão novo usuário
        /// <summary>
        /// Incluir novo usuário.
        /// </summary>
        /// <param name="usuario">Objeto - Pessoa</param>
        /// <response code="200">Usuário cadastrado com sucesso.</response>
        /// <response code="203">Informações inválidas.</response>
        /// <response code="403">Usuário não possui permissão para executar essa operação.</response>
        /// <response code="500">Desculpe-nos ocorreu um erro ao cadastrar o usuário.</response>
        [HttpPost]
        [Route("incluir")]
        [ProducesResponseType(typeof(Usuario), 200)]
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
        public async Task<IActionResult> InserirAsync(Usuario usuario)
        {
            try
            {
                var retornoValidacao = await ValidarInformacoes(usuario);
                if (!retornoValidacao.VerificarSucesso()) return RetornoRequisicaoInformacoesInvalidas(retornoValidacao);

                var retorno = await UsuarioRepositorio.InserirAsync(usuario);
                if (retorno.Status)
                    return Ok(UtilitarioRetornoRequisicao.GerarRetornoSucesso(retorno.CodigoRegistro, "Usuário cadastrado com sucesso."));
            }
            catch (Exception e)
            {
                GravarLogErro("UsuarioController", "InserirAsync", e.Message);
            }
            return BadRequest(UtilitarioRetornoRequisicao.GerarRetornoErro("Erro ao cadastrar usuário."));
        }
        #endregion
        #region Alteração de usuário
        /// <summary>
        /// Alterar um Usuário
        /// </summary>
        /// <param name="usuario">Objeto - Usuario</param>
        /// <response code="200">Usuário cadastrado com sucesso.</response>
        /// <response code="203">Informações inválidas.</response>
        /// <response code="403">Usuário não possui permissão para executar essa operação.</response>
        /// <response code="500">Desculpe-nos ocorreu um erro ao cadastrar o usuário.</response>
        [HttpPost]
        [Route("alterar")]
        [ProducesResponseType(typeof(Usuario), 200)]
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
        public async Task<IActionResult> AtualizarAsync(Usuario usuario)
        {
            try
            {
                var retornoValidacao = await ValidarInformacoes(usuario);
                if (!retornoValidacao.VerificarSucesso()) return RetornoRequisicaoInformacoesInvalidas(retornoValidacao);

                var retorno = await UsuarioRepositorio.AtualizarAsync(usuario);
                if (retorno.Status)
                    return Ok(UtilitarioRetornoRequisicao.GerarRetornoSucesso(retorno.CodigoRegistro, "Usuário alterado com sucesso."));
            }
            catch (Exception e)
            {
                GravarLogErro("UsuarioController", "AtualizarAsync", e.Message);
            }
            return BadRequest(UtilitarioRetornoRequisicao.GerarRetornoErro("Erro ao alterar usuário cadastrado."));
        }
        #endregion
        #region Exclusão de usuário
        /// <summary>
        /// Excluir um Usuário
        /// </summary>
        /// <param name="id">Digite o ID do Usuario</param>
        /// <response code="200">Usuário cadastrado com sucesso.</response>
        /// <response code="203">Informações inválidas.</response>
        /// <response code="403">Usuário não possui permissão para executar essa operação.</response>
        /// <response code="500">Desculpe-nos ocorreu um erro ao cadastrar o usuário.</response>
        [HttpDelete]
        [Route("excluir")]
        [ProducesResponseType(typeof(Usuario), 200)]
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

        public async Task<IActionResult> ExcluirAsync(long id)
        {
            try
            {
                var retorno = await UsuarioRepositorio.ExcluirAsync(id);
                if (retorno.Status)
                    return Ok(UtilitarioRetornoRequisicao.GerarRetornoSucesso(retorno.CodigoRegistro, "Usuário excluído com sucesso."));
            }
            catch (Exception e)
            {
                GravarLogErro("UsuarioController", "ExcluirAsync", e.Message);
            }
            return BadRequest(UtilitarioRetornoRequisicao.GerarRetornoErro("Erro ao excluir usuário cadastrado."));
        }
        #endregion
    }
}