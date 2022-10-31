using AppNFe.Dominio.Entidades.Empresas;
using AppNFe.Persistencia.Interfaces;
using AppNFe.Persistencia.Interfaces.Repositorios;
using Serilog;

namespace AppNFe.Persistencia.Repositorios.EmpresaRepositorio
{
    public class EmpresaRepositorio : RepositorioBase<Empresa>, IEmpresaRepositorio
    {
        public EmpresaRepositorio(IGerenteConexao gerenteConexao, ILogger logger) : base(gerenteConexao, logger) { }
    }
}
