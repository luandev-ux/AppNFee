using AppNFe.Core.MensagemPadronizada;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AppNFe.Dominio.Entidades.Pessoas
{
    [Display(Name = "Pessoa")]
    public class Pessoa
    {
        #region Propriedades Pessoa

        #region Codigo Pessoa
        [Required(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E001")]
        public long Codigo { get; set; }
        #endregion
        #region Cliente / Fornecedor
        [Required(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E001")]
        [MaxLength(100, ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E003")]
        [Display(Name = "O campo deve ter no máximo 100 caracteres!")]
        public string Nome { get; set; }
        #endregion
        #region Nome Fantasia
        [Required(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E001")]
        [MaxLength(100, ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E003")]
        [Display(Name = "O campo deve ter no máximo 100 caracteres!")]
        public string NomeFantasia { get; set; }
        #endregion
        #region CNPJ / CPF
        [Required(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E001")]
        [MaxLength(14, ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E003")]
        [Display(Name = "O campo é obrigatório!")]
        public string CnpjCpf { get; set; }
        #endregion
        #region Inscricao Estadual
        [Required(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E001")]
        [MaxLength(20, ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E003")]
        [Display(Name = "O campo {0} não é válido!")]
        public string InscricaoEstadual { get; set; }
        #endregion
        #region Endereco
        [Required(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E001")]
        [MaxLength(100, ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E003")]
        [Display(Name = "Endereco")]
        public string Endereco { get; set; }
        #endregion
        #region Bairro
        [Required(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E001")]
        [MaxLength(100, ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E003")]
        [Display(Name = "Bairro")]
        public string Bairro { get; set; }
        #endregion
        #region Cidade
        [Required(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E001")]
        [MaxLength(100, ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E003")]
        [Display(Name = "Cidade")]
        public string Cidade { get; set; }
        #endregion
        #region UF
        [Required(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E001")]
        [MaxLength(2, ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E003")]
        [Display(Name = "UF")]
        public string Uf { get; set; }
        #endregion
        #region CEP
        [Required(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E001")]
        [MaxLength(8, ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E003")]
        [Display(Name = "CEP")]
        public string Cep { get; set; }
        #endregion
        #region Telefone
        [Required(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E001")]
        [MaxLength(20, ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E003")]
        [Display(Name = "Telefone")]
        public string Telefone { get; set; }
        #endregion
        #region Email
        [Required(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E001")]
        [MaxLength(255, ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E003")]
        [EmailAddress(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E004")]
        [Display(Name = "Email")]
        public string Email { get; set; }
        #endregion
        #region Tipo Pessoa
        [Required(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E001")]
        [MaxLength(2, ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E003")]
        public string TipoPessoa { get; set; }
        #endregion
        
        public List<Cliente> Clientes { get; set; }

        public List<Fornecedor> Fornecedores { get; set; }

        public List<Movimento> Movimentos { get; set; }

        public Pessoa()
        {
            Clientes = new List<Cliente>();
            Fornecedores = new List<Fornecedor>();
            Movimentos = new List<Movimento>();
        }
        #endregion
    }
    
}