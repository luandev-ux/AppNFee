using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Dommel;
using System.Data;
using System.Transactions;
using AppNFe.Core.DominioProblema;
using AppNFe.Persistencia.Interfaces.Repositorios;
using Serilog;
using System.Threading.Tasks;
using AppNFe.Persistencia.Interfaces;
using System.Text;
using AppNFe.Core.Enumeradores;
using System.Linq;
using AppNFe.Core.Persistencia;
using AppNFe.Persistencia.Mapeadores;
using AppNFe.Core.Persistencia.Consulta;
using Dapper;
using AppNFe.Core.Utilitarios;
using AppNFe.Dominio.Entidades;

namespace AppNFe.Persistencia.Repositorios
{
    public abstract class RepositorioBase<TEntidade> : IRepositorioBase<TEntidade> where TEntidade : class
    {
        public readonly IConfiguration configuration;
        public readonly ILogger logger;
        public readonly IDbConnection conexaoDB;
        public readonly string contratante;

        #region Cria conexão com o banco de dados, cria mapeamento e log de erro
        public RepositorioBase(IGerenteConexao gerenteConexao, ILogger logger)
        {
            try
            {
                conexaoDB = gerenteConexao.CriarConexao();
                contratante = gerenteConexao.Contratante;
                this.logger = logger;
                MapeamentoDB.CriarMapeamento();
            }
            catch (Exception e)
            {
                GravarLogErro("RepositorioBase", "Construtor", e);
            }
        }
        #endregion
        
        #region Criar Transação e Executar Comando SQL com Dapper
        public TransactionScope CriarTransacaoAsync()
        {
            return new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
        }

        public string ObterNomeTabelaDB<Type>()
        {
            string nomeTabela = "";
            try
            {
                var resolver = new Dapper.FluentMap.Dommel.Resolvers.DommelTableNameResolver();
                nomeTabela = resolver.ResolveTableName(typeof(Type));
            }
            catch (Exception e)
            {
                GravarLogErro("RepositorioBase", "ObterNomeTabelaDB", e);
            }

            return nomeTabela;
        }
        #endregion

        #region Obter Registro por Código
        public string ObterNomeColunaDB<Type>(string propriedade)
        {
            string nomeColuna = "";
            try
            {
                var resolver = new Dapper.FluentMap.Dommel.Resolvers.DommelColumnNameResolver();
                nomeColuna = resolver.ResolveColumnName(typeof(Type).GetProperty(propriedade));
            }
            catch (Exception e)
            {
                GravarLogErro("RepositorioBase", "ObterNomeColunaDB", e);
            }

            return nomeColuna;
        }
        #endregion
        
        #region Operador AND / OR para consulta SQL
        public string ObtenhaOperadorSQL(byte operador)
        {
            string operadorSQL = "";
            switch (operador)
            {
                case (byte)EOperadorLogico.E:
                    operadorSQL = " AND ";
                    break;
                case (byte)EOperadorLogico.OU:
                    operadorSQL = " OR ";
                    break;
            }
            return operadorSQL;
        }
        #endregion
        
