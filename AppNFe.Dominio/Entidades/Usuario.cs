using AppNFe.Core.MensagemPadronizada;
using System.ComponentModel.DataAnnotations;

namespace AppNFe.Dominio.Entidades
{
    [Display(Name = "Usuário")]
    public class Usuario
    {
        #region Propriedades

        #region Codigo Usuario
        [Required(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E001")]
        [Display(Name = "Id do Usuário")]
        public long Codigo { get; set; }
        #endregion
        #region Nome de Usuario
        [Required(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E001")]
        [MaxLength(100, ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E003")]
        [Display(Name = "Nome")]
        public string Nome { get; set; }
        #endregion
        #region Senha
        [Required(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E001")]
        [MaxLength(50, ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E003")]
        [Display(Name = "Senha")]
        public string Senha { get; set; }
        #endregion
        #region E-mail
        [Required(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E001")]
        [MaxLength(255, ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E003")]
        [EmailAddress(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E004")]
        [Display(Name = "E-mail")]
        public string Email { get; set; }
        #endregion

        #endregion

    }
}