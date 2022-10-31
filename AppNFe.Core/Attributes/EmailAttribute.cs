using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace AppNFe.Core.Attributes
{
    /// <summary>
    /// Valida E-mail informado com possibilidade de ser obrigatório ou não.
    /// </summary>
    public class EmailAttribute : ValidationAttribute
    {
        private bool PreenchimentoObrigatorio;

        /// <summary>
        /// Valida E-mail informado - Preenchimento obrigatório.
        /// </summary>
        public EmailAttribute()
        {
            PreenchimentoObrigatorio = true;
        }

        /// <summary>
        /// Valida E-mail informado - Preenchimento obrigatório.
        /// </summary>
        public EmailAttribute(bool preenchimentoObrigatorio)
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
            
            return Regex.IsMatch(value.ToString(), @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9] ]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{ 1,3})(\]?)$");
        }
    }
}
