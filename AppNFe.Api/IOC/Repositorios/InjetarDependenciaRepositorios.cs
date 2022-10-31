﻿using Microsoft.Extensions.DependencyInjection;
using AppNFe.Persistencia.Interfaces.Repositorios;
using AppNFe.Persistencia.Repositorios;
using AppNFe.Persistencia.Mapeadores;

namespace AppNFe.Api.IOC.Repositorios
{
    public static class InjetarDependenciaRepositorios
    {
        public static void RegistrarRepositorios(this IServiceCollection services)
        {
            services.AddScoped<IUsuarioRepositorio, UsuarioRepositorio>();
            services.AddScoped<IEmpresaRepositorio, EmpresaRepositorio>();
            services.AddScoped<IPessoaRepositorio, PessoaRepositorio>();
            services.AddScoped<IConfiguracaoFiscalRepositorio, ConfiguracaoFiscalRepositorio>();

        }
    }
}
