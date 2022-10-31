using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppNFe.Dominio.DTO.Integracoes.CEP
{    
    public class DTOConsultaCorreiosCEPDado
    {
        public string uf { get; set; }
        public string localidade { get; set; }
        public string locNoSem { get; set; }
        public string locNu { get; set; }
        public string localidadeSubordinada { get; set; }
        public string logradouroDNEC { get; set; }
        public string logradouroTextoAdicional { get; set; }
        public string logradouroTexto { get; set; }
        public string bairro { get; set; }
        public string baiNu { get; set; }
        public string nomeUnidade { get; set; }
        public string cep { get; set; }
        public string tipoCep { get; set; }
        public string numeroLocalidade { get; set; }
        public string situacao { get; set; }
        public List<object> faixasCaixaPostal { get; set; }
        public List<object> faixasCep { get; set; }

        public DTORetornoConsultaCEP ToRetornoConsultaCEP()
        {
            DTORetornoConsultaCEP retornoConsultaCEP = new DTORetornoConsultaCEP();
            if (!string.IsNullOrEmpty(localidade) || !string.IsNullOrEmpty(localidadeSubordinada))
            {
                retornoConsultaCEP.Cep = cep;
                retornoConsultaCEP.Logradouro = logradouroDNEC;
                retornoConsultaCEP.Complemento = logradouroTextoAdicional;
                retornoConsultaCEP.Bairro = bairro;
                retornoConsultaCEP.Cidade = !string.IsNullOrEmpty(localidade)? localidade: localidadeSubordinada;
                retornoConsultaCEP.UF = uf;
            }            
            return retornoConsultaCEP;
        }
    }

    public class DTOConsultaCorreiosCEP
    {
        public bool erro { get; set; }
        public string mensagem { get; set; }
        public int total { get; set; }
        public List<DTOConsultaCorreiosCEPDado> dados { get; set; }
    }    
}
