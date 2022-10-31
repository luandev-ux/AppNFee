using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace AppNFe.Core.Json.Converters
{
    /// <summary>
    /// Escrita e leitura de uma propriedade no Json do tipo DateTime com o formato padrão: yyyy-MM-ddTHH:mm:ss
    /// </summary>
    public class DateTimeConverter : JsonConverter<DateTime>
    {
        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            DateTime data;
            if (DateTime.TryParse(reader.GetString(), out data))
            {               
                return data;
            }                
            return new DateTime();
        }
        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {            
            writer.WriteStringValue(value.ToString("yyyy-MM-ddTHH:mm:ss"));
        }
    }

    /// <summary>
    /// Escrita e leitura de uma propriedade no Json do tipo DateTime com o formato: yyyy-MM-dd
    /// </summary>
    public class DataConverter : JsonConverter<DateTime>
    {
        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            DateTime data;
            if (DateTime.TryParse(reader.GetString(), out data))
            {                
                return data;
            }
            return new DateTime();
        }
        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString("yyyy-MM-dd"));
        }
    }
}
