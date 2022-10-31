using AppNFe.Persistencia.Mapeadores.EmpresasMap;
using AppNFe.Persistencia.Mapeadores.UsuariosMap;
using AppNFe.Persistencia.Mapeadores;
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
                    
                    



                    c.ForDommel();
                });
            }
        }
    }
}
