using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace AppNFe.Core.Persistencia
{
    /// <summary>
    /// Filtro utilizado em Consultas, Relatórios e Fluxo de trabalho
    /// </summary>
    public class FiltroGenerico : PropriedadeAbstrata
    {       
        /// <summary>
        /// Valores disponíveis no enumerador: ECondicao
        /// </summary>
        public byte Condicao { get; set; }        
        public List<string> Valores { get; set; }
        /// <summary>
        /// Valores disponíveis no enumerador: EOperador
        /// </summary>
        public byte Operador { get; set; }
        public FiltroGenerico()
        {
            Valores = new List<string>();
            Operador = (byte)Enumeradores.EOperadorLogico.E;
        }        
    }
}
