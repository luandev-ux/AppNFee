using AppNFe.Core.Utilitarios;
using System.Collections.Generic;

namespace AppNFe.Dominio.DTO.Integracoes.CNPJ
{        
    public class DTOConsultaTechCNPJSimples
    {
        public bool optante { get; set; }
        public string data_opcao { get; set; }
        public string data_exclusao { get; set; }
    }

    public class DTOConsultaTechCNPJMei
    {
        public bool optante { get; set; }
        public string data_opcao { get; set; }
        public string data_exclusao { get; set; }
    }

    public class DTOConsultaTechCNPJContato
    {
        public string ddd_1 { get; set; }
        public string tel_1 { get; set; }
        public string ddd_2 { get; set; }
        public string tel_2 { get; set; }
        public string email { get; set; }
    }

    public class DTOConsultaTechCNPJEndereco
    {
        public string cep { get; set; }
        public string uf { get; set; }
        public int cod_municipio { get; set; }
        public string municipio { get; set; }
        public string bairro { get; set; }
        public string tipo_logadouro { get; set; }
        public string logadouro { get; set; }
        public string numero { get; set; }
        public string complemento { get; set; }
    }

    public class DTOConsultaTechCNPJQsa
    {
        public string ident { get; set; }
        public string doc { get; set; }
        public string nome { get; set; }
        public string data_entrada { get; set; }
        public string qualificacao { get; set; }
        public string idade { get; set; }
    }

    public class DTOConsultaTechCNPJ
    {
        public string cnpj { get; set; }
        public string matriz_filial { get; set; }
        public string razao_social { get; set; }
        public string nome_fantasia { get; set; }
        public int cod_natureza_juridica { get; set; }
        public string natureza_juridica { get; set; }
        public string porte { get; set; }
        public DTOConsultaTechCNPJSimples simples { get; set; }
        public DTOConsultaTechCNPJMei mei { get; set; }
        public string data_inicio_ativ { get; set; }
        public string situacao_cadastral { get; set; }
        public string data_situacao_cadastral { get; set; }
        public string motivo_situacao_cadastral { get; set; }
        public object cnae_principal { get; set; }
        public object cnae_secundaria { get; set; }        
        public DTOConsultaTechCNPJContato contato { get; set; }
        public DTOConsultaTechCNPJEndereco endereco { get; set; }
        public List<DTOConsultaTechCNPJQsa> qsa { get; set; }        
        public DTORetornoConsultaCNPJ ToRetornoConsultaCNPJ()
        {
            DTORetornoConsultaCNPJ retornoConsultaCNPJ = new DTORetornoConsultaCNPJ();

            if (!string.IsNullOrEmpty(razao_social))
            {
                retornoConsultaCNPJ.CNPJ = cnpj;
                retornoConsultaCNPJ.MatrizFilial = matriz_filial;
                retornoConsultaCNPJ.RazaoSocial = razao_social;
                retornoConsultaCNPJ.NomeFantasia = nome_fantasia;
                retornoConsultaCNPJ.CodigoNaturezaJuridica = cod_natureza_juridica;
                retornoConsultaCNPJ.NaturezaJuridica = natureza_juridica;
                retornoConsultaCNPJ.Porte = porte;
                if (retornoConsultaCNPJ.Simples != null)
                {
                    retornoConsultaCNPJ.Simples.Optante = simples.optante;
                    retornoConsultaCNPJ.Simples.DataOpcao = UtilitarioData.StringToDateTime(simples.data_opcao);
                    retornoConsultaCNPJ.Simples.DataExclusao = UtilitarioData.StringToDateTime(simples.data_exclusao);
                }

                if (retornoConsultaCNPJ.Mei != null)
                {
                    retornoConsultaCNPJ.Mei.Optante = mei.optante;
                    retornoConsultaCNPJ.Mei.DataOpcao = UtilitarioData.StringToDateTime(mei.data_opcao);
                    retornoConsultaCNPJ.Mei.DataExclusao = UtilitarioData.StringToDateTime(mei.data_exclusao);                    
                }

                retornoConsultaCNPJ.DataInicioAtividade = UtilitarioData.StringToDateTime(data_inicio_ativ);
                retornoConsultaCNPJ.SituacaoCadastral = situacao_cadastral;


                retornoConsultaCNPJ.DataSituacaoCadastral = UtilitarioData.StringToDateTime(data_situacao_cadastral);
                retornoConsultaCNPJ.MotivoSituacaoCadastral = motivo_situacao_cadastral;
                if (cnae_principal != null)
                {
                    string[] dadosCnae = cnae_principal.ToString().Split(':');
                    if (dadosCnae.Length > 0) { }
                    retornoConsultaCNPJ.CnaePrincipal = dadosCnae[0].Replace("\"", "").Replace("{", "");
                }

                if (contato != null)
                {
                    if (!string.IsNullOrEmpty(contato.ddd_1) && !string.IsNullOrEmpty(contato.tel_1))
                    {
                        retornoConsultaCNPJ.Contato.TelefonePrincipal = contato.ddd_1 + contato.tel_1;
                    }
                    if (!string.IsNullOrEmpty(contato.ddd_2) && !string.IsNullOrEmpty(contato.tel_2))
                    {
                        retornoConsultaCNPJ.Contato.TelefoneSecundario = contato.ddd_2 + contato.tel_2;
                    }
                    retornoConsultaCNPJ.Contato.Email = contato.email;
                }

                if (endereco != null)
                {
                    retornoConsultaCNPJ.Endereco.Cep = endereco.cep;
                    retornoConsultaCNPJ.Endereco.Logadouro = endereco.logadouro;
                    if (!string.IsNullOrEmpty(endereco.complemento))
                    {
                        retornoConsultaCNPJ.Endereco.Complemento = endereco.complemento;
                        retornoConsultaCNPJ.Endereco.Complemento = retornoConsultaCNPJ.Endereco.Complemento.Replace("                  ", " ");
                        retornoConsultaCNPJ.Endereco.Complemento = retornoConsultaCNPJ.Endereco.Complemento.Replace("  ", " ");
                    }
                    retornoConsultaCNPJ.Endereco.Numero = endereco.numero;
                    retornoConsultaCNPJ.Endereco.Bairro = endereco.bairro;
                    retornoConsultaCNPJ.Endereco.Cidade = endereco.municipio;
                    retornoConsultaCNPJ.Endereco.UF = endereco.uf;
                }

                if (qsa != null)
                {
                    foreach (var itemQse in qsa)
                    {
                        DTORetornoConsultaCNPJQsa socio = new DTORetornoConsultaCNPJQsa();
                        socio.Identificacao = itemQse.ident;
                        socio.Documento = itemQse.doc;
                        socio.Nome = itemQse.nome;
                        socio.Idade = itemQse.idade;
                        socio.Qualificacao = itemQse.qualificacao;
                        socio.DataEntrada = UtilitarioData.StringToDateTime(itemQse.data_entrada);

                        retornoConsultaCNPJ.Qsa.Add(socio);
                    }
                }
            }                  
            return retornoConsultaCNPJ;
        }
    }    
}
