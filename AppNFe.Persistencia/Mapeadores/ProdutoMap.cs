using AppNFe.Dominio.Entidades;
using Dapper.FluentMap.Dommel.Mapping;

namespace AppNFe.Persistencia.Mapeadores
{
    public class ProdutoMap : DommelEntityMap<Produto>
    {
        public ProdutoMap()
        {
            ToTable("produto");
            Map(c => c.Codigo).ToColumn("pk_id").IsKey().IsIdentity();
            Map(c => c.Descricao).ToColumn("descricao");
            Map(c => c.Preco).ToColumn("preco");
            Map(c => c.Ncm).ToColumn("ncm");
            Map(c => c.Custo).ToColumn("custo");
            Map(c => c.CodigoBarras).ToColumn("codigo_barras");
            Map(c => c.Unidade).ToColumn("unidade");
        }
    }
}