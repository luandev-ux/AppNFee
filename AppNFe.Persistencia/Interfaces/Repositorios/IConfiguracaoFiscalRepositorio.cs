using AppNFe.Core.Persistencia;
using AppNFe.Core.Persistencia.Consulta;
using AppNFe.Dominio.Consulta;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;
using AppNFe.Dominio.DTO.Usuarios;
using AppNFe.Dominio.Entidades;

namespace AppNFe.Persistencia.Interfaces.Repositorios
{
    public interface IConfiguracaoFiscalRepositorio : IRepositorioBase<ConfiguracaoFiscal>
    {

    }
}
