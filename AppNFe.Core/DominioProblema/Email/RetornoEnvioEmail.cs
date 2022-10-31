namespace AppNFe.Core.DominioProblema.Email
{
    public class RetornoEnvioEmail
    {
        public bool Enviado { get; set; }
        public string Mensagem { get; set; }
        public RetornoEnvioEmail(bool enviado)
        {
            Enviado = enviado;
        }
        public RetornoEnvioEmail(bool enviado, string mensagem)
        {
            Enviado = enviado;
            Mensagem = mensagem;
        }

    }
}
