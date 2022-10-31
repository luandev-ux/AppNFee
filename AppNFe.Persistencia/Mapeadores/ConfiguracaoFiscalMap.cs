using Dapper.FluentMap.Dommel.Mapping;
using AppNFe.Dominio.Entidades;

namespace AppNFe.Persistencia.Mapeadores
{
    public class ConfiguracaoFiscalMap : DommelEntityMap<ConfiguracaoFiscal>
    {
        public ConfiguracaoFiscalMap()
        {
            ToTable("configuracao_fiscal");
            Map(c => c.Codigo).ToColumn("pk_id").IsKey().IsIdentity();
            Map(c => c.UfOrigem).ToColumn("uf_origem");
            Map(c => c.UfDestino).ToColumn("uf_destino");
            Map(c => c.Cfop).ToColumn("cfop");
            Map(c => c.Cst).ToColumn("cst");
            Map(c => c.AliquotaIcms).ToColumn("aliquota_icms");
            Map(c => c.CstPis).ToColumn("cst_pis");
            Map(c => c.AliquotaPis).ToColumn("aliquota_pis");
            Map(c => c.CstCofins).ToColumn("cst_cofins");
            Map(c => c.AliquotaCofins).ToColumn("aliquota_cofins");
        }
    }
}