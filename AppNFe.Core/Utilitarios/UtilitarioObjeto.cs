using AppNFe.Core.Json.Converters;
using System.Text.Json;

namespace AppNFe.Core.Utilitarios
{
    public static class UtilitarioObjeto
    {
        public static bool ObjetosIguais(object primeiroObjeto, object segundoObjeto)
        {

            JsonSerializerOptions options = new JsonSerializerOptions();
            options.Converters.Add(new DateTimeConverter());

            string jsonPrimeiroObjeto = JsonSerializer.Serialize(primeiroObjeto, options);
            string jsonSegundoObjeto = JsonSerializer.Serialize(segundoObjeto, options);

            return (jsonPrimeiroObjeto == jsonSegundoObjeto);
        }
    }
}
