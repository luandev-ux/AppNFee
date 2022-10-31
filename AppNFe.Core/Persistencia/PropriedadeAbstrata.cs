using System.Text.Json.Serialization;

namespace AppNFe.Core.Persistencia
{
    public abstract class PropriedadeAbstrata
    {
        /// <summary>
        /// Identificador único para mapear dados no Backend - CampoDB, Tipo dado e outros.
        /// </summary>
        public string Identificador { get; set; }
        /// <summary>
        /// Texto para apresentação no FrontEnd - Combo, Tags e outros elementos para melhor interpretação do usuário final.
        /// </summary>
        public string Apresentacao { get; set; }
        [JsonIgnore]
        public string CampoDB { get; set; }
        /// <summary>
        /// Valores disponíveis no enumerador: ETipoDado 
        /// </summary>
        public byte TipoDado { get; set; }        
    }
}