        #region Filtro utilizado em Consultas, Relatórios e Fluxo de trabalho
        public string ObtenhaCondicaoFiltroGenericoComValorSQL(FiltroGenerico filtro)
        {
            string condicaoSQL = "";
            try
            {
                string valor = "";

                // Trata condições que possue lista de valores ou mais de um valor
                if (filtro.Condicao == (byte)ECondicao.Entre || filtro.Condicao == (byte)ECondicao.EstaEntre || filtro.Condicao == (byte)ECondicao.NaoEstaEntre)
                {
                    if (filtro.Condicao == (byte)ECondicao.Entre)
                    {
                        switch (filtro.TipoDado)
                        {
                            case (byte)ETipoDado.Inteiro:
                            case (byte)ETipoDado.Longo:
                                valor = " " + filtro.Valores[0] + " AND " + filtro.Valores[1] + " ";
                                break;

                            case (byte)ETipoDado.Numerico:
                                valor = " " + TratarValorMonetarioDB(filtro.Valores[0]) + " AND " + TratarValorMonetarioDB(filtro.Valores[1]) + " ";
                                break;
                            case (byte)ETipoDado.Data:
                                valor = " '" + DataToDB(DateTime.Parse(filtro.Valores[0])) + "' AND '" + DataToDB(DateTime.Parse(filtro.Valores[1])) + "' ";
                                break;
                            case (byte)ETipoDado.DataHoraMinuto:
                                valor = " '" + DataHoraMinutoToDB(DateTime.Parse(filtro.Valores[0])) + "' AND '" + DataHoraMinutoToDB(DateTime.Parse(filtro.Valores[1])) + "' ";
                                break;
                            case (byte)ETipoDado.DataHoraMinutoSegundo:
                                valor = " '" + DataHoraMinutoSegundoToDB(DateTime.Parse(filtro.Valores[0])) + "' AND '" + DataHoraMinutoSegundoToDB(DateTime.Parse(filtro.Valores[1])) + "' ";
                                break;
                        }
                    }
                    else
                    {
                        switch (filtro.TipoDado)
                        {
                            case (int)ETipoDado.Inteiro:
                            case (int)ETipoDado.Longo:
                                valor = " (" + string.Join(",", filtro.Valores.Select(c => c)) + ") ";
                                break;
                            case (int)ETipoDado.Numerico:
                                valor = " (" + string.Join(",", filtro.Valores.Select(c => TratarValorMonetarioDB(c))) + ") ";
                                break;

                            case (int)ETipoDado.Texto:
                                valor = " ('" + string.Join("','", filtro.Valores.Select(c => c)) + "') ";
                                break;
                        }
                    }
                }
                else
                {
                    // Trata condições e valores com um único valor
                    switch (filtro.TipoDado)
                    {
                        case (int)ETipoDado.Inteiro:
                        case (int)ETipoDado.Longo:
                            valor = filtro.Valores[0];
                            break;
                        case (int)ETipoDado.Numerico:
                            valor = TratarValorMonetarioDB(filtro.Valores[0]);
                            break;
                        case (int)ETipoDado.Data:
                            valor = "'" + DataToDB(DateTime.Parse(filtro.Valores[0])) + "'";
                            break;
                        case (int)ETipoDado.DataHoraMinuto:
                            valor = "'" + DataHoraMinutoToDB(DateTime.Parse(filtro.Valores[0])) + "'";
                            break;
                        case (int)ETipoDado.DataHoraMinutoSegundo:
                            valor = "'" + DataHoraMinutoSegundoToDB(DateTime.Parse(filtro.Valores[0])) + "'";
                            break;
                        default:
                            valor = filtro.Valores[0];
                            break;
                    }
                }

                // Preenche a condição e o valor do filtro
                switch (filtro.Condicao)
                {
                    case (byte)ECondicao.Contem:
                        condicaoSQL = " ilike '%" + valor + "%'";
                        break;
                    case (byte)ECondicao.Inicia:
                        condicaoSQL = " ilike '" + valor + "%'";
                        break;
                    case (byte)ECondicao.Termina:
                        condicaoSQL = " ilike '%" + valor + "'";
                        break;
                    case (int)ECondicao.Igual:
                        if (filtro.TipoDado == (int)ETipoDado.Texto)
                            condicaoSQL = " = '" + valor + "'";
                        else
                            condicaoSQL = " = " + valor;
                        break;
                    case (byte)ECondicao.Diferente:
                        condicaoSQL = " <> " + valor;
                        break;
                    case (byte)ECondicao.Menor:
                        condicaoSQL = " < " + valor;
                        break;
                    case (byte)ECondicao.Maior:
                        condicaoSQL = " > " + valor;
                        break;
                    case (byte)ECondicao.MenorOuIgual:
                        condicaoSQL = " <= " + valor;
                        break;
                    case (byte)ECondicao.MaiorOuIgual:
                        condicaoSQL = " >= " + valor;
                        break;
                    case (byte)ECondicao.Entre:
                        condicaoSQL = " BETWEEN " + valor;
                        break;
                    case (byte)ECondicao.EstaEntre:
                        condicaoSQL = " IN " + valor;
                        break;
                    case (byte)ECondicao.NaoEstaEntre:
                        condicaoSQL = " NOT IN " + valor;
                        break;
                }
            }
            catch (Exception e)
            {
                GravarLogErro("RepositorioBase", "ObtenhaCondicaoFiltroComValorSQL", e);
            }

            return condicaoSQL;
        }
        #endregion
        
