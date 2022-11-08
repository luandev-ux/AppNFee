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
using AppNFe.Dominio.Entidades;
using AppNFe.Persistencia.Interfaces.Repositorios;

namespace AppNFe.Persistencia.Repositorios
{
    public class ConfiguracaoFiscalRepositorio : RepositorioBase<ConfiguracaoFiscal>, IConfiguracaoFiscalRepositorio
    {
        public ConfiguracaoFiscalRepositorio(IGerenteConexao gerenteConexao, ILogger logger) : base(gerenteConexao, logger) { }

        #region Obter Usuário
        public async Task<ListaPaginada<ConfiguracaoFiscal>> ObterConfiguracaoFiscals(ParametrosConsulta parametrosConsulta, List<FiltroGenerico> filtros)
        {
            IEnumerable<ConfiguracaoFiscal> listaConfiguracaoFiscals = new List<ConfiguracaoFiscal>();
            try
            {
                string filtroEmpresa = "";
                string filtrosSQL = "";
                string agruparPor = " TU.pk_configuracaoFiscal,TU.nome,TU.login,TU.senha,TU.email,TU.imagem,TU.ativo ";

                filtroEmpresa = " WHERE TUE.fk_empresa IN (" + string.Join(",", parametrosConsulta.Empresas) + ") ";

                if (parametrosConsulta.CodigosSelecionados != null && parametrosConsulta.CodigosSelecionados.Count() > 0)
                {
                    filtrosSQL = " AND TU.pk_configuracaoFiscal IN (" + string.Join(",", parametrosConsulta.CodigosSelecionados.Select(c => c)) + ") GROUP BY " + agruparPor;
                }

                var sql = new StringBuilder();
                sql.Append(" SELECT TU.* ");
                sql.Append(" FROM tb_configuracaoFiscal TU ");
                sql.Append(" INNER JOIN tb_configuracaoFiscal_empresa TUE ON TUE.fk_configuracaoFiscal = TU.pk_configuracaoFiscal ");
                sql.Append(" " + filtroEmpresa + filtrosSQL + " ");

                listaConfiguracaoFiscals = await conexaoDB.QueryAsync<ConfiguracaoFiscal>(sql.ToString());

            }
            catch (Exception e)
            {
                GravarLogErro("ConfiguracaoFiscalRepositorio", "ObterConfiguracaoFiscals", e);
            }

            return ListaPaginada<ConfiguracaoFiscal>.ToListaPaginada(listaConfiguracaoFiscals,
                      parametrosConsulta.NumeroPagina,
                      parametrosConsulta.QtdeRegistrosPagina);
        }
        #endregion

        #region Select ConfiguracaoFiscal
        public async Task<IEnumerable<ItemConsultaRapida>> ConsultaRapida(string termo, List<int> empresas)
        {
            IEnumerable<ItemConsultaRapida> listaConfiguracaoFiscals = new List<ItemConsultaRapida>();
            try
            {
                var sql = new StringBuilder();
                sql.Append(" SELECT TU.pk_configuracaoFiscal AS Codigo, TU.nome AS Descricao ");
                sql.Append(" FROM tb_configuracaoFiscal TU ");
                sql.Append(" INNER JOIN tb_configuracaoFiscal_empresa TUE ON TUE.fk_configuracaoFiscal = TU.pk_configuracaoFiscal ");
                sql.Append(" WHERE TU.nome LIKE '%" + termo + "%' AND TUE.fk_empresa IN (" + string.Join(",", empresas) + ") ");
                sql.Append(" GROUP BY TU.pk_configuracaoFiscal,TU.nome ");
                sql.Append(" ORDER BY TU.nome ");

                listaConfiguracaoFiscals = await conexaoDB.QueryAsync<ItemConsultaRapida>(sql.ToString());
            }
            catch (Exception e)
            {
                GravarLogErro("ConfiguracaoFiscalRepositorio", "ConsultaRapida", e);
            }
            return listaConfiguracaoFiscals;
        }
        #endregion
        
        #region Inserir ConfiguracaoFiscal
        public override async Task<Retorno> InserirAsync(ConfiguracaoFiscal objeto, UsuariosRegistroAtividade registroAtividade)
        {
            var retorno = new Retorno();
            try
            {
                retorno = await base.InserirAsync(objeto);
            }
            catch (Exception e)
            {
                GravarLogErro("ConfiguracaoFiscalRepositorio", "InserirAsync", e);
            }
            return retorno;
        }
        #endregion
    }
}