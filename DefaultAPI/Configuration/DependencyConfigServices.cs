using DefaultAPI.Domain.Models;
using DefaultAPI.Infra.Data.Context;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace DefaultAPI.Configuration
{
    public static class DependencyConfigServices
    {

        internal static void RegisterJwtConfig(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication
                  (x =>
                  {
                      x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                      x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                  })
                  .AddJwtBearer(options =>
                  {
                      options.RequireHttpsMetadata = false;
                      options.SaveToken = true;
                      options.TokenValidationParameters = new TokenValidationParameters
                      {
                          ValidateIssuer = false,
                          ValidateAudience = false,
                          ValidateLifetime = true,
                          ValidateIssuerSigningKey = true,
                          ClockSkew = TimeSpan.Zero,
                          ValidIssuer = configuration["TokenConfiguration:Issuer"],
                          ValidAudience = configuration["TokenConfiguration:Audience"],
                          IssuerSigningKey = new SymmetricSecurityKey
                          (Encoding.UTF8.GetBytes(configuration["TokenConfiguration:Key"]))
                      };
                  });

            services.AddAuthorization(auth =>
            {
                auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme‌​)
                    .RequireAuthenticatedUser().Build());
            });
        }

        internal static void RegisterCorsConfig(this IServiceCollection services, IConfiguration configuration)
        {
            var origins = configuration["ConnectionStrings:CorsOrigins"].Split(',');
            services.AddCors(options =>
            {
                options.AddPolicy("EnableCORS", builder =>
                {
                    builder
                    //.WithMethods() // Configuração de tipos de metodos que serão liberados para consumo GET, POST, PUT, DELETE
                    .WithOrigins(origins.ToArray()) // Configuração de sites que tem permissão para acessar a API
                    .SetIsOriginAllowed((host) => true)
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials()
                    ;
                });
            });
        }

        internal static void RegisterConfigs(this IServiceCollection services, IConfiguration configuration)
        {
            var configEmail = new EmailSettings();
            configuration.Bind("EmailSettings", configEmail);
            services.AddSingleton(configEmail);

            var tokenConfiguration = new TokenConfiguration();
            configuration.Bind("TokenConfiguration", tokenConfiguration);
            services.AddSingleton(tokenConfiguration);
        }

        internal static void RegisterPolicy(this IServiceCollection services)
        {
            services.AddMvc(config =>
            {
                var policy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .Build();

                config.Filters.Add(new AuthorizeFilter(policy));
            }).SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
        }

        internal static void RegisterHangFireConfig(this IServiceCollection services, IConfiguration Configuration)
        {
            //services.AddHangfire(x => x.UseSqlServerStorage(Configuration["ConnectionString:DefaultConnection"]));
            //services.AddHangfireServer();
            //return services;
        }

        internal static void RegisterRedisConfig(this IServiceCollection services, IConfiguration Configuration)
        {
            //services.AddDistributedRedisCache(options =>
            //{
            //    options.Configuration = "localhost:6379";
            //    options.InstanceName = "HUBDATABASE - ";
            //});
        }
    }

    public class DefaultAPIContextFactory : IDesignTimeDbContextFactory<DefaultAPIContext>
    {
        /// <summary>
        /// Se for necessario, remover <PrivateAssets>all</PrivateAssets> da referência ao pacote Microsoft.EntityFrameworkCore.Design no arquivo de projeto. Assim a referência a este pacote ficou definida assim:
        /// <PackageReference Include = "Microsoft.EntityFrameworkCore.Design" Version="5.0.3">
        /// <IncludeAssets>runtime; build; native; contentfiles; analyzers; buildtransitive</IncludeAssets>
        /// </PackageReference>
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public DefaultAPIContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var builder = new DbContextOptionsBuilder<DefaultAPIContext>();
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            builder.UseSqlServer(connectionString);

            return new DefaultAPIContext(builder.Options);
        }
    }
}
