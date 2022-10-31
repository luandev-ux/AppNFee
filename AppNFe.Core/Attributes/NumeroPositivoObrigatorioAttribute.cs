using System;
using System.ComponentModel.DataAnnotations;

namespace AppNFe.Core.Attributes
{
    /// <summary>
    /// Valida se a data informada está num período aceitável.
    /// </summary>
    public class NumeroPositivoObrigatorioAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value == null) return false;

            if (double.Parse(value.ToString()) > 0) return true;
            return false;
        }
    }
}
