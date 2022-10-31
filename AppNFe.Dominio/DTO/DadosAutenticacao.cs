using AppNFe.Dominio.Entidades;
using System;

namespace AppNFe.Dominio.DTO
{
    public class DadosAutenticacao
    {
        public Usuario Usuario { get; set; }
        public string Token { get; set; }
        public DateTime ExpiracaoToken { get; set; }
    }
}
