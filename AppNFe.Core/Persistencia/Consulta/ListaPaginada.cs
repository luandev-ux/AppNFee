using System;
using System.Collections.Generic;
using System.Linq;

namespace AppNFe.Core.Persistencia.Consulta
{
    public class ListaPaginada <T> : List<T>
    {
        public ListaPaginadaMetaData MetaData { get; set; }

        public ListaPaginada(List<T> registros, int totalRegistros, int paginaAtual, int qtdeRegistrosPagina)
        {
            this.MetaData = new ListaPaginadaMetaData();

            this.MetaData.TotalRegistros = totalRegistros;
            this.MetaData.QtdeRegistrosPagina = qtdeRegistrosPagina;
            this.MetaData.PaginaAtual = paginaAtual;
            if (qtdeRegistrosPagina > 0)
                this.MetaData.TotalPaginas = (int)Math.Ceiling(totalRegistros / (double)qtdeRegistrosPagina);

            AddRange(registros);
        }
        public static ListaPaginada<T> ToListaPaginada(IEnumerable<T> items, int paginaAtual, int qtdeRegistrosPagina)
        {
            var totalRegistros = items.Count();
            var registros = items.ToList();

            if (qtdeRegistrosPagina > 0)
                registros = items.Skip((paginaAtual - 1) * qtdeRegistrosPagina).Take(qtdeRegistrosPagina).ToList();

            return new ListaPaginada<T>(registros, totalRegistros, paginaAtual, qtdeRegistrosPagina);
        }
    }
}
