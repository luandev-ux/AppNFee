using AppNFe.Core.Utilitarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppNFe.Dominio.DTO.Integracoes.CNPJ
{

    public class DTOConsultaCNPJWsPorte
    {
        public string id { get; set; }
        public string descricao { get; set; }
    }

    public class DTOConsultaCNPJWsNaturezaJuridica
    {
        public string id { get; set; }
        public string descricao { get; set; }
    }

    public class DTOConsultaCNPJWsQualificacaoDoResponsavel
    {
        public int id { get; set; }
        public string descricao { get; set; }
    }

    public class DTOConsultaCNPJWsQualificacaoSocio
    {
        public int id { get; set; }
        public string descricao { get; set; }
    }

    public class DTOConsultaCNPJWsSocio
    {
        public string cpf_cnpj_socio { get; set; }
        public string nome { get; set; }
        public string tipo { get; set; }
        public DateTime? data_entrada { get; set; }
        public string cpf_representante_legal { get; set; }
        public object nome_representante { get; set; }
        public string faixa_etaria { get; set; }
        public DateTime? atualizado_em { get; set; }
        public string pais_id { get; set; }
        public DTOConsultaCNPJWsQualificacaoSocio qualificacao_socio { get; set; }
        public object qualificacao_representante { get; set; }
    }

    public class DTOConsultaCNPJWsSimples
    {
        public string simples { get; set; }
        public DateTime? data_opcao_simples { get; set; }
        public DateTime? data_exclusao_simples { get; set; }
        public string mei { get; set; }
        public DateTime? data_opcao_mei { get; set; }
        public DateTime? data_exclusao_mei { get; set; }
        public string atualizado_em { get; set; }
    }


    public class DTOConsultaCNPJWsAtividadesSecundaria
    {
        public string id { get; set; }
        public string secao { get; set; }
        public string divisao { get; set; }
        public string grupo { get; set; }
        public string classe { get; set; }
        public string subclasse { get; set; }
        public string descricao { get; set; }
    }

    public class DTOConsultaCNPJWsAtividadePrincipal
    {
        public string id { get; set; }
        public string secao { get; set; }
        public string divisao { get; set; }
        public string grupo { get; set; }
        public string classe { get; set; }
        public string subclasse { get; set; }
        public string descricao { get; set; }
    }

    public class DTOConsultaCNPJWsPais
    {
        public string id { get; set; }
        public string iso2 { get; set; }
        public string iso3 { get; set; }
        public string nome { get; set; }
        public string comex_id { get; set; }
    }

    public class DTOConsultaCNPJWsEstado
    {
        public int id { get; set; }
        public string nome { get; set; }
        public string sigla { get; set; }
        public int ibge_id { get; set; }
    }

    public class DTOConsultaCNPJWsCidade
    {
        public int id { get; set; }
        public string nome { get; set; }
        public int ibge_id { get; set; }
        public string siafi_id { get; set; }
    }

    public class DTOConsultaCNPJWsInscricoesEstaduai
    {
        public string inscricao_estadual { get; set; }
        public bool ativo { get; set; }
        public DateTime atualizado_em { get; set; }
        public DTOConsultaCNPJWsEstado estado { get; set; }
    }

    public class DTOConsultaCNPJWsEstabelecimento
    {
        public string cnpj { get; set; }
        public List<DTOConsultaCNPJWsAtividadesSecundaria> atividades_secundarias { get; set; }
        public string cnpj_raiz { get; set; }
        public string cnpj_ordem { get; set; }
        public string cnpj_digito_verificador { get; set; }
        public string tipo { get; set; }
        public string nome_fantasia { get; set; }
        public string situacao_cadastral { get; set; }
        public DateTime? data_situacao_cadastral { get; set; }
        public DateTime? data_inicio_atividade { get; set; }
        public string nome_cidade_exterior { get; set; }
        public string tipo_logradouro { get; set; }
        public string logradouro { get; set; }
        public string numero { get; set; }
        public string complemento { get; set; }
        public string bairro { get; set; }
        public string cep { get; set; }
        public string ddd1 { get; set; }
        public string telefone1 { get; set; }
        public string ddd2 { get; set; }
        public string telefone2 { get; set; }
        public string ddd_fax { get; set; }
        public string fax { get; set; }
        public string email { get; set; }
        public string situacao_especial { get; set; }
        public string data_situacao_especial { get; set; }
        public DTOConsultaCNPJWsAtividadePrincipal atividade_principal { get; set; }
        public DTOConsultaCNPJWsPais pais { get; set; }
        public DTOConsultaCNPJWsEstado estado { get; set; }
        public DTOConsultaCNPJWsCidade cidade { get; set; }
        public object motivo_situacao_cadastral { get; set; }
        public List<DTOConsultaCNPJWsInscricoesEstaduai> inscricoes_estaduais { get; set; }
    }

    public class DTOConsultaCNPJWs
    {
        public string cnpj_raiz { get; set; }
        public string razao_social { get; set; }
        public string capital_social { get; set; }
        public string responsavel_federativo { get; set; }
        public DateTime atualizado_em { get; set; }
        public DTOConsultaCNPJWsPorte porte { get; set; }
        public DTOConsultaCNPJWsNaturezaJuridica natureza_juridica { get; set; }
        public DTOConsultaCNPJWsQualificacaoDoResponsavel qualificacao_do_responsavel { get; set; }
        public List<DTOConsultaCNPJWsSocio> socios { get; set; }
        public DTOConsultaCNPJWsSimples simples { get; set; }
        public DTOConsultaCNPJWsEstabelecimento estabelecimento { get; set; }

        public DTORetornoConsultaCNPJ ToRetornoConsultaCNPJ()
        {
            DTORetornoConsultaCNPJ retornoConsultaCNPJ = new DTORetornoConsultaCNPJ();

            if (!string.IsNullOrEmpty(razao_social) && estabelecimento != null)
            {
                retornoConsultaCNPJ.CNPJ = estabelecimento.cnpj;
                retornoConsultaCNPJ.MatrizFilial = estabelecimento.tipo;
                retornoConsultaCNPJ.RazaoSocial = razao_social;
                retornoConsultaCNPJ.NomeFantasia = estabelecimento.nome_fantasia;
                retornoConsultaCNPJ.CodigoNaturezaJuridica = int.Parse(natureza_juridica.id);
                retornoConsultaCNPJ.NaturezaJuridica = natureza_juridica.descricao;
                retornoConsultaCNPJ.Porte = porte.descricao;

                if (estabelecimento.inscricoes_estaduais != null && estabelecimento.inscricoes_estaduais.Count > 0){
                    if (estabelecimento.estado != null)
                    {
                        var inscricaoEstadual = estabelecimento.inscricoes_estaduais.FirstOrDefault(c => c.estado.sigla == estabelecimento.estado.sigla);
                        if (inscricaoEstadual != null)                        
                            retornoConsultaCNPJ.InscricaoEstadual = inscricaoEstadual.ativo ? inscricaoEstadual.inscricao_estadual : "";                        
                    }                    
                }

                if (simples != null)
                {
                    retornoConsultaCNPJ.Simples.Optante = simples.simples.ToLower() == "sim" ? true : false;
                    if (simples.data_opcao_simples != null)
                        retornoConsultaCNPJ.Simples.DataOpcao = (DateTime)simples.data_opcao_simples;

                    if (simples.data_exclusao_simples != null)
                        retornoConsultaCNPJ.Simples.DataExclusao = (DateTime)simples.data_exclusao_simples;

                    if (simples.mei != null)
                    {
                        retornoConsultaCNPJ.Mei.Optante = simples.mei.ToLower() == "sim" ? true : false;

                        if (simples.data_opcao_mei != null)
                            retornoConsultaCNPJ.Mei.DataOpcao = (DateTime)simples.data_opcao_mei;

                        if (simples.data_exclusao_mei != null)
                            retornoConsultaCNPJ.Mei.DataExclusao = (DateTime)simples.data_exclusao_mei;
                    }
                }
                
                if (estabelecimento.data_inicio_atividade != null)
                    retornoConsultaCNPJ.DataInicioAtividade = (DateTime)estabelecimento.data_inicio_atividade;
                
                retornoConsultaCNPJ.SituacaoCadastral = estabelecimento.situacao_cadastral;


                if (estabelecimento.data_situacao_cadastral != null)
                    retornoConsultaCNPJ.DataSituacaoCadastral = (DateTime)estabelecimento.data_situacao_cadastral;
                
                retornoConsultaCNPJ.MotivoSituacaoCadastral = "";
                if (estabelecimento.atividade_principal != null)
                {
                    retornoConsultaCNPJ.CnaePrincipal = estabelecimento.atividade_principal.id;
                }

                if (!string.IsNullOrEmpty(estabelecimento.ddd1) && !string.IsNullOrEmpty(estabelecimento.telefone1))
                {
                    retornoConsultaCNPJ.Contato.TelefonePrincipal = estabelecimento.ddd1 + estabelecimento.telefone1;
                }
                if (!string.IsNullOrEmpty(estabelecimento.ddd2) && !string.IsNullOrEmpty(estabelecimento.telefone2))
                {
                    retornoConsultaCNPJ.Contato.TelefoneSecundario = estabelecimento.ddd2 + estabelecimento.telefone2;
                }
                retornoConsultaCNPJ.Contato.Email = estabelecimento.email;
                retornoConsultaCNPJ.Endereco.Cep = estabelecimento.cep;
                retornoConsultaCNPJ.Endereco.Logadouro = estabelecimento.tipo_logradouro + " " + estabelecimento.logradouro;
                if (!string.IsNullOrEmpty(estabelecimento.complemento))
                {
                    retornoConsultaCNPJ.Endereco.Complemento = estabelecimento.complemento;
                    retornoConsultaCNPJ.Endereco.Complemento = retornoConsultaCNPJ.Endereco.Complemento.Replace("                  ", " ");
                    retornoConsultaCNPJ.Endereco.Complemento = retornoConsultaCNPJ.Endereco.Complemento.Replace("  ", " ");
                }
                retornoConsultaCNPJ.Endereco.Numero = estabelecimento.numero;
                retornoConsultaCNPJ.Endereco.Bairro = estabelecimento.bairro;
                retornoConsultaCNPJ.Endereco.Cidade = estabelecimento.cidade.nome;
                retornoConsultaCNPJ.Endereco.UF = estabelecimento.estado.sigla;


                if (socios != null)
                {
                    foreach (var itemQse in socios)
                    {
                        DTORetornoConsultaCNPJQsa socio = new DTORetornoConsultaCNPJQsa();
                        socio.Identificacao = itemQse.cpf_cnpj_socio;
                        socio.Documento = "";
                        socio.Nome = itemQse.nome;
                        socio.Idade = itemQse.faixa_etaria;
                        socio.Qualificacao = itemQse.qualificacao_socio.descricao;

                        if (itemQse.data_entrada != null)
                            socio.DataEntrada = (DateTime)itemQse.data_entrada;                        

                        retornoConsultaCNPJ.Qsa.Add(socio);
                    }
                }
            }
            return retornoConsultaCNPJ;
        }
    }
}
