using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace AppNFe.Core.Attributes
{    
    /// <summary>
    /// Obriga o preenchimento de pelo menos um item na lista de objetos.
    /// </summary>
    public class ListaObrigatoriaAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var lista = value as IList;
            if (lista != null)
            {
                return lista.Count > 0;
            }
            return false;
        }
    }
}
