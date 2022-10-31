using AppNFe.Core.MensagemPadronizada;
using System.ComponentModel.DataAnnotations;

namespace AppNFe.Dominio.DTO
{
    public class DadosLogin
    {
        [Required(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E001")]
        [MaxLength(60, ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E003")]
        [Display(Name = "Login")]
        public string Login { get; set; }

        [Required(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E001")]
        [Display(Name = "Senha")]
        public string Senha { get; set; }
    }
}
