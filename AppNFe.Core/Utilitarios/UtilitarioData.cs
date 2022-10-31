using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppNFe.Core.Utilitarios
{
    public class UtilitarioData
    {
        /// <summary>
        /// Converter uma string no formato: dd/MM/yyyy para uma propriedade DateTime valida
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static DateTime StringToDateTime(string data)
        {
            string[] formats = { "dd/MM/yyyy" };

            DateTime dataInicioAtividade;
            if (DateTime.TryParseExact(data, formats, new CultureInfo("pt-BR"),
                                      DateTimeStyles.None, out dataInicioAtividade))
            {
                return dataInicioAtividade;
            };

            return new DateTime();
        }

        /// <summary>
        /// Verifica se o dia, mês e ano data foi preenchido e se é válido
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>        
        public static bool VerificarData(DateTime data)
        {
            if ((data.Year >= 2000 && data.Year <= 9999) && (data.Month >= 1 && data.Month <= 12) && (data.Day >= 1 && data.Day <= 31))
                return true;            

            return false;
        }

        public static string ToDiaMesAnoHoraMinutoSegundo(DateTime data)
        {
            return data.ToString("dd/MM/yyyy HH:mm:ss");
        }
    }
}
