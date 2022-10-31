namespace AppNFe.Core.Persistencia.Consulta
{
    public class EstruturaConsultaRapida
    {
        public string TabelaDB { get; set; }
        public string ColunaCodigoDB { get; set; }
        public string ColunaTextoIdentificacaoDB { get; set; }
        public string CondicaoEspecialTextoIdentificacaoDB { get; set; }
        public string CondicaoApenasAtivos { get; set; } = "";
        public string CondicaoAdicional { get; set; } = "";
    }
}
