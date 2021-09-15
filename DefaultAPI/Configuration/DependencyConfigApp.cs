using DefaultAPI.Infra.Data.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DefaultAPI.Configuration
{
    public static class DependencyConfigApp
    {
        internal static void MigrateDatabase(this IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<DefaultAPIContext>();
                context.Database.Migrate();
            }
        }

        internal static void ApiConfig(this IApplicationBuilder app)
        {
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseCors("EnableCORS");
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            //app.UseMiddleware<ApiLoggingMiddleware>(); // Caso for usar Middleware no net core 2.2, basta descomentar essa linha
            //app.UseMiddleware<RequestResponseLoggingMiddleware>(); // Caso for usar Middleware no net core 3.0, basta descomentar essa linha
            //app.UseMiddleware<HttpLoggingMiddleware>(); // Caso for usar Middleware no net core 5.0, basta descomentar essa linha
        }
    }
}