        #region Filtro utilizado em Consultas, Relatórios e Fluxo de trabalho
        public string MontarFiltrosGenericosSQL(List<FiltroGenerico> filtrosComValores, List<PropriedadeConsulta> propriedadesConsulta, List<string> ordenacao, int qtdeRegistrosTotal = 500, bool preCondicaoAdicionada = false, string agruparColunas = "")
        {
            StringBuilder sqlQuery = new StringBuilder();

            try
            {
                if (filtrosComValores != null && propriedadesConsulta != null)
                {
                    bool primeiroFiltro = true;
                    byte ultimoOperador = (byte)EOperadorLogico.E;

                    foreach (var filtro in filtrosComValores)
                    {
                        filtro.CampoDB = propriedadesConsulta.Where(c => c.Identificador == filtro.Identificador).FirstOrDefault().CampoDB;

                        if (string.IsNullOrEmpty(filtro.CampoDB))
                        {
                            logger.Information("Informação: RepositorioBase > Método: MontarFiltrosGenericosSQL Detalhes: Coluna do filtro não localizada!");
                            return "";
                        }

                        if (filtro.Valores == null)
                            return "";

                        if (filtro.Valores.Count == 1 && filtro.Condicao == (byte)ECondicao.Entre)
                            return "";

                        // Estrategia - Operadores lógicos
                        if (primeiroFiltro && preCondicaoAdicionada == false)
                        {
                            sqlQuery.Append(" WHERE ");
                            primeiroFiltro = false;
                        }
                        else
                        {
                            sqlQuery.Append(ObtenhaOperadorSQL(filtro.Operador));
                        }

                        string condicaoSQL = ObtenhaCondicaoFiltroGenericoComValorSQL(filtro);

                        if (!string.IsNullOrEmpty(condicaoSQL))
                            sqlQuery.Append(filtro.CampoDB + condicaoSQL + " ");

                        ultimoOperador = filtro.Operador;
                    }
                }
               
                if (!string.IsNullOrEmpty(agruparColunas))
                    sqlQuery.Append(" GROUP BY " + agruparColunas + " ");

                sqlQuery.Append(" " + ObtenhaOrdenacaoSQL(ordenacao, propriedadesConsulta) + " ");
                if (qtdeRegistrosTotal > 0)
                    sqlQuery.Append(" LIMIT " + qtdeRegistrosTotal + " ");
            }
            catch (Exception e)
            {
                GravarLogErro("RepositorioBase", "MontarFiltrosGenericosSQL", e);
            }
            return sqlQuery.ToString();
        }
        #endregion
        
