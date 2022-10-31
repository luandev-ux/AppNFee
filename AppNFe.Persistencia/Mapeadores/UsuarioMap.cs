using Dapper.FluentMap.Dommel.Mapping;
using AppNFe.Dominio.Entidades;

namespace AppNFe.Persistencia.Mapeadores
{
    public class UsuarioMap : DommelEntityMap<Usuario>
    {
        public UsuarioMap()
        {
            ToTable("usuario");
            Map(c => c.Codigo).ToColumn("pk_id").IsKey().IsIdentity();
            Map(c => c.Nome).ToColumn("nome");
            Map(c => c.Senha).ToColumn("senha");
            Map(c => c.Email).ToColumn("email");
        }
    }
}