using System;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace AppNFe.Core.Json.Converters
{
    /// <summary>
    /// Escrita e leitura de uma propriedade no Json do tipo Double: Com 2 casas decimais
    /// </summary>
    public class DoubleConverter2Digits : JsonConverter<double>
    {
        public override double Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            double valor;
            try
            {                
                if (Double.TryParse(reader.GetString(), NumberStyles.Any, CultureInfo.InvariantCulture, out valor))
                    return valor;
            }
            catch (Exception)
            {
                valor = reader.GetDouble();
                return valor;
            }
            
            return 0;
        }
        public override void Write(Utf8JsonWriter writer, double value, JsonSerializerOptions options)
        {
            string valor = value.ToString("N2");
            valor = valor.Replace(".", "");
            valor = valor.Replace(",", ".");
            writer.WriteStringValue(valor);
        }
    }

    /// <summary>
    /// Escrita e leitura de uma propriedade no Json do tipo Double: Com 3 casas decimais
    /// </summary>
    public class DoubleConverter3Digits : JsonConverter<double>
    {
        public override double Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            double valor;
            try
            {
                if (Double.TryParse(reader.GetString(), NumberStyles.Any, CultureInfo.InvariantCulture, out valor))
                    return valor;
            }
            catch (Exception)
            {
                valor = reader.GetDouble();
                return valor;
            }

            return 0;
        }
        public override void Write(Utf8JsonWriter writer, double value, JsonSerializerOptions options)
        {
            string valor = value.ToString("N3");
            valor = valor.Replace(".", "");
            valor = valor.Replace(",", ".");
            writer.WriteStringValue(valor);
        }
    }

    /// <summary>
    /// Escrita e leitura de uma propriedade no Json do tipo Double: Com 4 casas decimais
    /// </summary>
    public class DoubleConverter4Digits : JsonConverter<double>
    {
        public override double Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            double valor;
            try
            {
                if (Double.TryParse(reader.GetString(), NumberStyles.Any, CultureInfo.InvariantCulture, out valor))
                    return valor;
            }
            catch (Exception)
            {
                valor = reader.GetDouble();
                return valor;
            }

            return 0;
        }
        public override void Write(Utf8JsonWriter writer, double value, JsonSerializerOptions options)
        {
            string valor = value.ToString("N4");
            valor = valor.Replace(".", "");
            valor = valor.Replace(",", ".");
            writer.WriteStringValue(valor);
        }
    }
}
