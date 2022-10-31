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

        /// <summary>
        /// Incluir novo usuário.
        /// </summary>
        /// <param name="pessoa">Objeto - Pessoa</param>
        /// <response code="200">Usuário cadastrado com sucesso.</response>
        /// <response code="203">Informações inválidas.</response>
        /// <response code="403">Usuário não possui permissão para executar essa operação.</response>
        /// <response code="500">Desculpe-nos ocorreu um erro ao cadastrar o usuário.</response>
        [HttpPost]
        [ProducesResponseType(typeof(Pessoa), 200)]
        [ProducesResponseType(typeof(Cliente), 200)]
        [ProducesResponseType(typeof(Fornecedor), 200)]
        [ProducesResponseType(typeof(RetornoRequisicao), 203)]

        public async Task<IActionResult> InserirAsync(Pessoa pessoa)
        {
            try
            {
                if (pessoa is Cliente)
                {
                    var cliente = pessoa as Cliente;
                    await PessoaRepositorio.InserirAsync(cliente);
                    return Ok(cliente);
                }
                else if (pessoa is Fornecedor)
                {
                    var fornecedor = pessoa as Fornecedor;
                    await PessoaRepositorio.InserirAsync(fornecedor);
                    return Ok(fornecedor);
                }
                else
                {
                    await PessoaRepositorio.InserirAsync(pessoa);
                    return Ok(pessoa);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Erro ao cadastrar usuário.");
                return StatusCode(500, new RetornoRequisicao(500, "Desculpe-nos ocorreu um erro ao cadastrar o usuário."));
            }
        }