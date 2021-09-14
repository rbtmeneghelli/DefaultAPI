using DefaultAPI.Configuration;
using DefaultAPI.Configuration.Middleware;
using DefaultAPI.Infra.Data.Context;
using DefaultAPI.Infra.Structure.IoC;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
            .AddJsonFile("appsettings.json")
            .AddEnvironmentVariables()
            .Build();

            Configuration = configuration;
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
                options.GroupNameFormat = "'v'V";
                options.SubstituteApiVersionInUrl = true;

            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.MigrateDatabase();
            app.ApiConfig();
        }
    }
}
