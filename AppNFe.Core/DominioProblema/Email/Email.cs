using System.Collections.Generic;

namespace AppNFe.Core.DominioProblema.Email
{
    public class Email
    {
        public ConfiguracaoSMTP ConfiguracaoSMTP { get; set; }
        public string Remetente { get; set; }
        public string Identificacao { get; set; }        
        public string Assunto { get; set; }
        public string Mensagem { get; set; }
        public List<string> Destinatarios { get; set; }
        public List<string> DestinatariosCopia { get; set; }
        public List<string> DestinatariosOcultos { get; set; }
        public List<string> Anexos { get; set; }

        public Email()
        {
            ConfiguracaoSMTP = new ConfiguracaoSMTP();
            Destinatarios = new List<string>();
            DestinatariosOcultos = new List<string>();
            Anexos = new List<string>();
        }
    }
}
