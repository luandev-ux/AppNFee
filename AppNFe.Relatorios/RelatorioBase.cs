using FastReport.ReportBuilder;
using FastReport.Export.Csv;
using FastReport.Export.Pdf;
using System.Text;
using FastReport;
using AppNFe.Core.Enumeradores;
using System;
using Serilog;
using AppNFe.Dominio.Relatorios;
using AppNFe.Core.DominioProblema;
using System.IO;

namespace AppNFe.Relatorios
{
    public abstract class RelatorioBase
    {
        protected ILogger Logger;
        public readonly ReportBuilder builder;

        public RelatorioBase(ILogger logger)
        {
            Logger = logger;
            builder = new ReportBuilder();
        }

        public void GravarLogErro(string servico, string metodo, Exception e)
        {
            Logger.Error("Erro: " + servico + " > Método: " + metodo + " Detalhes: " + e.Message);
        }
        public void GravarEstruturaObjetoRelatorio(string caminhoArquivo, Report relatorio)
        {
            try
            {
                using (FileStream fs = File.OpenWrite(caminhoArquivo))
                {
                    relatorio.Save(fs);
                }
            }
            catch (Exception e)
            {
                GravarLogErro("RelatorioBase", "GravarEstruturaObjetoRelatorio", e);
            }
        }

        public RetornoRelatorio GerarRelatorio(ConfiguracaoRelatorio configuracaoRelatorio, Report relatorio)
        {
            RetornoRelatorio retornoRelatorio;
            try
            {
                string arquivoCompleto = Path.Combine(configuracaoRelatorio.DiretorioServidor, configuracaoRelatorio.NomeArquivo + configuracaoRelatorio.ExtensaoArquivo);
                switch (configuracaoRelatorio.TipoArquivo)
                {
                    case ETipoArquivo.PDF:
                        PDFExport exportPDF = new PDFExport();
                        relatorio.Export(exportPDF, arquivoCompleto);
                        break;
                    case ETipoArquivo.Word:
                        FastReport.Export.OoXML.Word2007Export exportWord = new FastReport.Export.OoXML.Word2007Export();
                        relatorio.Export(exportWord, arquivoCompleto);
                        break;
                    case ETipoArquivo.Excel:
                        FastReport.Export.OoXML.Excel2007Export exportExcel = new FastReport.Export.OoXML.Excel2007Export();
                        relatorio.Export(exportExcel, arquivoCompleto);
                        break;
                    case ETipoArquivo.CSV:
                        CSVExport exportCSV = new CSVExport();
                        exportCSV.Encoding = Encoding.UTF8;
                        exportCSV.Separator = ";";
                        relatorio.Export(exportCSV, arquivoCompleto);
                        break;
                }
                string linkGerado = "";
                linkGerado = configuracaoRelatorio.UrlBaseApi + "/" + configuracaoRelatorio.DiretorioContratante + "/" + configuracaoRelatorio.NomeArquivo + configuracaoRelatorio.ExtensaoArquivo;
                linkGerado = linkGerado.Replace("\\", "/");
                retornoRelatorio = new RetornoRelatorio(linkGerado, EStatusRetornoRequisicao.Sucesso, "Relatório gerado com sucesso!");
            }
            catch (System.Exception e)
            {
                GravarLogErro("RelatorioBase", "GerarRelatorio", e);
                retornoRelatorio = new RetornoRelatorio("", EStatusRetornoRequisicao.Erro, "Desculpe-nos não conseguimos responder a sua solicitação no momento!");
            }

            return retornoRelatorio;
        }

        public DataDefinitionLabelBoolean LabelPadraoAtivoInativo()
        {
            return new DataDefinitionLabelBoolean { LabelCheck = "Ativo", LabelUnCheck = "Inativo" };
        }
        public DataDefinitionLabelBoolean LabelPadraoAtivaInativa()
        {
            return new DataDefinitionLabelBoolean { LabelCheck = "Ativa", LabelUnCheck = "Inativa" };
        }
        public DataDefinitionLabelBoolean LabelPadraoSimNao()
        {
            return new DataDefinitionLabelBoolean { LabelCheck = "Sim", LabelUnCheck = "Não" };
        }

    }
}
