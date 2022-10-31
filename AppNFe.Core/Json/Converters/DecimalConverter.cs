using System;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace AppNFe.Core.Json.Converters
{
    /// <summary>
    /// Escrita e leitura de uma propriedade no Json do tipo Decimal: Com 2 casas decimais
    /// </summary>
    public class DecimalConverter2Digits : JsonConverter<decimal>
    {
        public override decimal Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            decimal valor;
            try
            {
                if (Decimal.TryParse(reader.GetString(), NumberStyles.Any, CultureInfo.InvariantCulture, out valor))
                    return valor;
            }
            catch (Exception)
            {
                valor = reader.GetDecimal();
                return valor;
            }
            return 0;
        }
        public override void Write(Utf8JsonWriter writer, decimal value, JsonSerializerOptions options)
        {
            string valor = value.ToString("N2");
            valor = valor.Replace(".", "");
            valor = valor.Replace(",", ".");
            writer.WriteStringValue(valor);
        }
    }

    /// <summary>
    /// Escrita e leitura de uma propriedade no Json do tipo Decimal: Com 3 casas decimais
    /// </summary>
    public class DecimalConverter3Digits : JsonConverter<decimal>
    {
        public override decimal Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            decimal valor;
            try
            {
                if (Decimal.TryParse(reader.GetString(), NumberStyles.Any, CultureInfo.InvariantCulture, out valor))
                    return valor;
            }
            catch (Exception)
            {
                valor = reader.GetDecimal();
                return valor;
            }
            return 0;
        }
        public override void Write(Utf8JsonWriter writer, decimal value, JsonSerializerOptions options)
        {
            string valor = value.ToString("N3");
            valor = valor.Replace(".", "");
            valor = valor.Replace(",", ".");
            writer.WriteStringValue(valor);
        }
    }

    /// <summary>
    /// Escrita e leitura de uma propriedade no Json do tipo Decimal: Com 4 casas decimais
    /// </summary>
    public class DecimalConverter4Digits : JsonConverter<decimal>
    {
        public override decimal Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            decimal valor;
            try
            {
                if (Decimal.TryParse(reader.GetString(), NumberStyles.Any, CultureInfo.InvariantCulture, out valor))
                    return valor;
            }
            catch (Exception)
            {
                valor = reader.GetDecimal();
                return valor;
            }
            return 0;
        }
        public override void Write(Utf8JsonWriter writer, decimal value, JsonSerializerOptions options)
        {
            string valor = value.ToString("N4");
            valor = valor.Replace(".", "");
            valor = valor.Replace(",", ".");
            writer.WriteStringValue(valor);
        }
    }
}
