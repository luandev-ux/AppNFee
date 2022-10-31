using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace AppNFe.Core.Utilitarios
{
    public class UtilitarioTexto
    {
        public static string MascararCPF(string cpf)
        {
            return Convert.ToUInt64(cpf).ToString(@"000\.000\.000\-00");
        }
        public static string MascararCNPJ(string cnpj)
        {
            return Convert.ToUInt64(cnpj).ToString(@"00\.000\.000\/0000\-00");
        }                
        public static string MascararCEP(string cep)
        {
            if (string.IsNullOrEmpty(cep))
                return "";

            return Convert.ToUInt64(cep).ToString(@"00\.000\-000");
        }

        public static string MascararTelefone(string telefone)
        {
            if (string.IsNullOrEmpty(telefone))
                return "";

            // por omissão tem 10 ou menos dígitos
            string strMascara = "{0:(00)0000-0000}";
            // converter o texto em número
            long lngNumero = Convert.ToInt64(telefone);

            if (telefone.Length == 11)
                strMascara = "{0:(00)00000-0000}";

            return string.Format(strMascara, lngNumero);            
        }

        public static long ExtrairCodigoPedido(string codigoPedido)
        {
            if (string.IsNullOrEmpty(codigoPedido))
                return 0;

            try
            {
                string[] resultadoSeparacao = codigoPedido.Split("-");

                return long.Parse(resultadoSeparacao[1]);
            }
            catch (Exception)
            {
                return 0;
            }            
        }

        public static string RetornarApenasNumeros(string texto)
        {
            if (string.IsNullOrEmpty(texto))
                return "";
            
            return Regex.Replace(texto, @"[^\d]", "");
        }

        public static string RetornarApenasLetrasENumeros(string texto)
        {
            if (string.IsNullOrEmpty(texto))
                return "";

            return Regex.Replace(texto, "[^0-9a-zA-Z]+", "");
        }

        public static string ValidaValorNaoPreenchido(string texto)
        {
            if (string.IsNullOrEmpty(texto))
                return "Não preenchido";

            return texto;
        }

        public static string ValidaValorNaoPreenchida(string texto)
        {
            if (string.IsNullOrEmpty(texto))
                return "Não preenchida";

            return texto;
        }

        public static string ObtenhaTextoComLimiteCaracteres(string texto, int tamanho)
        {
            try
            {
                if (string.IsNullOrEmpty(texto))
                    return "";

                if (tamanho > 0 && texto.Length > tamanho)
                    return texto.Substring(0, tamanho);
            }
            catch
            {
                return "";
            }

            return texto;
        }

        public static string RemoverAcentos(string texto)
        {
            string comAcentos = "ÄÅÁÂÀÃäáâàãÉÊËÈéêëèÍÎÏÌíîïìÖÓÔÒÕöóôòõÜÚÛüúûùÇç";
            string semAcentos = "AAAAAAaaaaaEEEEeeeeIIIIiiiiOOOOOoooooUUUuuuuCc";

            for (int i = 0; i < comAcentos.Length; i++)
            {
                texto = texto.Replace(comAcentos[i].ToString(), semAcentos[i].ToString());
            }
            return texto;
        }

        /// <summary>
        /// Remove acentos pra consultas ou condições no banco de dados sempre transformando texto minusculo
        /// </summary>
        /// <param name="texto"></param>
        /// <returns></returns>
        public static string RemoverAcentosPadraoDB(string texto)
        {
            if (string.IsNullOrEmpty(texto))
                return "";
            
            return RemoverAcentos(texto.ToLower());
        }

        public static string UrlAmigavel(string texto)
        {                
            texto = texto.ToLower();
            texto = Regex.Replace(texto, @"\s+", "-");
            texto = Regex.Replace(texto, @"/[áàãâä]/g", "a");
            texto = Regex.Replace(texto, @"/[éèêë]/g", "e");
            texto = Regex.Replace(texto, @"/[íìîï]/g", "i");
            texto = texto.Replace("í", "i");
            texto = Regex.Replace(texto, @"/[óòõôö]/g", "o");
            texto = Regex.Replace(texto, @"/[úùûü]/g", "u");
            texto = Regex.Replace(texto, @"/[ç]/g", "c");
            texto = Regex.Replace(texto, @"/[^\w-]+/g", "");

            return texto;
        }
        public static string AdicionarQuebraDeLinhaInicio(string texto)
        {            
            return Environment.NewLine + texto;
        }
        public static string AdicionarQuebraDeLinhaFim(string texto)
        {
            return texto + Environment.NewLine;
        }

        public static bool VerificaApenasNumeros(string valor)
        {
            if (string.IsNullOrEmpty(valor))
                return false;

            long numero = 0;
            return long.TryParse(valor, out numero);
        }
    }
}
