using DefaultAPI.Infra.Data.Context;
using DefaultAPI.Infra.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using DefaultAPI.Application.Interfaces;
using DefaultAPI.Application.Services;
using DefaultAPI.Application;
using System;
using KissLog;
using KissLog.AspNetCore;
using KissLog.CloudListeners.Auth;
using KissLog.CloudListeners.RequestLogsListener;
using System.Text;
using Microsoft.AspNetCore.Builder;
using System.Diagnostics;

namespace DefaultAPI.Infra.Structure.IoC
{
    public static class DependencyContainer
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<DefaultAPIContext>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IGeneralService, GeneralService>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<ICepService, CepService>();
            services.AddScoped<IRegionService, RegionService>();
            services.AddScoped<IStatesService, StatesService>();
            services.AddScoped<ICityService, CityService>();
            services.AddScoped<ILogService, LogService>();
            services.AddScoped<IAuditService, AuditService>();
            services.AddScoped<INotificationMessageService, NotificationMessageService>();
            services.AddScoped(typeof(ILoggerService<>), typeof(LoggerService<>));

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ICepRepository, CepRepository>();
            services.AddScoped<ICityRepository, CityRepository>();
            services.AddScoped<IRegionRepository, RegionRepository>();
            services.AddScoped<IStatesRepository, StateRepository>();
            services.AddScoped<ILogRepository, LogRepository>();
            services.AddScoped<IAuditRepository, AuditRepository>();

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped(typeof(IRepositoryDapper<>), typeof(RepositoryDapper<>));

            return services;
        }

        public static IServiceCollection RegisterDbConnection(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DefaultAPIContext>(opts => opts.UseSqlServer(configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly(typeof(DefaultAPIContext).Assembly.FullName)));
            return services;
        }

        //public static IServiceCollection RegisterMapperConfig(IServiceCollection services)
        //{
        //    services.AddSingleton(MapperConfigurations.CreateMap().CreateMapper());
        //    return services;
        //}

        public static IServiceCollection RegisterMapperConfig(this IServiceCollection services)
        {
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            // services.AddAutoMapper(typeof(Startup));
            return services;
        }

        //public static IServiceCollection RegisterMediator(IServiceCollection services)
        //{
        //    services.AddMediatR(Assembly.GetExecutingAssembly());
        //    return services;
        //}

        public static IServiceCollection RegisterKissLog(this IServiceCollection services)
        {
            services.AddScoped<ILogger>((context) =>
            {
                return Logger.Factory.Get();
            });

            services.AddLogging(logging =>
            {
                logging.AddKissLog();
            });

            return services;
        }

        public static IApplicationBuilder UseKissLog(this IApplicationBuilder app, IConfiguration configuration)
        {
            app.UseKissLogMiddleware(options =>
            {
                ConfigureKissLog(options, configuration);
            });

            return app;
        }

        private static void ConfigureKissLog(IOptionsBuilder options, IConfiguration configuration)
        {
            // optional KissLog configuration
            options.Options
                .AppendExceptionDetails((Exception ex) =>
                {
                    StringBuilder sb = new StringBuilder();

                    if (ex is System.NullReferenceException nullRefException)
                    {
                        sb.AppendLine("Important: check for null references");
                    }

                    return sb.ToString();
                });

            // KissLog internal logs
            options.InternalLog = (message) =>
            {
                Debug.WriteLine(message);
            };

            // register logs output
            RegisterKissLogListeners(options, configuration);
        }

        private static void RegisterKissLogListeners(IOptionsBuilder options, IConfiguration configuration)
        {
            options.Listeners.Add(new RequestLogsApiListener(new KissLog.CloudListeners.Auth.Application(
                configuration["KissLog.OrganizationId"],
                configuration["KissLog.ApplicationId"])
            )
            {
                ApiUrl = configuration["KissLog.ApiUrl"]
            });
        }

    }
}
