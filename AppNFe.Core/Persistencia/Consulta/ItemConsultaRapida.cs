namespace AppNFe.Core.Persistencia.Consulta
{
    /// <summary>
    /// Utilizado em consultas rápidas como auto-completar, cadastros associados e outros.
    /// </summary>
    public class ItemConsultaRapida
    {
        /// <summary>
        /// Código do registro no banco de dados ou número único que identifica este item.
        /// </summary>
        public long Codigo { get; set; }
        /// <summary>
        /// Nome, título, descrição ou informações que melhor descreve este item.
        /// </summary>
        public string TextoIdentificacao { get; set; }
    }
}
