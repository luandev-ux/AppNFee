using AppNFe.Core.MensagemPadronizada;
using System.ComponentModel.DataAnnotations;

namespace AppNFe.Dominio.Entidades
{
    [Display(Name = "Produto")]
    public class Produto
    {
        #region Propriedades

        #region Codigo
        [Required(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E001")]
        public long Codigo { get; set; }
        #endregion
        #region Descricao
        [Required(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E001")]
        [MaxLength(100, ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E003")]
        [Display(Name = "Descrição do Produto")]
        public string Descricao { get; set; }
        #endregion
        #region Preço
        [Required(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E001")]
        [Display(Name = "Preço do Produto")]
        public double Preco { get; set; }
        #endregion
        #region NCM
        [Required(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E001")]
        [MaxLength(20, ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E003")]
        [Display(Name = "NCM do Produto")]
        public string Ncm { get; set; }
        #endregion
        #region Custo
        [Required(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E001")]
        [Display(Name = "Custo do Produto")]
        public double Custo { get; set; }
        #endregion
        #region Codigo de Barras
        [Required(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E001")]
        [MaxLength(100, ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E003")]
        [Display(Name = "Código de Barras do Produto")]
        public string CodigoBarras { get; set; }
        #endregion
        #region Unidade
        [Required(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E001")]
        [MaxLength(6, ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E003")]
        [Display(Name = "Unidade do Produto")]
        public string Unidade { get; set; }
        #endregion

        #endregion
    }
}
