using DefaultAPI.Configuration;
using DefaultAPI.Infra.Data.Context;
using DefaultAPI.Infra.Structure.IoC;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
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
            DependencyContainer.RegisterDbConnection(services, Configuration);
            DependencyContainer.RegisterServices(services);
            DependencyContainer.RegisterMapperConfig(services);
            services.AddHttpContextAccessor();
            DependencyConfig.RegisterCorsConfig(services, Configuration);
            DependencyConfig.RegisterJwtConfig(services, Configuration);
            DependencyConfig.RegisterSwaggerConfig(services);
            DependencyConfig.RegisterConfigs(services, Configuration);
            DependencyConfig.RegisterPolicy(services);
            services.AddControllers();
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
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseCors("EnableCORS");
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/V1/swagger.json", "DefaultAPIAPI");
            });
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
