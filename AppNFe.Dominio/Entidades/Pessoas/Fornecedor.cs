using AppNFe.Core.MensagemPadronizada;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppNFe.Dominio.Entidades.Pessoas
{
    public class Fornecedor
    {
        #region Codigo      
        [Required(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E001")]
        public long Codigo { get; set; }
        #endregion
        #region Codigo Pessoa      
        [Required(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E001")]
        [ForeignKey("pessoa")]
        public long CodigoPessoa { get; set; }
        #endregion
    }
}
