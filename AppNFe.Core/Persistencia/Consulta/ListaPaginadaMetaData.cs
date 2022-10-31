namespace AppNFe.Core.Persistencia.Consulta
{
    public class ListaPaginadaMetaData
    {
        public int PaginaAtual { get; set; }
        public int TotalPaginas { get; set; }
        public int QtdeRegistrosPagina { get; set; }
        public int TotalRegistros { get; set; }
        public bool PossuiPaginaAnterior => PaginaAtual > 1;
        public bool PossuiProximaPagina => PaginaAtual < TotalPaginas;
    }
}
