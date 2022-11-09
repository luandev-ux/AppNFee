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
using AppNFe.Persistencia.Interfaces.Repositorios;
using AppNFe.Dominio.Entidades;
using AppNFe.Dominio.Entidades.Pessoas;

namespace AppNFe.Persistencia.Repositorios
{
    public class ProdutoRepositorio : RepositorioBase<Produto>, IProdutoRepositorio
    {
        public ProdutoRepositorio(IGerenteConexao gerenteConexao, ILogger logger) : base(gerenteConexao, logger) { }

        #region Propriedades 

        #region Estrutura de Consulta Rápida
        public async Task<ListaPaginada<Produto>> ObterProdutos(ParametrosConsulta parametrosConsulta, List<FiltroGenerico> filtros)
        {
            IEnumerable<Produto> listaProdutos = new List<Produto>();
            try
            {
                string filtroProduto = "";
                string filtrosSQL = "";
                string agruparPor = " TU.pk_empresa,TU.nome,TU.login,TU.senha,TU.email,TU.imagem,TU.ativo ";

                filtroProduto = " WHERE TUE.fk_empresa IN (" + string.Join(",", parametrosConsulta.Empresas) + ") ";

                if (parametrosConsulta.CodigosSelecionados != null && parametrosConsulta.CodigosSelecionados.Count() > 0)
                {
                    filtrosSQL = " AND TU.pk_empresa IN (" + string.Join(",", parametrosConsulta.CodigosSelecionados.Select(c => c)) + ") GROUP BY " + agruparPor;
                }

                var sql = new StringBuilder();
                sql.Append(" SELECT TU.* ");
                sql.Append(" FROM tb_empresa TU ");
                sql.Append(" INNER JOIN tb_empresa_empresa TUE ON TUE.fk_empresa = TU.pk_empresa ");
                sql.Append(" " + filtroProduto + filtrosSQL + " ");

                listaProdutos = await conexaoDB.QueryAsync<Produto>(sql.ToString());

            }
            catch (Exception e)
            {
                GravarLogErro("ProdutoRepositorio", "ObterProdutos", e);
            }

            return ListaPaginada<Produto>.ToListaPaginada(listaProdutos,
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
                bool filtrarProdutos = parametrosConsultaRapida.Empresas != null && parametrosConsultaRapida.Empresas.Count > 0;

                EstruturaConsultaRapida estruturaConsultaRapida = new EstruturaConsultaRapida();
                estruturaConsultaRapida.TabelaDB = filtrarProdutos ? "tb_empresa TU INNER JOIN tb_empresa_empresa TUE ON TUE.fk_empresa = TU.pk_empresa " : " tb_empresa TU ";
                estruturaConsultaRapida.ColunaCodigoDB = "TU.pk_empresa";
                estruturaConsultaRapida.ColunaTextoIdentificacaoDB = apresentarCodigo ? "TU.pk_empresa ||' - '|| TU.nome" : "TU.nome";
                estruturaConsultaRapida.CondicaoApenasAtivos = " TU.ativo = true ";

                if (filtrarProdutos)
                    estruturaConsultaRapida.CondicaoAdicional = " AND TUE.fk_empresa IN (" + string.Join(",", parametrosConsultaRapida.Empresas) + ") ";

                listaItens = await conexaoDB.QueryAsync<ItemConsultaRapida>(MontaConsultaRapidaSQL(estruturaConsultaRapida, parametrosConsultaRapida));
            }
            catch (Exception e)
            {
                GravarLogErro("ProdutoRepositorio", "ConsultaRapida", e);
            }
            return listaItens;
        }
        #endregion
        #region Inserir Cadastro
        public override async Task<Retorno> InserirAsync(Produto produto, UsuariosRegistroAtividade registroAtividade)
        {
            var retorno = new Retorno();
            try
            {
                retorno = await base.InserirAsync(produto);
            }
            catch (Exception e)
            {
                GravarLogErro("ProdutoRepositorio", "InserirAsync", e);
            }
            return retorno;
        }
        #endregion
        #region Atualizar Cadastro
        public override async Task<Retorno> AtualizarAsync(Produto produto, UsuariosRegistroAtividade registroAtividade)
        {
            var retorno = new Retorno();
            try
            {
                retorno = await base.AtualizarAsync(produto);
            }
            catch (Exception e)
            {
                GravarLogErro("ProdutoRepositorio", "AtualizarAsync", e);
            }
            return retorno;
        }
        #endregion
        #region Excluir Cadastro
        public override async Task<Retorno> ExcluirAsync(long codigo, UsuariosRegistroAtividade registroAtividade)
        {
            var retorno = new Retorno();
            try
            {
                retorno = await base.ExcluirAsync(codigo);
            }
            catch (Exception e)
            {
                GravarLogErro("ProdutoRepositorio", "ExcluirAsync", e);
            }
            return retorno;
        }
        #endregion

        #endregion
    }
}