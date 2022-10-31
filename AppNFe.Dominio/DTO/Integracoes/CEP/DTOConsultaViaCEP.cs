using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppNFe.Dominio.DTO.Integracoes.CEP
{   
    public class DTOConsultaViaCEP
    {
        public string cep { get; set; }
        public string logradouro { get; set; }
        public string complemento { get; set; }
        public string bairro { get; set; }
        public string localidade { get; set; }
        public string uf { get; set; }
        public string ibge { get; set; }
        public string gia { get; set; }
        public string ddd { get; set; }
        public string siafi { get; set; }

        public DTORetornoConsultaCEP ToRetonoConsultaCEP()
        {
            DTORetornoConsultaCEP retornoConsultaCEP = new DTORetornoConsultaCEP();
            if (!string.IsNullOrEmpty(localidade))
            {
                retornoConsultaCEP.Cep = cep;
                retornoConsultaCEP.Logradouro = logradouro;
                retornoConsultaCEP.Complemento = complemento;
                retornoConsultaCEP.Bairro = bairro;
                retornoConsultaCEP.Cidade = localidade;
                retornoConsultaCEP.UF = uf;
            }           
            return retornoConsultaCEP;
        }
    }
}
