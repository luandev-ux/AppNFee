namespace AppNFe.Core.DominioProblema.Email
{
    public class ConfiguracaoSMTP
    {
        public string Servidor { get; set; }
        public string Porta { get; set; }
        public string Usuario { get; set; }
        public string Senha { get; set; }
        public bool SSL { get; set; }
    }
}
