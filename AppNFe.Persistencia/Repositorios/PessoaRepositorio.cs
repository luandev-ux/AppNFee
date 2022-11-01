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

        public override async Task<Retorno> InserirAsync(Pessoa objeto, UsuariosRegistroAtividade registroAtividade = null)
        {
            var retorno = 0;

            var c = new DynamicParameters();
            //nome, nome_fantasia, cnpj_cpf, inscricao_estadual, endereco, bairro, cidade, uf, cep, telefone, email, tipo_pessoa
            c.Add("pk_id", objeto.Nome);
            c.Add("nome", objeto.Nome);
            c.Add("nome_fantasia", objeto.NomeFantasia);
            c.Add("cnpj_cpf", objeto.CnpjCpf);
            c.Add("inscricao_estadual", objeto.InscricaoEstadual);
            c.Add("endereco", objeto.Endereco);
            c.Add("bairro", objeto.Bairro);
            c.Add("cidade", objeto.Cidade);
            c.Add("uf", objeto.Uf);
            c.Add("cep", objeto.Cep);
            c.Add("telefone", objeto.Telefone);
            c.Add("email", objeto.Email);
            c.Add("tipo_pessoa", objeto.TipoPessoa);
            c.Add("pk_id", objeto.Clientes);
            c.Add("fk_pessoa", objeto.Clientes);
            c.Add("fk_contribuinte", objeto.Clientes);
            c.Add("pk_id", objeto.Fornecedores);
            c.Add("fk_pessoa", objeto.Fornecedores);

            using (var transaction = conexaoDB.BeginTransaction())
            {
                try
                {
                    retorno = await conexaoDB.ExecuteAsync("INSERT INTO pessoa (nome, nome_fantasia, cnpj_cpf, inscricao_estadual, endereco, bairro, cidade, uf, cep, telefone, email, tipo_pessoa) VALUES (@nome, @nome_fantasia, @cnpj_cpf, @inscricao_estadual, @endereco, @bairro, @cidade, @uf, @cep, @telefone, @email, @tipo_pessoa)", c, transaction);
                    if (retorno > 0)
                    {
                        var id = await conexaoDB.QueryFirstOrDefaultAsync<int>("SELECT MAX(pk_id) FROM pessoa", transaction: transaction);
                        if (objeto.Clientes != null)
                        {
                            foreach (var item in objeto.Clientes)
                            {
                                c = new DynamicParameters();
                                c.Add("fk_pessoa", id);
                                c.Add("fk_contribuinte", item);
                                retorno = await conexaoDB.ExecuteAsync("INSERT INTO tb_cliente (fk_pessoa, fk_contribuinte) VALUES (@fk_pessoa, @fk_contribuinte)", c, transaction);
                            }
                        }
                        if (objeto.Fornecedores != null)
                        {
                            foreach (var item in objeto.Fornecedores)
                            {
                                c = new DynamicParameters();
                                c.Add("fk_pessoa", id);
                                c.Add("fk_contribuinte", item);
                                retorno = await conexaoDB.ExecuteAsync("INSERT INTO tb_fornecedor (fk_pessoa, fk_contribuinte) VALUES (@fk_pessoa, @fk_contribuinte)", c, transaction);
                            }
                        }
                    }
                    transaction.Commit();
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    GravarLogErro("PessoaRepositorio", "InserirAsync", e);
                    retorno = 0;
                }
            }
        }
    }
}

            
            