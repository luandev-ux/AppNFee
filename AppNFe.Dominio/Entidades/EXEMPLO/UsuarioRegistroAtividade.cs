using System;
using System.Collections.Generic;

namespace AppNFe.Dominio.Entidades.Usuario
{
    public class UsuarioRegistroAtividade
    {
        public long Codigo { get; set; }
        public long CodigoUsuario { get; set; }
        public string Recurso { get; set; }
        public string Detalhe { get; set; }
        public DateTime DataHora { get; set; }
        public List<UsuarioRegistroAtividadeEmpresa> Empresas { get; set; }

        public UsuarioRegistroAtividade()
        {

        }

        public UsuarioRegistroAtividade(List<UsuarioRegistroAtividadeEmpresa> empresas, long codigoUsuario, string recurso)
        {
            Empresas = empresas;
            CodigoUsuario = codigoUsuario;
            Recurso = recurso;
            DataHora = DateTime.Now;
        }

        public UsuarioRegistroAtividade(long codigoEmpresa, long codigoUsuario, string recurso, string detalhe)
        {
            Empresas = new List<UsuarioRegistroAtividadeEmpresa>();
            Empresas.Add(new UsuarioRegistroAtividadeEmpresa { CodigoEmpresa = codigoEmpresa });
            CodigoUsuario = codigoUsuario;
            Recurso = recurso;
            Detalhe = detalhe;
            DataHora = DateTime.Now;
        }

        public UsuarioRegistroAtividade(List<long> empresas, long codigoUsuario, string recurso, string detalhe)
        {
            Empresas = new List<UsuarioRegistroAtividadeEmpresa>();
            foreach (var empresa in empresas)
            {
                Empresas.Add(new UsuarioRegistroAtividadeEmpresa { CodigoEmpresa = empresa });
            }
            CodigoUsuario = codigoUsuario;
            Recurso = recurso;
            Detalhe = detalhe;
            DataHora = DateTime.Now;
        }

        public UsuarioRegistroAtividade(UsuarioRegistroAtividade registroAtividade, string detalhe)
        {
            CodigoUsuario = registroAtividade.CodigoUsuario;
            Recurso = registroAtividade.Recurso;
            Detalhe = detalhe;
            DataHora = registroAtividade.DataHora;
            Empresas = registroAtividade.Empresas;
        }
    }
}