        #region Monta consulta rápida padronizada com objetivo de deixa-las seguras e performatica. 
        /// <summary>
        /// Monta consulta rápida padronizada com objetivo de deixa-las seguras e performatica.
        /// </summary>
        /// <param name="estruturaConsultaRapida"></param>
        /// <param name="parametrosConsultaRapida"></param>
        /// <returns></returns>
        public string MontaConsultaRapidaSQL(EstruturaConsultaRapida estruturaConsultaRapida,
                                             ParametrosConsultaRapida parametrosConsultaRapida)
        {
            var sql = new StringBuilder();
            bool condicaoApenasAtivoAdicionada = false;
            try
            {
                if (estruturaConsultaRapida.ColunaTextoIdentificacaoDB == "" || estruturaConsultaRapida.ColunaTextoIdentificacaoDB == null)
                    estruturaConsultaRapida.ColunaTextoIdentificacaoDB = estruturaConsultaRapida.ColunaCodigoDB;
                sql.Append(" SELECT DISTINCT (" + estruturaConsultaRapida.ColunaCodigoDB + ") as Codigo,(" + estruturaConsultaRapida.ColunaTextoIdentificacaoDB + ") as TextoIdentificacao ");
                sql.Append(" FROM " + estruturaConsultaRapida.TabelaDB + " ");

                if (parametrosConsultaRapida.ApenasAtivos && !string.IsNullOrEmpty(estruturaConsultaRapida.CondicaoApenasAtivos))
                {
                    sql.Append(" WHERE " + estruturaConsultaRapida.CondicaoApenasAtivos + " ");
                    condicaoApenasAtivoAdicionada = true;
                }

                if (!string.IsNullOrEmpty(parametrosConsultaRapida.Valor) || parametrosConsultaRapida.Valores != null && parametrosConsultaRapida.Valores.Count > 0)
                {
                    sql.Append(condicaoApenasAtivoAdicionada ? " AND " : " WHERE ");

                    if (parametrosConsultaRapida.FiltrarPorCodigo)
                    {
                        sql.Append(" " + estruturaConsultaRapida.ColunaCodigoDB + " = " + parametrosConsultaRapida.Valor + " " + estruturaConsultaRapida.CondicaoAdicional);
                    }
                    else if (parametrosConsultaRapida.FiltrarPorVariosCodigos)
                    {
                        sql.Append(" " + estruturaConsultaRapida.ColunaCodigoDB + " IN(" + string.Join(", ", parametrosConsultaRapida.Valores) + ") " + estruturaConsultaRapida.CondicaoAdicional);
                    }
                    else
                    {
                        string colunaCondicaoTextoIdentificacao = estruturaConsultaRapida.ColunaTextoIdentificacaoDB;

                        if (!string.IsNullOrEmpty(estruturaConsultaRapida.CondicaoEspecialTextoIdentificacaoDB))
                            colunaCondicaoTextoIdentificacao = estruturaConsultaRapida.CondicaoEspecialTextoIdentificacaoDB;

                        if (parametrosConsultaRapida.FiltrarTextoExato)
                        {
                            sql.Append(" removerAcentos(" + colunaCondicaoTextoIdentificacao + ") = '" + UtilitarioTexto.RemoverAcentosPadraoDB(parametrosConsultaRapida.Valor) + "' " + estruturaConsultaRapida.CondicaoAdicional);
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(parametrosConsultaRapida.Valor))
                            {
                                sql.Append(" removerAcentos(" + colunaCondicaoTextoIdentificacao + ") LIKE '%" + UtilitarioTexto.RemoverAcentosPadraoDB(parametrosConsultaRapida.Valor) + "%' " + estruturaConsultaRapida.CondicaoAdicional);
                            }
                            else if (parametrosConsultaRapida.Valores != null && parametrosConsultaRapida.Valores.Count > 0)
                            {
                                int valoresExecutados = 0;
                                foreach (var valorPesquisa in parametrosConsultaRapida.Valores)
                                {
                                    if (valoresExecutados > 0)
                                        sql.Append(" OR ");

                                    sql.Append(" removerAcentos(" + colunaCondicaoTextoIdentificacao + ") LIKE '%" + UtilitarioTexto.RemoverAcentosPadraoDB(valorPesquisa) + "%' ");
                                    valoresExecutados++;
                                }

                                sql.Append(estruturaConsultaRapida.CondicaoAdicional);
                            }

                        }

                        sql.Append(" ORDER BY TextoIdentificacao ASC ");
                    }
                }
                else
                {
                    sql.Append(condicaoApenasAtivoAdicionada ? " AND " : " WHERE ");
                    sql.Append(" " + estruturaConsultaRapida.ColunaCodigoDB + " > 0 " + estruturaConsultaRapida.CondicaoAdicional);
                    sql.Append(" ORDER BY " + estruturaConsultaRapida.ColunaCodigoDB + " ASC ");
                }

                if (parametrosConsultaRapida.QuantidadeRegistros > 0)
                    sql.Append(" LIMIT " + parametrosConsultaRapida.QuantidadeRegistros);
            }
            catch (Exception e)
            {
                GravarLogErro("RepositorioBase", "MontaConsultaRapidaSQL", e);
            }

            return sql.ToString();
        }
        #endregion

