﻿using AppNFe.Core.Attributes;
using AppNFe.Core.MensagemPadronizada;

namespace AppNFe.Dominio.Entidades.Usuario
{
    public class UsuarioEmpresa
    {
        public long Codigo { get; set; }
        public long CodigoUsuario { get; set; }
        [CodigoObrigatorio(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E001")]
        public long CodigoEmpresa { get; set; }
    }
}
