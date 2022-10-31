using System.ComponentModel.DataAnnotations;

namespace AppNFe.Core.Attributes
{   
    /// <summary>
    /// Valida se o código informado é maior do que zero.
    /// </summary>
    public class CodigoObrigatorioAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value == null)
                return false;
                        
            if ((long)value > 0)
                return true;

            return false;
        }
    }
}
