using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace AppNFe.Core.Attributes
{
    /// <summary>
    /// Valida telefone informado com possibilidade de ser obrigatório ou não.
    /// </summary>
    public class TelefoneAttribute : ValidationAttribute
    {
        private bool PreenchimentoObrigatorio;

        /// <summary>
        /// Valida telefone informado - Preenchimento obrigatório.
        /// </summary>
        public TelefoneAttribute()
        {
            PreenchimentoObrigatorio = true;
        }

        /// <summary>
        /// Valida telefone informado - Preenchimento obrigatório.
        /// </summary>
        public TelefoneAttribute(bool preenchimentoObrigatorio)
        {
            PreenchimentoObrigatorio = preenchimentoObrigatorio;
        }

        public override bool IsValid(object value)
        {
            
            if (PreenchimentoObrigatorio)
            {
                if (value == null) return false;
                if (string.IsNullOrEmpty(value.ToString())) return false;                
            }
            else
            {
                if (value == null) return true;
                if (string.IsNullOrEmpty(value.ToString())) return true;
            }

            string valorPreenchido = value.ToString();

            if (valorPreenchido.Length < 8)
                return false;

            if (valorPreenchido.Length > 14)
                return false;
            
            return Regex.IsMatch(valorPreenchido, @"^\d+$");
        }
    }
}
