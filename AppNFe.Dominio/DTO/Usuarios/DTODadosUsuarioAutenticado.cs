using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppNFe.Dominio.DTO.Usuarios
{
    public class DTODadosUsuarioAutenticado
    {
        public long Codigo { get; set; }
        public string Nome { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        public string Imagem { get; set; }
        public List<long> Empresas { get; set; }

    }
}
