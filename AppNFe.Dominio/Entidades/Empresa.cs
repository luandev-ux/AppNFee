using AppNFe.Core.Attributes;
using AppNFe.Core.MensagemPadronizada;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AppNFe.Dominio.Entidades
{
    [Display(Name = "Empresa")]
    public class Empresa
    {
        #region Propriedades
        #region Codigo Empresa
        [Required(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E001")]
        public long Codigo { get; set; }
        #endregion
        #region Nome
        [Required(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E001")]
        [MaxLength(100, ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E003")]
        [Display(Name = "Nome")]
        public string Nome { get; set; }
        #endregion
        #region Nome_fantasia
        [Required(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E001")]
        [MaxLength(100, ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E003")]
        [Display(Name = "nome_fantasia")]
        public string Nome_fantasia { get; set; }
        #endregion
        #region Cnpj
        [Required(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E001")]
        [MaxLength(20, ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E003")]
        [Display(Name = "cnpj")]
        public string Cnpj { get; set; }
        #endregion
        #region Endereco
        [Required(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E001")]
        [MaxLength(100, ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E003")]
        [Display(Name = "endereco")]
        public string Endereco { get; set; }
        #endregion
        #region Bairro
        [Required(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E001")]
        [MaxLength(100, ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E003")]
        [Display(Name = "bairro")]
        public string Bairro { get; set; }
        #endregion
        #region Numero
        [Required(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E001")]
        [MaxLength(5, ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E003")]
        [Display(Name = "numero")]
        public string Numero { get; set; }
        #endregion
        #region Uf
        [Required(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E001")]
        [MaxLength(2, ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E003")]
        [Display(Name = "uf")]
        public string Uf { get; set; }
        #endregion
        #region Cep
        [Required(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E001")]
        [MaxLength(8, ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E003")]
        [Display(Name = "cep")]
        public string Cep { get; set; }
        #endregion
        #region Inscricao_estadual
        [Required(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E001")]
        [MaxLength(20, ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E003")]
        [Display(Name = "inscricao_estadual")]
        public string Inscricao_estadual { get; set; }
        #endregion
        #region Telefone
        [Required(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E001")]
        [MaxLength(20, ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E003")]
        [Display(Name = "telefone")]
        public string Telefone { get; set; }
        #endregion
        #region Ultimo_nfe
        [Required(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E001")]
        [Display(Name = "ultimo_nfe")]
        public long Ultimo_nfe { get; set; }
        #endregion
        #region Ultimo_nfce
        [Required(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E001")]
        [Display(Name = "ultimo_nfce")]
        public long Ultimo_nfce { get; set; }
        #endregion
        #region Serie_nfe
        [Required(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E001")]
        [MaxLength(3, ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E003")]
        [Display(Name = "serie_nfe")]
        public string Serie_nfe { get; set; }
        #endregion
        #region Serie_nfce
        [Required(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E001")]
        [MaxLength(3, ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E003")]
        [Display(Name = "serie_nfce")]
        public string Serie_nfce { get; set; }
        #endregion
        #region Perc_simples
        [Required(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E001")]
        [Display(Name = "perc_simples")]
        public double Perc_simples { get; set; }
        #endregion
        #region Certificado_digital
        [Required(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E001")]
        [Display(Name = "certificado_digital")]
        public string Certificado_digital { get; set; }
        #endregion
        #region Tipo_certificado
        [Required(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E001")]
        [Display(Name = "tipo_certificado")]
        public int Tipo_certificado { get; set; }
        #endregion
        #region Logo
        [Required(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E001")]
        [Display(Name = "logo")]
        public string Logo { get; set; }
        #endregion
        #endregion
    }
}