namespace AppNFe.Core.DominioProblema
{
    public class Retorno
    {
        public long CodigoRegistro { get; set; }
        public bool Status { get; set; }
        public string Mensagem { get; set; }

        public Retorno()
        {
            Status = false;
        }

        public Retorno(bool status)
        {
            Status = status;
        }

        public Retorno(string mensagem)
        {
            Status = false;
            Mensagem = mensagem;
        }

        public Retorno(long codigoRegistro)
        {
            Status = true;
            CodigoRegistro = codigoRegistro;
        }

        public Retorno(long codigoRegistro, string mensagem)
        {
            Status = true;
            Mensagem = mensagem;
            CodigoRegistro = codigoRegistro;
        }

        public Retorno(bool status, string mensagem)
        {
            Status = status;
            Mensagem = mensagem;
        }

        public Retorno(bool status, string mensagem, long codigoRegistro)
        {
            Status = status;
            Mensagem = mensagem;
            CodigoRegistro = codigoRegistro;
        }
    }
}
