using System;
using System.ComponentModel.DataAnnotations;

namespace AppNFe.Core.Attributes
{
    /// <summary>
    /// Valida se a data foi informada.
    /// </summary>
    public class DataObrigatoriaAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value == null) return false;

            if ((DateTime)value > new DateTime(1900, 1, 1) && (DateTime)value < new DateTime(2100, 1, 1)) return true;

            return false;
        }
    }
}
