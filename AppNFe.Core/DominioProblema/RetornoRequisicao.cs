namespace AppNFe.Core.DominioProblema
{
    public enum EStatusRetornoRequisicao
    {
        Sucesso = 1,
        Alerta = 2,
        Erro = 3
    }

    public class RetornoRequisicao
    {
        public long CodigoRegistro { get; set; }
        public EStatusRetornoRequisicao Status { get; set; }
        public string Mensagem { get; set; }

        public RetornoRequisicao(Retorno retorno)
        {
            CodigoRegistro = retorno.CodigoRegistro;
            Mensagem = retorno.Mensagem;
            if (retorno.Status)
            {
                Status = EStatusRetornoRequisicao.Sucesso;
            }
            else
            {
                Status = EStatusRetornoRequisicao.Erro;
            }
        }

        public RetornoRequisicao(long codigoRegistro, EStatusRetornoRequisicao status, string mensagem)
        {
            CodigoRegistro = codigoRegistro;
            Status = status;
            Mensagem = mensagem;
        }

        public RetornoRequisicao(string mensagem)
        {
            Status = EStatusRetornoRequisicao.Alerta;
            Mensagem = mensagem;
        }

        public bool VerificarSucesso()
        {
            return Status == EStatusRetornoRequisicao.Sucesso;
        }
    }
}
