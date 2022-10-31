using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;

namespace AppNFe.Core.Utilitarios
{
    public static class UtilitarioEnumerador
    {

        public static string ObterDescricaoEnumerador(Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());

            DescriptionAttribute[] attributes = fi.GetCustomAttributes(typeof(DescriptionAttribute), false) as DescriptionAttribute[];

            if (attributes != null && attributes.Any())
            {
                return attributes.First().Description;
            }

            return value.ToString();
        }

        public static void ObtenhaListaValoresEnumerador(Enum e)
        {
            //Type eType = e.GetType();

            //string ddescricao = "";
            //foreach (var c in e.GetProperties())
            //{
            //    DescriptionAttribute[] attributes = c.GetCustomAttributes(typeof(DescriptionAttribute), false) as DescriptionAttribute[];
            //    ddescricao += attributes.First().Description;
            //}

        }
        
    }
}
