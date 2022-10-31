using FastReport.Format;
using FastReport.Utils;
using AppNFe.Dominio.Relatorios;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace FastReport.ReportBuilder
{
    /// <summary>
    /// Get instance for report builder
    /// </summary>
    public class ReportBuilder
    {
        /// <summary>
        /// Build a report
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        public ReportBuilder<T> Report<T>(IEnumerable<T> data) where T : class
        {
            return new ReportBuilder<T>(data);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public static class ReportBuilderExtension
    {
        ///// <summary>
        ///// Prepare report when finished
        ///// </summary>
        ///// <typeparam name="T"></typeparam>
        ///// <param name="reportBuilder"></param>
        ///// <returns></returns>
        //public static Report Prepare<T>(this ReportBuilder<T> reportBuilder)
        //{
        //    var report = new Report();
        //    var name = typeof(T).Name;
        //    report.RegisterData(reportBuilder._data, name);
        //    report.GetDataSource(name).Enabled = true;

        //    ReportPage page = new ReportPage();
        //    report.Pages.Add(page);
        //    page.CreateUniqueName();

        //    page.ReportTitle = new ReportTitleBand();
        //    page.ReportTitle.Height = Units.Centimeters * 1;
        //    page.ReportTitle.CreateUniqueName();
        //    page.ReportTitle.Visible = reportBuilder._reportTitle.Visible;

        //    TextObject titleText = new TextObject();
        //    titleText.Parent = page.ReportTitle;
        //    titleText.CreateUniqueName();
        //    titleText.Bounds = new RectangleF(Units.Centimeters * 5, 0, Units.Centimeters * 10, Units.Centimeters * 1);
        //    titleText.Font = reportBuilder._reportTitle.Font;
        //    titleText.Text = reportBuilder._reportTitle.Text;
        //    titleText.TextColor = reportBuilder._reportTitle.TextColor;
        //    titleText.FillColor = reportBuilder._reportTitle.FillColor;
        //    titleText.HorzAlign = HorzAlign.Center;          

        //    DataBand dataBand = new DataBand();
        //    dataBand.Parent = page;
        //    dataBand.CreateUniqueName();
        //    dataBand.DataSource = report.GetDataSource(name);
        //    dataBand.Height = Units.Centimeters * 0.5f;

        //    if (reportBuilder._groupHeader.Visible)
        //    {
        //        GroupHeaderBand groupHeader = new GroupHeaderBand();
        //        groupHeader.CreateUniqueName();
        //        groupHeader.Height = Units.Centimeters * 0.5f;
        //        groupHeader.Condition = string.IsNullOrEmpty(reportBuilder._groupHeader.Expression)
        //            ? $"[{name}.{reportBuilder._groupHeader.Name}]"
        //            : string.Format(reportBuilder._groupHeader.Expression, $"[{name}.{reportBuilder._groupHeader.Name}]");
        //        groupHeader.Data = dataBand;
        //        groupHeader.SortOrder = reportBuilder._groupHeader.SortOrder;
        //        groupHeader.Parent = page;

        //        if (reportBuilder._groupHeader.TextVisible)
        //        {
        //            TextObject textGroupHeader = new TextObject();
        //            textGroupHeader.CreateUniqueName();
        //            textGroupHeader.Bounds = new RectangleF(0, 0, Units.Centimeters * 2, Units.Centimeters * 0.5f);
        //            textGroupHeader.Text = $"[{groupHeader.Condition}]";
        //            textGroupHeader.Font = new Font("Tahoma", 10, FontStyle.Bold);
        //            textGroupHeader.Parent = groupHeader;
        //        }
        //    }

        //    var dataHeaderBand = new DataHeaderBand();
        //    if (reportBuilder._dataHeader.Visible)
        //    {
        //        dataHeaderBand.Parent = dataBand;
        //        dataHeaderBand.CreateUniqueName();
        //        dataHeaderBand.Height = Units.Centimeters * 0.5f;
        //    }

        //    float leftCm = 0.0f;
        //    float size = 0.0f;
        //    float pageWidth = page.PaperWidth - (page.LeftMargin + page.RightMargin);
        //    float cellWidth = pageWidth / 100;
        //    var remainColumn = reportBuilder._columns.Count(a => a.Width == 0);
        //    float remainSize = 100 - reportBuilder._columns.Sum(a => a.Width);
        //    float remainWidth = remainSize / (remainColumn * 10);

        //    foreach (var item in reportBuilder._columns)
        //    {
        //        size = item.Width == 0 ? remainWidth : (float)item.Width / 10;
        //        if (reportBuilder._dataHeader.Visible)
        //        {
        //            TextObject headerText = new TextObject();
        //            headerText.CreateUniqueName();
        //            headerText.Bounds = new RectangleF(leftCm, 0f * Units.Centimeters, cellWidth * Units.Centimeters * size, 0.1f * Units.Centimeters);
        //            headerText.VertAlign = reportBuilder._reportTitle.VertAlign ?? reportBuilder._report.VertAlign;
        //            headerText.HorzAlign = reportBuilder._reportTitle.HorzAlign ?? reportBuilder._report.HorzAlign;
        //            headerText.Font = reportBuilder._dataHeader.Font;
        //            headerText.TextColor = reportBuilder._dataHeader.TextColor;
        //            headerText.FillColor = reportBuilder._dataHeader.FillColor;
        //            headerText.Border.Lines = BorderLines.All;
        //            headerText.Text = item.Title;
        //            headerText.GrowToBottom = true;
        //            headerText.Parent = dataHeaderBand;
        //        }

        //        TextObject text = new TextObject();
        //        text.Parent = dataBand;
        //        text.CreateUniqueName();
        //        text.Bounds = new RectangleF(leftCm, 0, Units.Centimeters * cellWidth * size, Units.Centimeters * 0.5f);
        //        text.Text = string.IsNullOrEmpty(item.Expression)
        //            ? $"[{name}.{item.Name}]"
        //            : string.Format($"[{item.Expression}]", $"[{name}.{item.Name}]");
        //        text.Border.Lines = BorderLines.All;
        //        text.TextColor = Color.Black;
        //        text.VertAlign = item.VertAlign ?? reportBuilder._report.VertAlign;
        //        text.HorzAlign = item.HorzAlign ?? reportBuilder._report.HorzAlign;

        //        if (!string.IsNullOrEmpty(item.Format))
        //        {
        //            CustomFormat format = new CustomFormat();
        //            format.Format = item.Format;
        //            text.Format = format;
        //        }

        //        leftCm += cellWidth * Units.Centimeters * size;
        //    }

        //    report.Prepare();

        //    return report;
        //}

        /// <summary>
        /// Prepare report when finished
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="reportBuilder"></param>
        /// <returns></returns>
        public static Report Prepare<T>(this ReportBuilder<T> reportBuilder, ConfiguracaoRelatorio configuracaoRelatorio)
        {
            var report = new Report();
            var name = typeof(T).Name;
            report.RegisterData(reportBuilder._data, name);
            report.GetDataSource(name).Enabled = true;
            bool variasPaginas = false;            
            variasPaginas = reportBuilder._data.Count() > 50;
            
            ReportPage page = new ReportPage();
            if (configuracaoRelatorio.Paisagem)
                page.Landscape = true;

            report.Pages.Add(page);
            page.CreateUniqueName();

            if (configuracaoRelatorio.TipoArquivo != AppNFe.Core.Enumeradores.ETipoArquivo.CSV)
            {
                page.ReportTitle = new ReportTitleBand();
                page.ReportTitle.Height = 60;
                page.ReportTitle.CreateUniqueName();
                page.ReportTitle.Visible = reportBuilder._reportTitle.Visible;

                var fontNegrito = new Font("Tahoma", 10, FontStyle.Bold);
                var fontRegular = new Font("Tahoma", 10, FontStyle.Regular);

                TextObject lblEmpresa = new TextObject();
                lblEmpresa.Parent = page.ReportTitle;
                lblEmpresa.CreateUniqueName();
                lblEmpresa.Bounds = new RectangleF(0, 9.45f, 85f, 18.9f);
                lblEmpresa.Font = fontNegrito;
                lblEmpresa.Text = configuracaoRelatorio.VariasEmpresas ? "Empresas:" : "Empresa:";
                lblEmpresa.HorzAlign = HorzAlign.Left;
                lblEmpresa.VertAlign = VertAlign.Center;

                TextObject txtEmpresa = new TextObject();
                txtEmpresa.Parent = page.ReportTitle;
                txtEmpresa.CreateUniqueName();
                txtEmpresa.Bounds = new RectangleF(85f, 9.45f, 623.7f, 18.9f);
                txtEmpresa.Font = fontRegular;
                txtEmpresa.Text = configuracaoRelatorio.Empresa;
                txtEmpresa.HorzAlign = HorzAlign.Left;
                txtEmpresa.VertAlign = VertAlign.Center;

                TextObject lblRelatorio = new TextObject();
                lblRelatorio.Parent = page.ReportTitle;
                lblRelatorio.CreateUniqueName();
                lblRelatorio.Bounds = new RectangleF(0, 30f, 85f, 18.9f);
                lblRelatorio.Font = fontNegrito;
                lblRelatorio.Text = "Relatório:";
                lblRelatorio.HorzAlign = HorzAlign.Left;
                lblRelatorio.VertAlign = VertAlign.Center;

                TextObject txtRelatorio = new TextObject();
                txtRelatorio.Parent = page.ReportTitle;
                txtRelatorio.CreateUniqueName();
                txtRelatorio.Bounds = new RectangleF(85f, 30f, 292.95f, 18.9f);
                txtRelatorio.Font = fontRegular;
                txtRelatorio.Text = configuracaoRelatorio.Titulo;
                txtRelatorio.HorzAlign = HorzAlign.Left;
                txtRelatorio.VertAlign = VertAlign.Center;

                TextObject txtEmissao = new TextObject();
                txtEmissao.Parent = page.ReportTitle;
                txtEmissao.CreateUniqueName();
                txtEmissao.Bounds = new RectangleF(415.45f, 30f, 190.25f, 18.9f);
                txtEmissao.Font = fontRegular;
                txtEmissao.Text = "Emitido:";
                txtEmissao.HorzAlign = HorzAlign.Right;
                txtEmissao.VertAlign = VertAlign.Center;

                TextObject txtData = new TextObject();
                txtData.Parent = page.ReportTitle;
                txtData.CreateUniqueName();
                txtData.Bounds = new RectangleF(603.7f, 30f, 124.5f, 18.9f);
                txtData.Font = fontRegular;
                txtData.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
                txtData.HorzAlign = HorzAlign.Left;
                txtData.VertAlign = VertAlign.Center;
            }
                        
            DataBand dataBand = new DataBand();
            dataBand.Parent = page;            
            dataBand.CreateUniqueName();
            dataBand.DataSource = report.GetDataSource(name);
            dataBand.Height = Units.Centimeters * 0.5f;

            if (reportBuilder._groupHeader.Visible)
            {
                GroupHeaderBand groupHeader = new GroupHeaderBand();
                groupHeader.CreateUniqueName();
                groupHeader.Height = Units.Centimeters * 0.5f;
                groupHeader.Condition = string.IsNullOrEmpty(reportBuilder._groupHeader.Expression)
                    ? $"[{name}.{reportBuilder._groupHeader.Name}]"
                    : string.Format(reportBuilder._groupHeader.Expression, $"[{name}.{reportBuilder._groupHeader.Name}]");
                groupHeader.Data = dataBand;
                groupHeader.SortOrder = reportBuilder._groupHeader.SortOrder;
                groupHeader.Parent = page;

                if (reportBuilder._groupHeader.TextVisible)
                {
                    TextObject textGroupHeader = new TextObject();
                    textGroupHeader.CreateUniqueName();
                    textGroupHeader.Bounds = new RectangleF(0, 0, Units.Centimeters * 2, Units.Centimeters * 0.5f);
                    textGroupHeader.Text = $"[{groupHeader.Condition}]";
                    textGroupHeader.Font = new Font("Tahoma", 10, FontStyle.Bold);
                    textGroupHeader.VertAlign = VertAlign.Bottom;
                    textGroupHeader.Parent = groupHeader;
                }
            }

            var dataHeaderBand = new DataHeaderBand();
            if (reportBuilder._dataHeader.Visible)
            {
                dataHeaderBand.Parent = dataBand;
                dataHeaderBand.CreateUniqueName();
                dataHeaderBand.Height = Units.Centimeters * 0.5f;
            }

            float leftCm = 0.0f;
            float size = 0.0f;
            float pageWidth = page.PaperWidth - (page.LeftMargin + page.RightMargin);
            float cellWidth = pageWidth / 100;
            var remainColumn = reportBuilder._columns.Count(a => a.Width == 0);
            float remainSize = 100 - reportBuilder._columns.Sum(a => a.Width);
            float remainWidth = remainSize / (remainColumn * 10);

            foreach (var item in reportBuilder._columns)
            {
                size = item.Width == 0 ? remainWidth : (float)item.Width / 10;
                if (reportBuilder._dataHeader.Visible)
                {
                    TextObject headerText = new TextObject();
                    headerText.CreateUniqueName();
                    headerText.Bounds = new RectangleF(leftCm, 0f * Units.Centimeters, cellWidth * Units.Centimeters * size, 0.1f * Units.Centimeters);
                    headerText.VertAlign = item.VertAlign ?? reportBuilder._report.VertAlign;
                    headerText.HorzAlign = item.HorzAlign ?? reportBuilder._report.HorzAlign;
                    headerText.Font = reportBuilder._dataHeader.Font;
                    headerText.TextColor = reportBuilder._dataHeader.TextColor;
                    headerText.FillColor = reportBuilder._dataHeader.FillColor;
                    headerText.Border.Lines = BorderLines.All;
                    headerText.Text = item.Title;
                    headerText.GrowToBottom = true;
                    headerText.Parent = dataHeaderBand;
                }

                TextObject text = new TextObject();
                text.Parent = dataBand;
                text.CreateUniqueName();
                text.Bounds = new RectangleF(leftCm, 0, Units.Centimeters * cellWidth * size, Units.Centimeters * 0.5f);
                if (item.LabelBoolean != null)
                {                    
                    text.Text = $"[IIf([{name}.{item.Name}], \"" + item.LabelBoolean.LabelCheck + "\", \"" + item.LabelBoolean.LabelUnCheck + "\")]";
                }
                else
                {
                    text.Text = string.IsNullOrEmpty(item.Expression)
                    ? $"[{name}.{item.Name}]"
                    : string.Format($"[{item.Expression}]", $"[{name}.{item.Name}]");
                }
                
                text.Border.Lines = BorderLines.All;
                text.TextColor = Color.Black;
                text.VertAlign = item.VertAlign ?? reportBuilder._report.VertAlign;
                text.HorzAlign = item.HorzAlign ?? reportBuilder._report.HorzAlign;
                
                if (!string.IsNullOrEmpty(item.Format))
                {
                    CustomFormat format = new CustomFormat();
                    format.Format = item.Format;
                    text.Format = format;
                }

                leftCm += cellWidth * Units.Centimeters * size;
            }

            if (configuracaoRelatorio.TipoArquivo != AppNFe.Core.Enumeradores.ETipoArquivo.CSV && configuracaoRelatorio.TipoArquivo != AppNFe.Core.Enumeradores.ETipoArquivo.Excel)
            {
                var fontRegularRodape = new Font("Tahoma", 9, FontStyle.Regular);
                page.PageFooter = new PageFooterBand();
                page.PageFooter.Height = 18.9f;
                page.PageFooter.CreateUniqueName();
                TextObject txtRodape = new TextObject();
                txtRodape.Parent = page.PageFooter;
                txtRodape.CreateUniqueName();
                if(configuracaoRelatorio.Paisagem)
                    txtRodape.Bounds = new RectangleF(0, 0, 1040.0f, 18.9f);
                else
                    txtRodape.Bounds = new RectangleF(0, 0, 718.2f, 18.9f);
                txtRodape.Border.Lines = BorderLines.Top;
                txtRodape.Font = fontRegularRodape;
                txtRodape.Text = "Sabre Sistemas - Soluções para alavancar o seu negócio!";
                txtRodape.HorzAlign = HorzAlign.Center;
                txtRodape.VertAlign = VertAlign.Center;

                if (variasPaginas)
                {
                    TextObject txtPagina = new TextObject();
                    txtPagina.Parent = page.PageFooter;
                    txtPagina.CreateUniqueName();
                    txtPagina.Bounds = new RectangleF(523.7f, 0, 150f, 18.9f);
                    txtPagina.Font = fontRegularRodape;
                    txtPagina.Text = "Página: [Page] de [TotalPages#]";
                    txtPagina.HorzAlign = HorzAlign.Right;
                    txtPagina.VertAlign = VertAlign.Center;
                }
            }
             
            report.Prepare();
            return report;
        }
    }
}
