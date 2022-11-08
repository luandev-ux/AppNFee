using AppNFe.Dominio.Entidades.Pessoas;
using Dapper.FluentMap.Dommel.Mapping;


namespace AppNFe.Persistencia.Mapeadores
{
    public class MovimentoMap : DommelEntityMap<Movimento>
    {
        public MovimentoMap()
        {
            ToTable("movimento");
            Map(c => c.Codigo).ToColumn("pk_id").IsKey().IsIdentity();
            Map(c => c.CodigoPessoa).ToColumn("fk_pessoa");
            Map(c => c.Data).ToColumn("data");
            Map(c => c.Numero).ToColumn("numero");
            Map(c => c.Serie).ToColumn("serie");
            Map(c => c.Modelo).ToColumn("modelo");
            Map(c => c.ValorTotal).ToColumn("valor_total");
            Map(c => c.Desconto).ToColumn("desconto");
            Map(c => c.OutrasDespesas).ToColumn("outras_despesas");
            Map(c => c.Frete).ToColumn("frete");
            Map(c => c.ChaveDeAcesso).ToColumn("chave_de_acesso");
            Map(c => c.TipoMovimentacao).ToColumn("tipo_movimentacao");
        }
    }
    public class PessoasMap : DommelEntityMap<Pessoa>
    {
        public PessoasMap()
        {
            ToTable("pessoa");
            Map(c => c.Codigo).ToColumn("pk_id").IsKey().IsIdentity();
            Map(c => c.Nome).ToColumn("nome");
            Map(c => c.NomeFantasia).ToColumn("nome_fantasia");
            Map(c => c.CnpjCpf).ToColumn("cnpj_cpf");
            Map(c => c.InscricaoEstadual).ToColumn("inscricao_estadual");
            Map(c => c.Endereco).ToColumn("endereco");
            Map(c => c.Bairro).ToColumn("bairro");
            Map(c => c.Cidade).ToColumn("cidade");
            Map(c => c.Uf).ToColumn("uf");
            Map(c => c.Cep).ToColumn("cep");
            Map(c => c.Telefone).ToColumn("telefone");
            Map(c => c.Email).ToColumn("email");
            Map(c => c.TipoPessoa).ToColumn("tipo_pessoa");
        }
    }
}