        #region Ordernar Consulta
        public string ObtenhaOrdenacaoSQL(List<string> ordenacao, List<PropriedadeConsulta> propriedadesConsulta)
        {
            string ordenacaoSQL = "";

            try
            {
                if (propriedadesConsulta == null)
                    return "";

                if (ordenacao == null)
                    return "";

                foreach (var itemOrdenacao in ordenacao)
                {
                    if (!string.IsNullOrEmpty(itemOrdenacao))
                    {
                        string item = itemOrdenacao.Replace(" ", "");

                        string[] dadosOrdenacao = item.Split('|');

                        foreach (var dado in dadosOrdenacao)
                        {
                            var campoOrdenacao = propriedadesConsulta.Where(c => c.Identificador == dado && c.Ordenacao == true).FirstOrDefault();
                            if (campoOrdenacao != null)
                            {
                                if (!string.IsNullOrEmpty(ordenacaoSQL))
                                    ordenacaoSQL += ",";

                                ordenacaoSQL += campoOrdenacao.CampoDB;
                            }
                        }

                        if (!string.IsNullOrEmpty(ordenacaoSQL))
                        {
                            if (dadosOrdenacao.LastOrDefault().Contains("ASC") || dadosOrdenacao.LastOrDefault().Contains("DESC"))
                            {
                                ordenacaoSQL += " " + dadosOrdenacao.LastOrDefault() + " ";
                            }
                            else
                            {
                                ordenacaoSQL += " ASC ";
                            }
                        }
                    }
                }

                if (!string.IsNullOrEmpty(ordenacaoSQL))
                    return " ORDER BY " + ordenacaoSQL;
            }
            catch (Exception e)
            {
                GravarLogErro("RepositorioBase", "ObtenhaOrdenacaoSQL", e);
            }
            return ordenacaoSQL;
        }
        #endregion

        #region Inserir Registro no Banco de Dados
        public virtual Retorno Inserir(TEntidade objeto)
        {
            try
            {
                long codigoRegistro = (long)conexaoDB.Insert(objeto);
                return new Retorno(codigoRegistro);
            }
            catch (Exception e)
            {
                GravarLogErro("RepositorioBase", "Inserir", e);
                return new Retorno();
            }
        }
        #endregion

        #region Atualizar Registro no Banco de Dados
        public virtual Retorno Atualizar(TEntidade objeto)
        {
            try
            {
                var resultado = conexaoDB.Update(objeto);
                return new Retorno(resultado);
            }
            catch (Exception e)
            {
                GravarLogErro("RepositorioBase", "Atualizar", e);
                return new Retorno();
            }
        }
        #endregion

        #region Excluir Registro no Banco de Dados
        public virtual Retorno Excluir(long codigo)
        {
            try
            {
                var resultado = conexaoDB.Delete(conexaoDB.Get<TEntidade>(codigo));
                return new Retorno(resultado);
            }
            catch (Exception e)
            {
                GravarLogErro("RepositorioBase", "Excluir", e);
                return new Retorno();
            }
        }
        #endregion

        #region Obter Registro no Banco de Dados
        public virtual TEntidade ObterPeloCodigo(long codigo)
        {
            TEntidade tEntity = null;
            try
            {
                tEntity = conexaoDB.Get<TEntidade>(codigo);
            }
            catch (Exception e)
            {
                GravarLogErro("RepositorioBase", "ObterPeloCodigo", e);
            }
            return tEntity;
        }
        #endregion

        #region Obter Lista de Registros no Banco de Dados
        public virtual IEnumerable<TEntidade> ObterTodosRegistros()
        {
            IEnumerable<TEntidade> tEntity = null;
            try
            {
                tEntity = conexaoDB.GetAll<TEntidade>();
            }
            catch (Exception e)
            {
                GravarLogErro("RepositorioBase", "ObterTodosRegistros", e);
            }

            return tEntity;
        }
        #endregion

        #region  Insere registro  no banco de dados e registra a atividade do usuário
        /// <summary>
        /// Insere registro  no banco de dados e registra a atividade do usuário
        /// </summary>
        /// <param name="objeto"></param>
        /// <param name="registroAtividade"></param>
        /// <returns></returns>
        public virtual async Task<Retorno> InserirAsync(TEntidade objeto, UsuariosRegistroAtividade registroAtividade = null)
        {
            try
            {
                long codigoRegistro = (long)await conexaoDB.InsertAsync(objeto);
                if (codigoRegistro > 0)
                {
                    if (registroAtividade != null)
                    {
                        var retornoRegistroAtividade = await InserirRegistroAtividadeAsync(registroAtividade);
                        if (retornoRegistroAtividade.Status) return new Retorno(codigoRegistro);
                    }
                    else
                    {
                        return new Retorno(codigoRegistro);
                    }
                }
            }
            catch (Exception e)
            {
                GravarLogErro("RepositorioBase", "InserirAsync com Registro de atividade", e);
                return new Retorno();
            }

            return new Retorno("Não foi possível efetuar a inclusão do registro!");
        }
        #endregion

