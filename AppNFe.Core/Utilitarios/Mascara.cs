using System;
namespace AppNFe.Core.Utilitarios
{
    public sealed class Mascara
    {        
        public static string qtdeCasasDecimais;
        public static string FormataCasasDecimais(decimal valor)
        {
          
            //string formato = "";
            //if (string.IsNullOrEmpty(qtdeCasasDecimais))
            //{
            //    formato = "{0:0.00}";
            //    qtdeCasasDecimais = "2";
            //}
            //else
            //{
            //    formato = "{0:0.0000}";
            //    qtdeCasasDecimais = "4";
            //}
                
            return String.Format("{0:0.0000}", valor);
        }
        public static string Remover(string valor)
        {
            return valor.Replace("(", "").Replace(")", "").Replace("-", "").Replace(".", "").Replace("R$", "").Replace(",", "").Replace(" ", "");
        }
        /*
         * PagarMe
         * - O PagarMe recebe o valor no seguinte formato: 3310, que representa R$ 33,10
         */

        public static int ConverterValorPagarMe(decimal valor)
        {
            string valorString = valor.ToString("C");
            valorString = Remover(valorString);
            int valorInt = int.Parse(valorString);

            return valorInt;
        }

        public static decimal ConverterPagarMeIntToDecimal(int valor)
        {
            //10000 -> "10000" -> "100.00" -> 100.00
            string valorPagarMeString = valor.ToString();
            string valorDecimalString = valorPagarMeString.Substring(0, valorPagarMeString.Length - 2) + "," + valorPagarMeString.Substring(valorPagarMeString.Length - 2);

            var dec = decimal.Parse(valorDecimalString);

            return dec;
        }        
    }
}
