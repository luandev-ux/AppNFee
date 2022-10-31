using System.Collections.Generic;

namespace AppNFe.Core.Persistencia.Consulta
{
    public class ParametrosConsultaRapida
    {
        public List<long> Empresas { get; set; }
        public string RecursoAssociado { get; set; }
        public string Valor { get; set; }
        public List<string> Valores { get; set; }
        public bool FiltrarPorCodigo { get; set; }
        public bool FiltrarPorVariosCodigos { get; set; }
        public bool FiltrarTextoExato { get; set; }
        public bool ApenasAtivos { get; set; }
        public int QuantidadeRegistros { get; set; } = 10;
    }
}