        #region Atualiza registro  no banco de dados e registra a atividade do usuário
        /// <summary>
        /// Atualiza registro no banco de dados e registra a atividade do usuário
        /// </summary>
        /// <param name="objeto"></param>
        /// <param name="registroAtividade"></param>
        /// <returns></returns>
        public virtual async Task<Retorno> AtualizarAsync(TEntidade objeto, UsuariosRegistroAtividade registroAtividade = null)
        {
            try
            {
                var resultado = await conexaoDB.UpdateAsync(objeto);
                if (resultado)
                {
                    if (registroAtividade != null)
                    {
                        var retornoRegistroAtividade = await InserirRegistroAtividadeAsync(registroAtividade);
                        if (retornoRegistroAtividade.Status) return new Retorno(resultado);
                    }
                    else
                    {
                        return new Retorno(resultado);
                    }
                }
            }
            catch (Exception e)
            {
                GravarLogErro("RepositorioBase", "AtualizarAsync com Registro de atividade", e);
                return new Retorno();
            }

            return new Retorno("Não foi possível atualizar o registro!");
        }
        #endregion

        #region Exclui registro  no banco de dados e registra a atividade do usuário
        /// <summary>
        /// Exclui regitro no banco de dados e registra a atividade do usuário
        /// </summary>
        /// <param name="codigo"></param>
        /// <param name="registroAtividade"></param>
        /// <returns></returns>
        public virtual async Task<Retorno> ExcluirAsync(long codigo, UsuariosRegistroAtividade registroAtividade = null)
        {
            try
            {
                var resultado = await conexaoDB.DeleteAsync(conexaoDB.Get<TEntidade>(codigo));
                if (resultado)
                {
                    if (registroAtividade != null)
                    {
                        var retornoRegistroAtividade = await InserirRegistroAtividadeAsync(registroAtividade);
                        if (retornoRegistroAtividade.Status) return new Retorno(resultado);
                    }
                    else
                    {
                        return new Retorno(resultado);
                    }
                }
            }
            catch (Exception e)
            {
                GravarLogErro("RepositorioBase", "ExcluirAsync com Registro de atividade", e);
                if (e.Message.Contains("violates foreign key constraint"))
                    return new Retorno("Registro está vinculado em outros recursos do sistema.");
            }

            return new Retorno("Não foi possível excluir o registro!");
        }
        #endregion

        #region Grava registro de atividade do usuário Async
        public async Task<Retorno> InserirRegistroAtividadeAsync(UsuariosRegistroAtividade registroAtividade)
        {
            var retornoFalha = new Retorno("Não foi possível salvar as informações do registro de atividade do usuário!");
            try
            {
                long codigoRegistro = (long)await conexaoDB.InsertAsync(registroAtividade);
                if (codigoRegistro > 0)
                {
                    foreach (var registroAtividadeEmpresa in registroAtividade.Empresas)
                    {
                        registroAtividadeEmpresa.CodigoRegistroAtividade = codigoRegistro;
                        long retornoRegistroAtividadeEmpresa = (long)await conexaoDB.InsertAsync(registroAtividadeEmpresa);
                        if (retornoRegistroAtividadeEmpresa <= 0) return new Retorno("Não foi possível salvar as empresas vinculadas ao registro de atividade.");
                    }
                    return new Retorno(codigoRegistro);
                }
                return retornoFalha;
            }
            catch (Exception e)
            {
                GravarLogErro("RepositorioBase", "InserirRegistroAtividadeAsync", e);
                return retornoFalha;
            }
        }
        #endregion

