using DefaultAPI.Configuration;
using DefaultAPI.Infra.Structure.IoC;
using KissLog;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DefaultAPI
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public IWebHostEnvironment HostingEnvironment { get; }

        public Startup(IWebHostEnvironment hostingEnvironment)
        {
            HostingEnvironment = hostingEnvironment;

            var configuration = new ConfigurationBuilder()
            .SetBasePath(hostingEnvironment.ContentRootPath)
            .AddJsonFile("appsettings.json", true, true)
            .AddJsonFile($"appsettings.{hostingEnvironment.EnvironmentName}.json", true, true)
            .AddEnvironmentVariables();

            if (hostingEnvironment.IsDevelopment())
            {
                configuration.AddUserSecrets<Startup>();
            }

            Configuration = configuration.Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.RegisterDbConnection(Configuration);
            services.RegisterServices();
            services.RegisterMapperConfig();
            services.RegisterCorsConfig(Configuration);
            services.RegisterJwtConfig(Configuration);
            services.RegisterSwaggerConfig();
            services.RegisterConfigs(Configuration);
            services.AddHttpContextAccessor();
            services.RegisterPolicy();
            services.AddControllers();
            services.AddApiVersioning(options =>
            {
                options.ReportApiVersions = true;
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = new ApiVersion(1, 0);
            });
            services.AddVersionedApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VV";
                options.SubstituteApiVersionInUrl = true;

            });
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

            services.AddScoped<ILogger>((context) =>
            {
                return Logger.Factory.Get();
            });

            services.RegisterKissLog();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider provider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // Middleware para tratamento de exceção sem exibir o erro da aplicação para o cliente...
                // E necessario criar um metodo na controller principal para que todos possam utilizar
                app.UseExceptionHandler("/errors");
                app.UseHsts();
            }

            app.MigrateDatabase();
            app.ApiConfig();
            app.UseKissLog(Configuration);
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.UseSwaggerConfig(provider);
        }
    }
}
