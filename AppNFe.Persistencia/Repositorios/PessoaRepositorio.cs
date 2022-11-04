using Dapper;
using Microsoft.Extensions.Caching.Distributed;
using AppNFe.Core.Persistencia;
using AppNFe.Core.Persistencia.Consulta;
using AppNFe.Dominio.Consulta;
using AppNFe.Persistencia.Cache;
using AppNFe.Persistencia.Interfaces;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dommel;
using AppNFe.Core.DominioProblema;
using System.Text;
using AppNFe.Dominio.DTO.Usuarios;
using AppNFe.Persistencia.Interfaces.Repositorios;
using AppNFe.Dominio.Entidades.Pessoas;
using AppNFe.Dominio.Entidades;
using Microsoft.Extensions.Configuration;
using System.Collections;
using System.Configuration;

namespace AppNFe.Persistencia.Repositorios
{
    public class PessoaRepositorio : RepositorioBase<Pessoa>, IPessoaRepositorio
    {
        public PessoaRepositorio(IGerenteConexao gerenteConexao, ILogger logger) : base(gerenteConexao, logger) { }

        public async Task<ListaPaginada<Usuario>> ObterUsuarios(ParametrosConsulta parametrosConsulta, List<FiltroGenerico> filtros)
        {
            IEnumerable<Usuario> listaUsuarios = new List<Usuario>();
            try
            {
                string filtroEmpresa = "";
                string filtrosSQL = "";
                string agruparPor = " TU.pk_usuario,TU.nome,TU.login,TU.senha,TU.email,TU.imagem,TU.ativo ";

                filtroEmpresa = " WHERE TUE.fk_empresa IN (" + string.Join(",", parametrosConsulta.Empresas) + ") ";

                if (parametrosConsulta.CodigosSelecionados != null && parametrosConsulta.CodigosSelecionados.Count() > 0)
                {
                    filtrosSQL = " AND TU.pk_usuario IN (" + string.Join(",", parametrosConsulta.CodigosSelecionados.Select(c => c)) + ") GROUP BY " + agruparPor;
                }

                var sql = new StringBuilder();
                sql.Append(" SELECT TU.* ");
                sql.Append(" FROM tb_usuario TU ");
                sql.Append(" INNER JOIN tb_usuario_empresa TUE ON TUE.fk_usuario = TU.pk_usuario ");
                sql.Append(" " + filtroEmpresa + filtrosSQL + " ");

                listaUsuarios = await conexaoDB.QueryAsync<Usuario>(sql.ToString());

            }
            catch (Exception e)
            {
                GravarLogErro("UsuarioRepositorio", "ObterUsuarios", e);
            }

            return ListaPaginada<Usuario>.ToListaPaginada(listaUsuarios,
                      parametrosConsulta.NumeroPagina,
                      parametrosConsulta.QtdeRegistrosPagina);
        }

        public async Task<IEnumerable<ItemConsultaRapida>> ConsultaRapida(ParametrosConsultaRapida parametrosConsultaRapida, bool apresentarCodigo)
        {
            IEnumerable<ItemConsultaRapida> listaItens = new List<ItemConsultaRapida>();
            try
            {
                bool filtrarEmpresas = parametrosConsultaRapida.Empresas != null && parametrosConsultaRapida.Empresas.Count > 0;

                EstruturaConsultaRapida estruturaConsultaRapida = new EstruturaConsultaRapida();
                estruturaConsultaRapida.TabelaDB = filtrarEmpresas ? "tb_usuario TU INNER JOIN tb_usuario_empresa TUE ON TUE.fk_usuario = TU.pk_usuario " : " tb_usuario TU ";
                estruturaConsultaRapida.ColunaCodigoDB = "TU.pk_usuario";
                estruturaConsultaRapida.ColunaTextoIdentificacaoDB = apresentarCodigo ? "TU.pk_usuario ||' - '|| TU.nome" : "TU.nome";
                estruturaConsultaRapida.CondicaoApenasAtivos = " TU.ativo = true ";

                if (filtrarEmpresas)
                    estruturaConsultaRapida.CondicaoAdicional = " AND TUE.fk_empresa IN (" + string.Join(",", parametrosConsultaRapida.Empresas) + ") ";

                listaItens = await conexaoDB.QueryAsync<ItemConsultaRapida>(MontaConsultaRapidaSQL(estruturaConsultaRapida, parametrosConsultaRapida));
            }
            catch (Exception e)
            {
                GravarLogErro("UsuarioRepositorio", "ConsultaRapida", e);
            }
            return listaItens;
        }

