using DefaultAPI.Domain.Models;
using DefaultAPI.Infra.Data.Context;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;

namespace DefaultCQRSAPI.Configuration
{
    public static class DependencyConfig
    {
        internal static void RegisterSwaggerConfig(IServiceCollection services)
        {
            services.AddSwaggerGen(x =>
            {
                x.SwaggerDoc("v1", GetApiConfig("v1"));
                x.AddSecurityDefinition("Bearer", GetBearerConfig());
                x.AddSecurityRequirement(GetSecurityConfig());
            });
        }

        internal static void RegisterConfigs(IServiceCollection services, IConfiguration configuration)
        {
            var configEmail = new EmailSettings();
            configuration.Bind("EmailSettings", configEmail);
            services.AddSingleton(configEmail);

            var tokenConfiguration = new TokenConfiguration();
            configuration.Bind("TokenConfiguration", tokenConfiguration);
            services.AddSingleton(tokenConfiguration);
        }

        internal static void MigrateDatabase(this IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<DefaultAPIContext>();
                context.Database.Migrate();
            }
        }

        /// <summary>
        /// Para mais detalhes sobre CQRS: http://www.macoratti.net/20/07/aspc_mediatr1.htm
        /// </summary>
        /// <param name="services"></param>
        internal static void RegisterMediator(IServiceCollection services)
        {
            services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());
        }

        private static OpenApiSecurityScheme GetBearerConfig()
        {
            return new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Insira a palavra Bearer com o token JWT gerado no campo",
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey
            };
        }

        private static OpenApiSecurityRequirement GetSecurityConfig()
        {
            return new OpenApiSecurityRequirement {
                { new OpenApiSecurityScheme
                  {
                    Reference = new OpenApiReference
                    {
                      Type = ReferenceType.SecurityScheme,
                      Id = "Bearer"
                    }
                  },
                  new string[] { }
            }   };
        }

        private static OpenApiInfo GetApiConfig(string version, bool isNotDepreciated = true)
        {
            return new OpenApiInfo
            {
                Title = "DefaultCQRSAPI",
                Version = version,
                Description = isNotDepreciated ? "Lista de Endpoints disponíveis" : "Endpoints descontinuados",
                Contact = new OpenApiContact() { Name = "Roberto Meneghelli", Email = "teste@teste.com.br" },
                License = new OpenApiLicense() { Name = "ROMAR SOFTWARE" }
            };
        }
    }
}
