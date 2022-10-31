using Microsoft.Extensions.Configuration;
using AppNFe.Persistencia.Interfaces;
using Npgsql;
using Serilog;
using System;
using System.Data;

namespace AppNFe.Persistencia
{
    public class GerenteConexao: IGerenteConexao
    {        
        public readonly IConfiguration configuration;

        public string Contratante { get; set; }
        private ILogger logger;

        public GerenteConexao(IConfiguration configuration , ILogger logger)
        {
            this.configuration = configuration;
            this.logger = logger;
        }

        public IDbConnection CriarConexao()
        {
            IDbConnection conexaoDB = null;

            try
            {
                if (!string.IsNullOrWhiteSpace(this.Contratante))
                {
                    string stringConexao = configuration.GetConnectionString("Padrao");
                    string configuracaoContratante = "Contratantes:Contratante_" + Contratante;
                    string servidor = configuration[configuracaoContratante + ":Servidor"];
                    string porta = configuration[configuracaoContratante + ":Porta"];
                    string bancoDados = configuration[configuracaoContratante + ":BancoDados"];

                    stringConexao = stringConexao.Replace("{{Servidor}}", servidor);
                    stringConexao = stringConexao.Replace("{{Porta}}", porta);
                    stringConexao = stringConexao.Replace("{{BancoDados}}", bancoDados);                    
                    conexaoDB = new NpgsqlConnection(stringConexao);                    
                }
            }
            catch (Exception ex)
            {
                logger.Error("Erro: GerenteConexao > CriarConexao Detalhes: " + ex.Message);
            }
            
            return conexaoDB;
        }
    }
}
