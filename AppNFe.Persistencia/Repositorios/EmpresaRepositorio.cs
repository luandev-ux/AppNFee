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

namespace AppNFe.Persistencia.Repositorios
{
    public class EmpresaRepositorio : RepositorioBase<Empresa>, IEmpresaRepositorio
    {
        public EmpresaRepositorio(IGerenteConexao gerenteConexao, ILogger logger) : base(gerenteConexao, logger) { }

        public async Task<ListaPaginada<Empresa>> ObterEmpresas(ParametrosConsulta parametrosConsulta, List<FiltroGenerico> filtros)
        {
            IEnumerable<Empresa> listaEmpresas = new List<Empresa>();
            try
            {
                string filtroEmpresa = "";
                string filtrosSQL = "";
                string agruparPor = " TU.pk_empresa,TU.nome,TU.login,TU.senha,TU.email,TU.imagem,TU.ativo ";

                filtroEmpresa = " WHERE TUE.fk_empresa IN (" + string.Join(",", parametrosConsulta.Empresas) + ") ";

                if (parametrosConsulta.CodigosSelecionados != null && parametrosConsulta.CodigosSelecionados.Count() > 0)
                {
                    filtrosSQL = " AND TU.pk_empresa IN (" + string.Join(",", parametrosConsulta.CodigosSelecionados.Select(c => c)) + ") GROUP BY " + agruparPor;
                }

                var sql = new StringBuilder();
                sql.Append(" SELECT TU.* ");
                sql.Append(" FROM tb_empresa TU ");
                sql.Append(" INNER JOIN tb_empresa_empresa TUE ON TUE.fk_empresa = TU.pk_empresa ");
                sql.Append(" " + filtroEmpresa + filtrosSQL + " ");

                listaEmpresas = await conexaoDB.QueryAsync<Empresa>(sql.ToString());

            }
            catch (Exception e)
            {
                GravarLogErro("EmpresaRepositorio", "ObterEmpresas", e);
            }

            return ListaPaginada<Empresa>.ToListaPaginada(listaEmpresas,
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
                estruturaConsultaRapida.TabelaDB = filtrarEmpresas ? "tb_empresa TU INNER JOIN tb_empresa_empresa TUE ON TUE.fk_empresa = TU.pk_empresa " : " tb_empresa TU ";
                estruturaConsultaRapida.ColunaCodigoDB = "TU.pk_empresa";
                estruturaConsultaRapida.ColunaTextoIdentificacaoDB = apresentarCodigo ? "TU.pk_empresa ||' - '|| TU.nome" : "TU.nome";
                estruturaConsultaRapida.CondicaoApenasAtivos = " TU.ativo = true ";

                if (filtrarEmpresas)
                    estruturaConsultaRapida.CondicaoAdicional = " AND TUE.fk_empresa IN (" + string.Join(",", parametrosConsultaRapida.Empresas) + ") ";

                listaItens = await conexaoDB.QueryAsync<ItemConsultaRapida>(MontaConsultaRapidaSQL(estruturaConsultaRapida, parametrosConsultaRapida));
            }
            catch (Exception e)
            {
                GravarLogErro("EmpresaRepositorio", "ConsultaRapida", e);
            }
            return listaItens;
        }

        public override async Task<Retorno> InserirAsync(Empresa empresa, UsuariosRegistroAtividade registroAtividade)
        {
            var retorno = new Retorno();
            try
            {
                retorno = await base.InserirAsync(empresa);
            }
            catch (Exception e)
            {
                GravarLogErro("EmpresaRepositorio", "InserirAsync", e);
            }
            return retorno;
        }
    }
}