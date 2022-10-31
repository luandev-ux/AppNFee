using AppNFe.Api.Controllers.Base;
using AppNFe.Core.DominioProblema;
using AppNFe.Core.Utilitarios;
using AppNFe.Dominio.Entidades.Empresas;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using System;
using Serilog;
using AppNFe.Persistencia.Interfaces.Repositorios;

namespace AppNFe.Api.Controllers.EmpresasController
{
    [ApiController, Route("v1/{contratante}/empresa")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(RetornoRequisicao), 403)]
    [ProducesResponseType(typeof(RetornoRequisicao), 500)]
    public class EmpresaController : BaseController
    {
        private IEmpresaRepositorio EmpresaRepositorio;

        public EmpresaController(IConfiguration configuracao,
                                  IEmpresaRepositorio usuarioRepositorio,
                                  ILogger logger)
        : base(configuracao, logger)
        {
            EmpresaRepositorio = usuarioRepositorio;
            Logger = logger;
            IdentificadorPermissao = "PER_CADASTRO_USUARIOS";
            IdentificadorRecurso = "CADASTRO_USUARIOS";
        }

        /// <summary>
        /// Incluir novo usuário.
        /// </summary>
        /// <param name="empresa">Objeto - Empresa</param>
        /// <response code="200">Usuário cadastrado com sucesso.</response>
        /// <response code="203">Informações inválidas.</response>
        /// <response code="403">Usuário não possui permissão para executar essa operação.</response>
        /// <response code="500">Desculpe-nos ocorreu um erro ao cadastrar o usuário.</response>
        [HttpPost]
        [ProducesResponseType(typeof(Empresa), 200)]
        [ProducesResponseType(typeof(RetornoRequisicao), 203)]
        public async Task<IActionResult> Incluir([FromBody] Empresa empresa)
        {
            try
            {
                var retornoValidacao = await ValidarInformacoes(empresa);
                if (!retornoValidacao.VerificarSucesso()) return RetornoRequisicaoInformacoesInvalidas(retornoValidacao);

                var retorno = await EmpresaRepositorio.InserirAsync(empresa);
                if (retorno.Status)
                    return Ok(UtilitarioRetornoRequisicao.GerarRetornoSucesso(retorno.CodigoRegistro, "Usuário cadastrado com sucesso."));

            }
            catch (Exception e)
            {
                GravarLogErro("EmpresaController", "Incluir", e.Message);
            }
            return RetornoRequisicaoErro("cadastrar o usuário");
        }

        public override ObjectResult RetornoRequisicaoNaoEncontrado()
        {
            return StatusCode(404, UtilitarioRetornoRequisicao.GerarRetornoAlerta("Usuário não encontrado com código o informado."));
        }
    }
}
