namespace AppNFe.Dominio.DTO.Integracoes.CEP
{
    public class DTORetornoConsultaCEP
    {        
        public string Cep { get; set; }        
        public string Logradouro { get; set; }
        public string Complemento { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string UF { get; set; }
    }
}
