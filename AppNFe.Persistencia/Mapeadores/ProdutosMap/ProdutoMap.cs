using Dapper.FluentMap.Dommel.Mapping;
using AppNFe.Dominio.Entidades.Empresas;

namespace AppNFe.Persistencia.Mapeadores.EmpresasMap
{
    public class ProdutoMap : DommelEntityMap<Empresa>
    {
        public ProdutoMap()
        {
            ToTable("produto");
            Map(c => c.Codigo).ToColumn("pk_id").IsKey().IsIdentity();
            Map(c => c.Nome).ToColumn("nome");
            Map(c => c.Nome_fantasia).ToColumn("nome_fantasia");
            Map(c => c.Cnpj).ToColumn("cnpj");
            Map(c => c.Endereco).ToColumn("endereco");
            Map(c => c.Bairro).ToColumn("bairro");
            
            
        }
    }
}