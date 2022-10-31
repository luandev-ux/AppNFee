using AppNFe.Core.DominioProblema;
using AppNFe.Core.Utilitarios;
using NeoXBI.Core.DominioProblema;
//using Npgsql;
using System;
using System.Data;
using DataSet = System.Data.DataSet;

namespace NeoXBI.Core.Persistencia
{
    public class MapeadorPostgreSQL
    {
        protected Retorno AbrirConexao()
        {
            //try
            //{
            //if (ContextoConexao.Conexao != null && ContextoConexao.Conexao.State == ConnectionState.Open) ContextoConexao.Conexao.Close();
            //ContextoConexao.Conexao = new NpgsqlConnection();

            //var npgsqlBuilder = new NpgsqlConnectionStringBuilder();
            //npgsqlBuilder.Host = "localhost";
            //npgsqlBuilder.Port = 5432;
            //npgsqlBuilder.Database = "BI";
            //npgsqlBuilder.UserName = "ADMIN";
            //npgsqlBuilder.Password = "L30P4RDGR4MP";
            //npgsqlBuilder.PreloadReader = true;
            //npgsqlBuilder.Timeout = 15;
            //npgsqlBuilder.ConnectionLifeTime = 300;
            //ContextoConexao.Conexao.ConnectionString = npgsqlBuilder.ToString();
            //ContextoConexao.Conexao.Open();

            return new Retorno();
            //}
            //catch (Exception e)
            //{
              //  return new Retorno(false,e.Message);
            //}
        }

        protected void FecharConexao()
        {
            if (ContextoConexao.Conexao != null && ContextoConexao.Conexao.State == ConnectionState.Open) ContextoConexao.Conexao.Close();
        }

        protected Retorno ExecutarComando(string sql)
        {
            //try
            //{
            //NpgsqlCommand comando;
            //comando = new NpgsqlCommand(sql, (NpgsqlConnection)ContextoConexao.Conexao);
            //comando.ExecuteNonQuery();
            return new Retorno();
            //}
            //catch (Exception e)
            //{
            //    return new Retorno(false,e.Message);
            //}
        }

        protected RetornoConsulta<DataRow> ExecutarConsulta(string sql)
        {
            try
            {
                //var command = new NpgsqlCommand(sql, (NpgsqlConnection)ContextoConexao.Conexao);
                //var adapter = new NpgsqlDataAdapter(command);
                var dataSet = new DataSet();

                //adapter.Fill(dataSet);
                //adapter.Dispose();
                return new RetornoConsulta<DataRow>(dataSet.Tables[0].Rows);
            }
            catch (Exception e)
            {
                return new RetornoConsulta<DataRow>(e.Message);
            }
        }
    }
}
