using AppNFe.Core.Persistencia;
using AppNFe.Core.Persistencia.Consulta;
using AppNFe.Dominio.Consulta;
using System.Collections.Generic;
using AppNFe.Dominio.Entidades.Usuario;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;
using AppNFe.Dominio.DTO.Usuarios;

namespace AppNFe.Persistencia.Interfaces.Repositorios
{
    public interface IUsuarioRepositorio : IRepositorioBase<Usuario>
    {

    }
}
