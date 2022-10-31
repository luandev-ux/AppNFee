using System.Collections.Generic;

namespace AppNFe.Core.DominioProblema
{
    public class RetornoValidacao
    {        
        public List<string> Mensagens { get; set; }
        public RetornoValidacao()
        {
            Mensagens = new List<string>();
        }
        public bool Sucesso()
        {
            return (this.Mensagens.Count == 0);
        }
    }
}
