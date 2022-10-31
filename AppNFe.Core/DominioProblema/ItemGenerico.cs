using AppNFe.Core.Enumeradores;

namespace AppNFe.Core
{
    /// <summary>
    /// Utilizado em lista de enumeradores, tabelas com valores fixos ou dinâmicos.
    /// </summary>
    public class ItemGenerico
    {
        /// <summary>
        /// Tipo de dado do identificador
        /// </summary>
        /// 
        public ETipoDado TipoDadoIdentificador { get; set; }
        /// <summary>
        /// Identificador do item
        /// </summary>
        /// 
        public string Identificador { get; set; }
        /// <summary>
        /// Nome, título, descrição ou informações que melhor descreve este item.
        /// </summary>
        public string Descricao { get; set; }
    }
}
