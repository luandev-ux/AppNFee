namespace AppNFe.Core.DominioProblema
{
    public class RetornoRelatorio
    {
        public EStatusRetornoRequisicao Status { get; set; }
        public string Link { get; set; }
        public string Mensagem { get; set; }

        public RetornoRelatorio(string link, EStatusRetornoRequisicao status, string mensagem)
        {
            Link = link;
            Status = status;
            Mensagem = mensagem;
        }
    }
}
