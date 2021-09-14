using DefaultAPI.Infra.Data.Context;
using DefaultAPI.Infra.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using DefaultAPI.Application.Interfaces;
using DefaultAPI.Application.Services;
using DefaultAPI.Application;
using DefaultAPI.Infra.Structure.IoC.MapEntitiesXDto;
using System;
using System.Reflection;
using MediatR;
using DefaultAPI.Application.Queries;

namespace DefaultAPI.Infra.Structure.IoC
{
    public static class DependencyContainer
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IGeneralService, GeneralService>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<ICepService, CepService>();
            services.AddScoped<IRegionService, RegionService>();
            services.AddScoped<IStatesService, StatesService>();
            services.AddScoped<ICityService, CityService>();
            services.AddScoped<ILogService, LogService>();
            services.AddScoped<IAuditService, AuditService>();
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

    }
}
