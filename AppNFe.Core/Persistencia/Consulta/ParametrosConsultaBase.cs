using System.Collections.Generic;

namespace AppNFe.Core.Persistencia.Consulta
{
    public abstract class ParametrosConsultaBase
    {
        public List<long> Empresas { get; set; }
        public int QtdeRegistrosTotal { get; set; } = 100;
        public int QtdeRegistrosPagina { get; set; } = 10;
        public int NumeroPagina { get; set; } = 1;
        public List<string> Ordenacao { get; set; }
        public List<long> CodigosSelecionados { get; set; }
        public void ObterTodosRegistros()
        {
            this.QtdeRegistrosTotal = 9999999;
            this.QtdeRegistrosPagina = 9999999;
        }
    }
}
