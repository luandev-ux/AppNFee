using System;
using System.Globalization;

namespace AppNFe.Core.Utilitarios
{
    public static class UtilitarioNumerico
    {
        public static bool VerificarNumerico(string valor)
        {
            double numero = 0;
            return double.TryParse(valor, out numero);
        }

        /// <summary>
        /// Converter uma string em valor Double
        /// </summary>
        /// <param name="valor"></param>
        /// <returns></returns>
        public static Double StringToDouble(string valor)
        {
            double valorRetorno;

            if (Double.TryParse(valor, NumberStyles.Any, CultureInfo.InvariantCulture, out valorRetorno))
                return valorRetorno;            

            return new Double();
        }       
    }
}
