using Dapper;

using AppNFe.Core.Persistencia;
using AppNFe.Core.Persistencia.Consulta;
using AppNFe.Dominio.Consulta;
using AppNFe.Persistencia.Interfaces;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dommel;
using AppNFe.Core.DominioProblema;
using System.Text;
using AppNFe.Dominio.Entidades;
using AppNFe.Persistencia.Interfaces.Repositorios;
using AppNFe.Dominio.Entidades.Pessoas;

namespace AppNFe.Persistencia.Repositorios
{
    public class MovimentoRepositorio : RepositorioBase<Movimento>, IMovimentoRepositorio
    {
        public MovimentoRepositorio(IGerenteConexao gerenteConexao, ILogger logger) : base(gerenteConexao, logger) { }

        #region Propriedades
        
        #region Estrutura de Consulta Rápida
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
        #endregion
        #region Consulta Rápida
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
        #endregion
        #region Inserir Movimento
        public override async Task<Retorno> InserirAsync(Movimento movimento, UsuariosRegistroAtividade registroAtividade)
        {
            try
            {
                using (var transacao = CriarTransacaoAsync())
                {
                    Retorno retorno = await base.InserirAsync(movimento, registroAtividade);
                    if (retorno.Status)
                    {
                        foreach (var pessoa in movimento.Pessoas)
                        {
                            pessoa.Codigo = retorno.CodigoRegistro;
                            long codigoCliente = (long)await conexaoDB.InsertAsync(pessoa);
                            if (codigoCliente <= 0)
                                return new Retorno(false, "Não foi possível salvar as informações de cliente");
                        }


                        transacao.Complete();
                        retorno.Mensagem = "Movimento cadastrada com sucesso!";
                        return retorno;
                    }
                }
            }
            catch (Exception e)
            {
                GravarLogErro("EmpresaRepositorio", "InserirAsync", e);
            }
            return new Retorno(false, "Não foi possível salvar as informações de movimento");
        }
        #endregion
        #region Atualizar Movimento
        public override async Task<Retorno> AtualizarAsync(Movimento movimento, UsuariosRegistroAtividade registroAtividade)
        {
            try
            {
                using (var transacao = CriarTransacaoAsync())
                {
                    Retorno retorno = await base.AtualizarAsync(movimento, registroAtividade);
                    if (retorno.Status)
                    {
                        foreach (var pessoa in movimento.Pessoas)
                        {
                            if (pessoa.Codigo > 0)
                            {
                                movimento.Codigo = retorno.CodigoRegistro;
                                bool retornoAtualizacaoPessoa = await conexaoDB.UpdateAsync(pessoa);
                                if (!retornoAtualizacaoPessoa)
                                    return new Retorno(false, "Não foi possível atualizar as informações de Movimentação");
                            }
                            else
                            {
                                pessoa.Codigo = retorno.CodigoRegistro;
                                long codigoPessoa = (long)await conexaoDB.InsertAsync(pessoa);
                                if (codigoPessoa <= 0)
                                    return new Retorno(false, "Não foi possível salvar as informações de Movimentação");
                            }

                        }

                        transacao.Complete();
                        retorno.Mensagem = "Movimentação atualizada com sucesso!";
                        return retorno;
                    }
                }
            }
            catch (Exception e)
            {
                GravarLogErro("EmpresaRepositorio", "AtualizarAsync", e);
            }
            return new Retorno(false, "Não foi possível salvar as informações de movimento");
        }
        #endregion
        #region Excluir Movimento
        public override async Task<Retorno> ExcluirAsync(long codigo, UsuariosRegistroAtividade registroAtividade)
        {
            try
            {
                using (var transacao = CriarTransacaoAsync())
                {
                    Retorno retornoExclusaoCliente = await ExcluirEmMassaAsync<Cliente>("fk_movimento = " + codigo);
                    if (!retornoExclusaoCliente.Status) return new Retorno("Não foi possível remover os dados de cliente");

                    Retorno retornoExclusaoFornecedor = await ExcluirEmMassaAsync<Fornecedor>("fk_movimento = " + codigo);
                    if (!retornoExclusaoFornecedor.Status) return new Retorno("Não foi possível remover os dados de fornecedor");

                    Retorno retorno = await base.ExcluirAsync(codigo, registroAtividade);
                    if (retorno.Status)
                    {
                        transacao.Complete(); //commit 
                        retorno.Mensagem = "Movimento excluída com sucesso!";
                        return retorno;
                    }
                }
            }
            catch (Exception e)
            {
                GravarLogErro("EmpresaRepositorio", "ExcluirAsync", e);
            }
            return new Retorno(false, "Não foi possível excluir as informações de movimento");
        }
        #endregion

        #endregion
    }
}

