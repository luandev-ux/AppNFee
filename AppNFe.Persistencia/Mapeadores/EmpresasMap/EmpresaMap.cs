using Dapper.FluentMap.Dommel.Mapping;
using AppNFe.Dominio.Entidades.Empresas;

namespace AppNFe.Persistencia.Mapeadores.EmpresasMap
{
    public class EmpresaMap : DommelEntityMap<Empresa>
    {
        public EmpresaMap()
        {
            ToTable("empresa");
            Map(c => c.Codigo).ToColumn("pk_id").IsKey().IsIdentity();
            Map(c => c.Nome).ToColumn("nome");
            Map(c => c.Nome_fantasia).ToColumn("nome_fantasia");
            Map(c => c.Cnpj).ToColumn("cnpj");
            Map(c => c.Endereco).ToColumn("endereco");
            Map(c => c.Bairro).ToColumn("bairro");
            Map(c => c.Numero).ToColumn("numero");
            Map(c => c.Uf).ToColumn("uf");
            Map(c => c.Cep).ToColumn("cep");
            Map(c => c.Inscricao_estadual).ToColumn("inscricao_estadual");
            Map(c => c.Telefone).ToColumn("telefone");
            Map(c => c.Ultimo_nfe).ToColumn("ultimo_nfe");
            Map(c => c.Ultimo_nfce).ToColumn("ultimo_nfce");
            Map(c => c.Serie_nfe).ToColumn("serie_nfe");
            Map(c => c.Serie_nfce).ToColumn("serie_nfce");
            Map(c => c.Perc_simples).ToColumn("perc_simples");
            Map(c => c.Certificado_digital).ToColumn("certificado_digital");
            Map(c => c.Tipo_certificado).ToColumn("tipo_certificado");
            Map(c => c.Logo).ToColumn("logo");

        }
    }
}