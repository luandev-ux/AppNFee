using AppNFe.Core.MensagemPadronizada;
using AppNFe.Dominio.Entidades.Movimentos;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AppNFe.Dominio.Entidades.Movimento
{
    public class ItemMovimento
    {
        #region Propriedades

        #region Codigo
        [Required(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E001")]
        public long Codigo { get; set; }
        #endregion
        #region Quantidade
        [Required(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E001")]
        [Display(Name = "Coloque a quantidade")]
        public double Quantidade { get; set; }
        #endregion
        #region Valor_unitario
        [Required(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E001")]
        [Display(Name = "Valor Unitário")]
        public double ValorUnitario { get; set; }
        #endregion
        #region Desconto
        [Required(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E001")]
        [Display(Name = "Desconto")]
        public double Desconto { get; set; }
        #endregion
        #region Valor_total
        [Required(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E001")]
        [Display(Name = "Valor Total")]
        public double ValorTotal { get; set; }
        #endregion
        #region Unidade
        [Required(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E001")]
        [Display(Name = "Unidade")]
        [MaxLength(6, ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E003")]
        public string Unidade { get; set; }
        #endregion
        #region NCM
        [Required(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E001")]
        [Display(Name = "NCM")]
        [MaxLength(20, ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E003")]
        public string NCM { get; set; }
        #endregion
        #region Custo
        [Required(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E001")]
        [Display(Name = "Custo")]
        public double Custo { get; set; }
        #endregion

        #endregion

        public List<Produto> Produtos { get; set; }
        public List<ItemMovimentoImposto> ItemMovimentoImpostos { get; set; }

        public ItemMovimento()
        {
            Produtos = new List<Produto>();
            ItemMovimentoImpostos = new List<ItemMovimentoImposto>();
        }
    }
}
