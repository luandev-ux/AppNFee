using Microsoft.AspNetCore.Mvc.ModelBinding;
using AppNFe.Core.Persistencia;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;

namespace AppNFe.Api.Binders
{
    public class FiltroGenericoBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            try
            {
                if (bindingContext == null)
                {
                    throw new ArgumentNullException(nameof(bindingContext));
                }

                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                var valor = bindingContext.ValueProvider.GetValue("Filtros");

                var model = JsonSerializer.Deserialize<List<FiltroGenerico>>(valor.ToString(), options);

                bindingContext.Result = ModelBindingResult.Success(model);
            }
            catch (Exception ex)
            {
                string logErro = ex.Message;
            }            
            return Task.CompletedTask;
        }
    }
}