        #region Inserir registro de atividade Async
        public async Task<Retorno> InserirRegistroAtividadeAsync(UsuariosRegistroAtividade registroAtividade, string detalhe)
        {
            if (registroAtividade != null)
            {
                var registroAtividadeNovo = new UsuariosRegistroAtividade();

                registroAtividadeNovo.CodigoUsuario = registroAtividade.CodigoUsuario;
                registroAtividadeNovo.Empresas = registroAtividade.Empresas;
                registroAtividadeNovo.Recurso = registroAtividade.Recurso;
                registroAtividadeNovo.DataHora = registroAtividade.DataHora;
                registroAtividadeNovo.Detalhe = detalhe;
                return await InserirRegistroAtividadeAsync(registroAtividadeNovo);
            }
            else
            {
                return new Retorno(true);
            }
        }
        #endregion

        #region Atualizar registro Async
        public virtual async Task<Retorno> AtualizarAsync(TEntidade objeto)
        {
            try
            {
                var resultado = await conexaoDB.UpdateAsync(objeto);

                return new Retorno(resultado);
            }
            catch (Exception e)
            {
                GravarLogErro("RepositorioBase", "AtualizarAsync", e);
                return new Retorno();
            }
        }
        #endregion
        
        #region Exclui registro no banco de dados Async
        public virtual async Task<Retorno> ExcluirAsync(long codigo)
        {
            try
            {
                var resultado = await conexaoDB.DeleteAsync(conexaoDB.Get<TEntidade>(codigo));
                return new Retorno(resultado);
            }
            catch (Exception e)
            {
                GravarLogErro("RepositorioBase", "ExcluirAsync", e);
                return new Retorno();
            }
        }
        #endregion

        #region Exclusão em Massa Async
        /// <summary>
        /// Efetua exclusão em massa no banco de dados com o preenchimento obrigatório das chaves primárias
        /// Funciona somente em objetos cujo a chave primária foi mapeada na propriedade com Nome: Codigo
        /// </summary>
        /// <typeparam name="Type"></typeparam>
        /// <param name="chavesPrimarias">Lista de códigos</param>
        /// <param name="condicaoAdicional">Ex: AND condicao</param>
        /// <returns></returns>
        public virtual async Task<Retorno> ExcluirEmMassaAsync<Type>(List<long> chavesPrimarias, string condicaoAdicional = "")
        {
            try
            {
                if (VerificaListaCodigos(chavesPrimarias))
                {
                    // Obtem nome tabela do objeto                
                    string nomeTabela = ObterNomeTabelaDB<Type>();
                    // Obtem nome da coluna da chave primaria da tabela
                    string nomecolunaChavePrimaria = ObterNomeColunaDB<Type>("Codigo");

                    if (!string.IsNullOrEmpty(nomeTabela) && !string.IsNullOrEmpty(nomecolunaChavePrimaria))
                    {
                        await conexaoDB.ExecuteAsync(" DELETE FROM " + nomeTabela + " WHERE " + nomecolunaChavePrimaria + " IN (" + string.Join(", ", chavesPrimarias) + ") " + condicaoAdicional + "; ");
                        return new Retorno(true);
                    }
                }
            }
            catch (Exception e)
            {
                GravarLogErro("RepositorioBase", "ExcluirEmMassaAsync", e);
                if (e.Message.Contains("violates foreign key constraint"))
                    return new Retorno("Registro está vinculado em outros recursos do sistema.");
            }
            return new Retorno("Não foi possível excluir os registros.");
        }
        #endregion

        #region Efetua exclusão em massa no banco de dados por uma condição customizada
        /// <summary>
        /// Efetua exclusão em massa no banco de dados pela por uma condição customizada        
        /// </summary>
        /// <typeparam name="Type"></typeparam>        
        /// <param name="condicao">Ex: pk_tabela = codigo ou fk_tabela = codigo </param>
        /// <returns></returns>
        public virtual async Task<Retorno> ExcluirEmMassaAsync<Type>(string condicao)
        {
            try
            {
                if (!string.IsNullOrEmpty(condicao))
                {
                    // Obtem nome tabela do objeto                
                    string nomeTabela = ObterNomeTabelaDB<Type>();
                    if (!string.IsNullOrEmpty(nomeTabela))
                    {
                        await conexaoDB.ExecuteAsync(" DELETE FROM " + nomeTabela + " WHERE " + condicao + "; ");
                        return new Retorno(true);
                    }
                }
            }
            catch (Exception e)
            {
                GravarLogErro("RepositorioBase", "ExcluirEmMassaAsync", e);
                if (e.Message.Contains("violates foreign key constraint"))
                    return new Retorno("Registro está vinculado em outros recursos do sistema.");
            }
            return new Retorno("Não foi possível excluir os registros.");
        }
        #endregion

