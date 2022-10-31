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
using AppNFe.Dominio.Entidades;

namespace AppNFe.Persistencia.Repositorios
{
    public class UsuarioRepositorio : RepositorioBase<Usuario>, IUsuarioRepositorio
    {
        public UsuarioRepositorio(IGerenteConexao gerenteConexao, ILogger logger) : base(gerenteConexao, logger) { }

        #region Obter Usuário
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
        #region Select Usuario
        public async Task<IEnumerable<ItemConsultaRapida>> ConsultaRapida(string termo, List<int> empresas)
        {
            IEnumerable<ItemConsultaRapida> listaUsuarios = new List<ItemConsultaRapida>();
            try
            {
                var sql = new StringBuilder();
                sql.Append(" SELECT TU.pk_usuario AS Codigo, TU.nome AS Descricao ");
                sql.Append(" FROM tb_usuario TU ");
                sql.Append(" INNER JOIN tb_usuario_empresa TUE ON TUE.fk_usuario = TU.pk_usuario ");
                sql.Append(" WHERE TU.nome LIKE '%" + termo + "%' AND TUE.fk_empresa IN (" + string.Join(",", empresas) + ") ");
                sql.Append(" GROUP BY TU.pk_usuario,TU.nome ");
                sql.Append(" ORDER BY TU.nome ");

                listaUsuarios = await conexaoDB.QueryAsync<ItemConsultaRapida>(sql.ToString());
            }
            catch (Exception e)
            {
                GravarLogErro("UsuarioRepositorio", "ConsultaRapida", e);
            }
            return listaUsuarios;
        }
        #endregion
        #region Inserir Usuario
        public override async Task<Retorno> InserirAsync(Usuario objeto, UsuariosRegistroAtividade registroAtividade)
        {
            var retorno = new Retorno();
            try
            {
                retorno = await base.InserirAsync(objeto);
            }
            catch (Exception e)
            {
                GravarLogErro("UsuarioRepositorio", "InserirAsync", e);
            }
            return retorno;
        }
        #endregion
    }
}