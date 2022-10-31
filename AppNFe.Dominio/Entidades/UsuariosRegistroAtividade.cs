using System;
using System.Collections.Generic;

namespace AppNFe.Dominio.Entidades
{
    public class UsuariosRegistroAtividade
    {
        public long Codigo { get; set; }
        public long CodigoUsuario { get; set; }
        public string Recurso { get; set; }
        public string Detalhe { get; set; }
        public DateTime DataHora { get; set; }
        public List<UsuariosRegistroAtividadeEmpresa> Empresas { get; set; }

        public UsuariosRegistroAtividade()
        {

        }

        public UsuariosRegistroAtividade(List<UsuariosRegistroAtividadeEmpresa> empresas, long codigoUsuario, string recurso)
        {
            Empresas = empresas;
            CodigoUsuario = codigoUsuario;
            Recurso = recurso;
            DataHora = DateTime.Now;
        }

        public UsuariosRegistroAtividade(long codigoEmpresa, long codigoUsuario, string recurso, string detalhe)
        {
            Empresas = new List<UsuariosRegistroAtividadeEmpresa>();
            Empresas.Add(new UsuariosRegistroAtividadeEmpresa { CodigoEmpresa = codigoEmpresa });
            CodigoUsuario = codigoUsuario;
            Recurso = recurso;
            Detalhe = detalhe;
            DataHora = DateTime.Now;
        }

        public UsuariosRegistroAtividade(List<long> empresas, long codigoUsuario, string recurso, string detalhe)
        {
            Empresas = new List<UsuariosRegistroAtividadeEmpresa>();
            foreach (var empresa in empresas)
            {
                Empresas.Add(new UsuariosRegistroAtividadeEmpresa { CodigoEmpresa = empresa });
            }
            CodigoUsuario = codigoUsuario;
            Recurso = recurso;
            Detalhe = detalhe;
            DataHora = DateTime.Now;
        }

        public UsuariosRegistroAtividade(UsuariosRegistroAtividade registroAtividade, string detalhe)
        {
            CodigoUsuario = registroAtividade.CodigoUsuario;
            Recurso = registroAtividade.Recurso;
            Detalhe = detalhe;
            DataHora = registroAtividade.DataHora;
            Empresas = registroAtividade.Empresas;
        }
    }
}
