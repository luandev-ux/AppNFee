namespace AppNFe.Core.DominioProblema
{
    public class RetornoArquivo
    {
        public string Arquivo { get; set; }
        public string LinkArquivo { get; set; }
        public string TamanhoApresentacao { get; set; }        
        public long CodigoArquivo { get; set; }
        public EStatusRetornoRequisicao Status { get; set; }
        public string Mensagem { get; set; }

        public bool VerificarSucesso()
        {
            return Status == EStatusRetornoRequisicao.Sucesso;
        }

        public void PreencherComRetornoPermissao(RetornoRequisicao retornoRequisicao)
        {
            this.Status = retornoRequisicao.Status;
            this.Mensagem = retornoRequisicao.Mensagem;
        }
    }
}
