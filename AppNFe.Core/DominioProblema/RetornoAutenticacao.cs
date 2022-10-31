using System;

namespace AppNFe.Core.DominioProblema
{
    public class RetornoAutenticacao
    {
        public bool Autenticado { get; set; }
        public string Token { get; set; }
        public DateTime DataExpiracao { get; set; }
        public string Mensagem { get; set; }
    }
}
