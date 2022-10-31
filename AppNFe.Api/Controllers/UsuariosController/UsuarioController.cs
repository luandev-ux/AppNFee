using AppNFe.Api.Controllers.Base;
using AppNFe.Core.DominioProblema;
using AppNFe.Core.Utilitarios;
using AppNFe.Dominio.DTO.Integracoes.Jobs;
using AppNFe.Dominio.Entidades.Pessoas;
using AppNFe.Dominio.Entidades.Usuario;
using AppNFe.Persistencia.Interfaces.Repositorios;
using AppNFe.Persistencia.Repositorios.UsuarioRepositorio;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Pipelines.Sockets.Unofficial.Arenas;
using Serilog;
using System;
using System.Threading.Tasks;

namespace API.Controllers
{
    [ApiController, Route("v1/{contratante}/usuario")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(RetornoRequisicao), 403)]
    [ProducesResponseType(typeof(RetornoRequisicao), 500)]
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

        /// <summary>
        /// Incluir novo usuário.
        /// </summary>
        /// <param name="usuario">Objeto - Pessoa</param>
        /// <response code="200">Usuário cadastrado com sucesso.</response>
        /// <response code="203">Informações inválidas.</response>
        /// <response code="403">Usuário não possui permissão para executar essa operação.</response>
        /// <response code="500">Desculpe-nos ocorreu um erro ao cadastrar o usuário.</response>
        [HttpPost]
        [ProducesResponseType(typeof(Usuario), 200)]
        [ProducesResponseType(typeof(RetornoRequisicao), 203)]

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

        /// <summary>
        /// Excluir um Usuário
        /// </summary>
        /// <param name="usuario">Objeto - Usuario</param>
        /// <response code="200">Usuário cadastrado com sucesso.</response>
        /// <response code="203">Informações inválidas.</response>
        /// <response code="403">Usuário não possui permissão para executar essa operação.</response>
        /// <response code="500">Desculpe-nos ocorreu um erro ao cadastrar o usuário.</response>
        [HttpPut]
        [ProducesResponseType(typeof(Usuario), 200)]
        [ProducesResponseType(typeof(RetornoRequisicao), 203)]
        public async Task<IActionResult> ExcluirAsync(long usuario)
        {
            try
            {
                var retorno = await UsuarioRepositorio.ExcluirAsync(usuario);
                if (retorno.Status)
                    return Ok(UtilitarioRetornoRequisicao.GerarRetornoSucesso(retorno.CodigoRegistro, "Usuário excluído com sucesso."));
            }
            catch (Exception e)
            {
                GravarLogErro("UsuarioController", "ExcluirAsync", e.Message);
            }
            return BadRequest(UtilitarioRetornoRequisicao.GerarRetornoErro("Erro ao excluir usuário."));
        }
    }
}
        
        