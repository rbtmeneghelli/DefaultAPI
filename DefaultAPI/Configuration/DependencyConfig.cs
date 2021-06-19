using DefaultAPI.Domain.Models;
using DefaultAPI.Infra.Data.Context;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DefaultAPI.Configuration
{
    public static class DependencyConfig
    {
        internal static void RegisterJwtConfig(IServiceCollection services, IConfiguration configuration)
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

        internal static void RegisterCorsConfig(IServiceCollection services, IConfiguration configuration)
        {
            var origins = configuration["ConnectionStrings:CorsOrigins"].Split(',');
            services.AddCors(options =>
            {
                options.AddPolicy("EnableCORS", builder =>
                {
                    builder
                    .WithOrigins(origins.ToArray())
                    .SetIsOriginAllowed((host) => true)
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials()
                    ;
                });
            });
        }

        internal static void RegisterSwaggerConfig(IServiceCollection services)
        {
            services.AddSwaggerGen(x =>
            {
                x.SwaggerDoc("V1", new OpenApiInfo
                {
                    Title = "DefaultAPIAPI",
                    Version = "V1",
                    Description = "Lista de Endpoints disponíveis",
                });
                x.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Insira a palavra Bearer com o token JWT gerado no campo",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });
                x.AddSecurityRequirement(new OpenApiSecurityRequirement {
                    { new OpenApiSecurityScheme
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

        internal static void RegisterPolicy(IServiceCollection services)
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

        internal static void MigrateDatabase(this IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<DefaultAPIContext>();
                context.Database.Migrate();
            }
        }
    }

    public class SwaggerDefaultValues : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (operation.Parameters == null) operation.Parameters = new List<OpenApiParameter>();

            var descriptor = context.ApiDescription.ActionDescriptor as ControllerActionDescriptor;
        }
    }

    // Metodo de configuração para permitir acesso ao hangFire externo
    //public class DashboardNoAuthorizationFilter : IDashBoardAuthorizationFilter
    //{
    //    public bool Authorize(DashboardContext dashboardContext) => true;
    //}

    // Adicionar ao Arquivo Startup.cs o codigo abaixo
    // App.UseHangFireDashboard("/hangfire", new DashboardOptions() {
    // Authorization = new [] { new DashboardNoAuthorizationFilter() } });
}
