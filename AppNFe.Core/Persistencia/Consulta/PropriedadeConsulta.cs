using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppNFe.Core.Persistencia.Consulta
{
    public class PropriedadeConsulta: PropriedadeAbstrata
    {
        public bool Filtro { get; set; }
        public bool Ordenacao { get; set; }
        public string RecursoAssociado { get; set; }
        public List<ItemGenerico> ListaItens { get; set; }
    }
}
