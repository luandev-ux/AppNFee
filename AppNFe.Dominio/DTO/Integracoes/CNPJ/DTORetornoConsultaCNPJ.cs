using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppNFe.Dominio.DTO.Integracoes.CNPJ
{
    
    public class DTORetornoConsultaCNPJSimples
    {
        public bool Optante { get; set; }
        public DateTime DataOpcao { get; set; }
        public DateTime DataExclusao { get; set; }
    }

    public class DTORetornoConsultaCNPJMei
    {
        public bool Optante { get; set; }
        public DateTime DataOpcao { get; set; }
        public DateTime DataExclusao { get; set; }
    }

    public class DTORetornoConsultaCNPJContato
    {        
        public string TelefonePrincipal { get; set; }      
        public string TelefoneSecundario { get; set; }
        public string Email { get; set; }
    }

    public class DTORetornoConsultaCNPJEndereco
    {
        public string Cep { get; set; }        
        public string Logadouro { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string UF { get; set; }
    }

    public class DTORetornoConsultaCNPJQsa
    {
        public string Identificacao { get; set; }
        public string Documento { get; set; }
        public string Nome { get; set; }
        public DateTime DataEntrada { get; set; }
        public string Qualificacao { get; set; }
        public string Idade { get; set; }
    }

    public class DTORetornoConsultaCNPJ
    {
        public string CNPJ { get; set; }
        public string MatrizFilial { get; set; }
        public string RazaoSocial { get; set; }
        public string NomeFantasia { get; set; }
        public int CodigoNaturezaJuridica { get; set; }
        public string NaturezaJuridica { get; set; }
        public string Porte { get; set; }
        public DTORetornoConsultaCNPJSimples Simples { get; set; }
        public DTORetornoConsultaCNPJMei Mei { get; set; }
        public DateTime DataInicioAtividade { get; set; }
        public string SituacaoCadastral { get; set; }
        public DateTime DataSituacaoCadastral { get; set; }
        public string MotivoSituacaoCadastral { get; set; }
        public string CnaePrincipal { get; set; }
        public string InscricaoEstadual { get; set; }
        public DTORetornoConsultaCNPJContato Contato { get; set; }
        public DTORetornoConsultaCNPJEndereco Endereco { get; set; }
        public List<DTORetornoConsultaCNPJQsa> Qsa { get; set; }

        public DTORetornoConsultaCNPJ()
        {
            Simples = new DTORetornoConsultaCNPJSimples();
            Mei = new DTORetornoConsultaCNPJMei();            
            Contato = new DTORetornoConsultaCNPJContato();
            Endereco = new DTORetornoConsultaCNPJEndereco();
            Qsa = new List<DTORetornoConsultaCNPJQsa>();
        }
    }
}
