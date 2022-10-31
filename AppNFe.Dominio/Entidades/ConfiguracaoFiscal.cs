using AppNFe.Core.MensagemPadronizada;
using System.ComponentModel.DataAnnotations;

namespace AppNFe.Dominio.Entidades
{
    public class ConfiguracaoFiscal
    {
        #region Codigo
        [Required(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E001")]
        public long Codigo { get; set; }
        #endregion
        #region uf_origem
        [Required(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E001")]
        [MaxLength(2, ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E003")]
        [Display(Name = "uf_origem")]
        public string UfOrigem { get; set; }
        #endregion
        #region uf_destino
        [Required(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E001")]
        [MaxLength(2, ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E003")]
        [Display(Name = "uf_destino")]
        public string UfDestino { get; set; }
        #endregion
        #region cfop
        [Required(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E001")]
        [MaxLength(4, ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E003")]
        [Display(Name = "cfop")]
        public string Cfop { get; set; }
        #endregion
        #region cst
        [Required(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E001")]
        [MaxLength(4, ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E003")]
        [Display(Name = "cst")]
        public string Cst { get; set; }
        #endregion
        #region aliquota_icms
        [Required(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E001")]
        [Display(Name = "aliquota_icms")]
        public double AliquotaIcms { get; set; }
        #endregion
        #region cst_pis
        [Required(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E001")]
        [MaxLength(3, ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E003")]
        [Display(Name = "cst_pis")]
        public string CstPis { get; set; }
        #endregion
        #region aliquota_pis
        [Required(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E001")]
        [Display(Name = "aliquota_pis")]
        public double AliquotaPis { get; set; }
        #endregion
        #region cst_cofins
        [Required(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E001")]
        [MaxLength(3, ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E003")]
        [Display(Name = "cst_cofins")]
        public string CstCofins { get; set; }
        #endregion
        #region aliquota_cofins
        [Required(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E001")]
        [Display(Name = "aliquota_cofins")]
        public double AliquotaCofins { get; set; }
        #endregion
    }
}
