using Dapper.FluentMap;
using Dapper.FluentMap.Dommel;

namespace AppNFe.Persistencia.Mapeadores
{
    public static class MapeamentoDB
    {
        public static void CriarMapeamento()
        {
            if (FluentMapper.EntityMaps.IsEmpty)
            {
                FluentMapper.Initialize(c =>
                {
                    c.AddMap(new UsuarioMap());
                    c.AddMap(new EmpresaMap());
                    c.AddMap(new PessoaMap());
                    c.AddMap(new ClienteMap());
                    c.AddMap(new FornecedorMap());
                    c.AddMap(new MovimentoMap());
                    c.AddMap(new ConfiguracaoFiscalMap());
                    c.ForDommel();
                });
            }
        }
    }
}
