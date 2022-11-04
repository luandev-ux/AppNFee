using AppNFe.Core.MensagemPadronizada;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppNFe.Dominio.Entidades.Movimentos
{
    public class ItemMovimentoImposto
    {
        #region Propriedades
        
        #region Codigo
        [Required(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E001")]
        public long Codigo { get; set; }
        #endregion
        #region Bc_icms
        [Required(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E001")]
        public double BaseCalculoIcms { get; set; }
        #endregion
        #region Cst
        [Required(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E001")]
        [MaxLength(4, ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E003")]
        public string Cst { get; set; }
        #endregion
        #region Aliquota_icms
        [Required(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E001")]
        public double AliquotaIcms { get; set; }
        #endregion
        #region Valor_icms
        [Required(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E001")]
        public double ValorIcms { get; set; }
        #endregion
        #region Base Calculo Pis
        [Required(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E001")]
        public double BaseCalculoPis { get; set; }
        #endregion
        #region Cst Pis
        [Required(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E001")]
        [MaxLength(3, ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E003")]
        public string CstPis { get; set; }
        #endregion
        #region Aliquota Pis
        [Required(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E001")]
        public double AliquotaPis { get; set; }
        #endregion
        #region Valor Pis
        [Required(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E001")]
        public double ValorPis { get; set; }
        #endregion
        #region Base Calculo Cofins
        [Required(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E001")]
        public double BaseCalculoCofins { get; set; }
        #endregion
        #region Cst Cofins
        [Required(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E001")]
        [MaxLength(3, ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E003")]
        public string CstCofins { get; set; }
        #endregion
        #region Aliquota_cofins
        [Required(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E001")]
        public double AliquotaCofins { get; set; }
        #endregion
        #region Valor_cofins
        [Required(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E001")]
        public double ValorCofins { get; set; }
        #endregion
        
        #endregion
    }
}