        public override async Task<Retorno> InserirAsync(Pessoa pessoa, UsuariosRegistroAtividade registroAtividade)
        {            
            try
            {
                using (var transacao = CriarTransacaoAsync())
                {
                    Retorno retorno = await base.InserirAsync(pessoa, registroAtividade);
                    if (retorno.Status)
                    {
                        foreach (var cliente in pessoa.Clientes)
                        {
                            cliente.CodigoPessoa = retorno.CodigoRegistro;
                            long codigoCliente = (long)await conexaoDB.InsertAsync(cliente);
                            if (codigoCliente <=0)
                                return new Retorno(false, "Não foi possível salvar as informações de cliente");
                        }

                        foreach (var fornecedor in pessoa.Fornecedores)
                        {
                            fornecedor.CodigoPessoa = retorno.CodigoRegistro;
                            long codigoFornecedor = (long)await conexaoDB.InsertAsync(fornecedor);
                            if (codigoFornecedor <= 0)
                                return new Retorno(false, "Não foi possível salvar as informações de fornecedor");
                        }
                        
                        foreach (var movimento in pessoa.Movimentos)
                        {
                            movimento.CodigoPessoa = retorno.CodigoRegistro;
                            long codigoMovimento = (long)await conexaoDB.InsertAsync(movimento);
                            if (codigoMovimento <= 0)
                                return new Retorno(false, "Não foi possível salvar as informações de movimento");
                        }
                        
                        transacao.Complete(); 
                        retorno.Mensagem = "Pessoa cadastrada com sucesso!";
                        return retorno;
                    }                    
                }
            }
            catch (Exception e)
            {
                GravarLogErro("EmpresaRepositorio", "InserirAsync", e);
            }
            return new Retorno(false, "Não foi possível salvar as informações de pessoa");
        }
        
        public override async Task<Retorno> AtualizarAsync(Pessoa pessoa, UsuariosRegistroAtividade registroAtividade)
        {
            try
            {
                using (var transacao = CriarTransacaoAsync())
                {
                    Retorno retorno = await base.AtualizarAsync(pessoa, registroAtividade);
                    if (retorno.Status)
                    {
                        foreach (var cliente in pessoa.Clientes)
                        {
                            if (cliente.Codigo > 0)
                            {
                                cliente.CodigoPessoa = retorno.CodigoRegistro;
                                bool retornoAtualizacaoCliente = (bool)await conexaoDB.UpdateAsync(cliente);
                                if (!retornoAtualizacaoCliente)
                                    return new Retorno(false, "Não foi possível atualizar as informações de cliente");
                            }
                            else
                            {
                                cliente.CodigoPessoa = retorno.CodigoRegistro;
                                long codigoCliente = (long)await conexaoDB.InsertAsync(cliente);
                                if (codigoCliente <= 0)
                                    return new Retorno(false, "Não foi possível salvar as informações de cliente");
                            }
                            
                        }

                        foreach (var fornecedor in pessoa.Fornecedores)
                        {
                            if (fornecedor.Codigo > 0)
                            {                                
                                bool retornoAtualizacaoFornecedor = (bool)await conexaoDB.UpdateAsync(fornecedor);
                                if (!retornoAtualizacaoFornecedor)
                                    return new Retorno(false, "Não foi possível atualizar as informações de fornecedor");
                            }
                            else
                            {
                                fornecedor.CodigoPessoa = retorno.CodigoRegistro;
                                long codigoFornecedor = (long)await conexaoDB.InsertAsync(fornecedor);
                                if (codigoFornecedor <= 0)
                                    return new Retorno(false, "Não foi possível salvar as informações de fornecedor");
                            }

                        }

                        transacao.Complete(); 
                        retorno.Mensagem = "Pessoa atualizada com sucesso!";
                        return retorno;
                    }
                }
            }
            catch (Exception e)
            {
                GravarLogErro("EmpresaRepositorio", "AtualizarAsync", e);
            }
            return new Retorno(false, "Não foi possível salvar as informações de pessoa");
        }


        public override async Task<Retorno> ExcluirAsync(long codigo, UsuariosRegistroAtividade registroAtividade)
        {
            try
            {
                using (var transacao = CriarTransacaoAsync())
                {                    
                    Retorno retornoExclusaoCliente = await ExcluirEmMassaAsync<Cliente>("fk_pessoa = " + codigo);
                    if (!retornoExclusaoCliente.Status) return new Retorno("Não foi possível remover os dados de cliente");

                    Retorno retornoExclusaoFornecedor = await ExcluirEmMassaAsync<Fornecedor>("fk_pessoa = " + codigo);
                    if (!retornoExclusaoFornecedor.Status) return new Retorno("Não foi possível remover os dados de fornecedor");

                    Retorno retorno = await base.ExcluirAsync(codigo, registroAtividade);
                    if (retorno.Status)
                    {
                        transacao.Complete(); //commit 
                        retorno.Mensagem = "Pessoa excluída com sucesso!";
                        return retorno;
                    }
                }
            }
            catch (Exception e)
            {
                GravarLogErro("EmpresaRepositorio", "ExcluirAsync", e);
            }
            return new Retorno(false, "Não foi possível excluir as informações de pessoa");
        }
    }
}
            
            