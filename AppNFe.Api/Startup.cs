using Amazon.Runtime;
using AWS.Logger;
using AWS.Logger.SeriLog;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using AppNFe.Core.Json.Converters;
using AppNFe.Api.IOC.Repositorios;
using AppNFe.Api.IOC.Servicos;
using AppNFe.Api.Middlewares;
using AppNFe.Persistencia;
using AppNFe.Persistencia.Interfaces;
using Serilog;
using System;
using System.IO;
using System.Reflection;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace AppNFe.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            services.AddControllers().AddJsonOptions(options => {
                options.JsonSerializerOptions.Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping;
                options.JsonSerializerOptions.Converters.Add(new DateTimeConverter());                
            });
            services.AddHttpClient();
            
            AWSLoggerConfig configuracaoAWSLogger = new AWSLoggerConfig("AppNFe.Api")
            {
                Region = "us-east-1",
                Credentials = new BasicAWSCredentials(Configuration.GetValue<string>("Amazon:AccessKey"), Configuration.GetValue<string>("Amazon:SecretKey"))
            };

            Log.Logger = new LoggerConfiguration().WriteTo.AWSSeriLog(configuracaoAWSLogger).CreateLogger();
            services.AddSingleton(Log.Logger);
            
            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = Configuration.GetValue<string>("ServidorRedis");
            });

            services.AddScoped<IGerenteConexao, GerenteConexao>();            
            services.AddScoped(serviceProvider => new GerenteConexao(Configuration, Log.Logger));
            services.RegistrarRepositorios();            
            services.RegistrarServicos();
            
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "AppNFe", Version = "v1", Description = "Documentação da API de Emissor NF-e." });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath, includeControllerXmlComments: true);
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Por favor, informe o seu Token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                {
                    new OpenApiSecurityScheme
                    {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                    },
                    new string[] { }
                }
                });
            });            

            var key = Encoding.ASCII.GetBytes(Configuration.GetValue<string>("ChaveCriptografia"));
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
                x.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        var accessToken = context.Request.Query["access_token"];                        
                        var path = context.HttpContext.Request.Path;
                        if (!string.IsNullOrEmpty(accessToken) &&
                            (path.StartsWithSegments("/hub-comunicacao")))
                        {                            
                            context.Token = accessToken;
                        }
                        return Task.CompletedTask;
                    }
                };
            });

            services.Configure<Microsoft.AspNetCore.Mvc.ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

            services.AddSignalR();
            services.AddCors(options =>
            {
                options.AddPolicy(name: "SignalRCors",
                    builder =>
                    {
                        builder.SetIsOriginAllowed(origin => true)
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials();
                    });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseMiddleware<ContratanteMappingMiddleware>();
            app.UseCors("SignalRCors");
            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()
                .WithExposedHeaders("X-Paginacao"));

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {                
                endpoints.MapControllers();                
                endpoints.MapControllerRoute(name: "default", pattern: "v1/{contratante}/{controller=Home}/{action=Index}/{id?}");
            });
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.RoutePrefix = string.Empty;
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                c.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.None);
            });
        }
    }
}