        #region Validar Entidades Referenciadas
        public virtual Task<Retorno> ValidarEntidadesReferenciadas(TEntidade objeto)
        {
            return Task.FromResult(new Retorno(true));
        }
        #endregion

        #region Obter pelo código Async
        public virtual async Task<TEntidade> ObterPeloCodigoAsync(long codigo)
        {
            TEntidade tEntity = null;
            try
            {
                tEntity = await conexaoDB.GetAsync<TEntidade>(codigo);
            }
            catch (Exception e)
            {
                GravarLogErro("RepositorioBase", "ObterPeloCodigoAsync", e);
            }
            return tEntity;
        }
        #endregion

        #region Obter registro pelo código Async
        public virtual async Task<TEntidade> ObterPeloCodigoLazyAsync(long codigo)
        {
            return await ObterPeloCodigoAsync(codigo);
        }
        #endregion

        #region Obter todos os registros Async
        public virtual async Task<IEnumerable<TEntidade>> ObterTodosRegistrosAsync()
        {
            IEnumerable<TEntidade> tEntity = null;
            try
            {
                tEntity = await conexaoDB.GetAllAsync<TEntidade>();
            }
            catch (Exception e)
            {
                GravarLogErro("RepositorioBase", "ObterTodosRegistrosAsync", e);
            }
            return tEntity;
        }
        #endregion

        #region Ajustar formato de data para o banco de dados
        public string DataToDB(DateTime data)
        {
            return data.ToString("yyyy-MM-dd");
        }
        
        public string DataHoraMinutoToDB(DateTime data)
        {
            return data.ToString("yyyy-MM-dd HH:mm");
        }

        public string DataHoraMinutoSegundoToDB(DateTime data)
        {
            return data.ToString("yyyy-MM-dd HH:mm:ss");
        }
        #endregion

        #region Impedir que o objeto seja salvo com o código zerado
        private string TratarValorMonetarioDB(string valor)
        {
            string valorTratado = "0";
            try
            {
                valorTratado = valor.ToString().Replace(".", "");
                valorTratado = valor.ToString().Replace(",", ".");
            }
            catch (Exception e)
            {
                GravarLogErro("RepositorioBase", "TratarValorDecimal", e);
            }

            return valorTratado;
        }
        
        public string DecimalToDB(decimal valor)
        {
            try
            {
                return TratarValorMonetarioDB(valor.ToString());
            }
            catch (Exception e)
            {
                GravarLogErro("RepositorioBase", "DecimalToDB", e);
            }

            return "0";
        }
         
        public string DoubleToDB(double valor)
        {
            try
            {
                return TratarValorMonetarioDB(valor.ToString());
            }
            catch (Exception e)
            {
                GravarLogErro("RepositorioBase", "DoubleToDB", e);
            }

            return "0";
        }
        #endregion

        #region Gravar log de erro no banco de dados
        public void GravarLogErro(string repositorio, string metodo, Exception e)
        {
            logger.Error("Erro: " + repositorio + " > Método: " + metodo + " Detalhes: " + e.Message);
        }

        public void GravarLogErro(string repositorio, string metodo, string detalhes)
        {
            logger.Error("Erro: " + repositorio + " > Método: " + metodo + " Detalhes: " + detalhes);
        }
        #endregion

        #region Verificar se o objeto existe no banco de dados
        public bool VerificaListaCodigos(List<long> listaCodigos)
        {
            if (listaCodigos != null)
                return listaCodigos.Count > 0;

            return false;
        }

        ~RepositorioBase() => Dispose();

        private bool _disposed = false;

        public void Dispose()
        {
            try
            {
                if (conexaoDB != null)
                {
                    if (!_disposed)
                    {
                        conexaoDB.Close();
                        conexaoDB.Dispose();
                        _disposed = true;
                    }
                    GC.SuppressFinalize(this);
                }
            }
            catch (Exception e)
            {
                GravarLogErro("RepositorioBase", "Dispose", e);
            }
            #endregion
            
        }
    }
}