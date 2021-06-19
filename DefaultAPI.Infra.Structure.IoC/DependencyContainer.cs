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

namespace DefaultAPI.Infra.Structure.IoC
{
    public static class DependencyContainer
    {
        public static IServiceCollection RegisterServices(IServiceCollection services)
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

        public static IServiceCollection RegisterDbConnection(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DefaultAPIContext>(opts => opts.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            return services;
        }

        //public static IServiceCollection RegisterMapperConfig(IServiceCollection services)
        //{
        //    services.AddSingleton(MapperConfigurations.CreateMap().CreateMapper());
        //    return services;
        //}

        public static IServiceCollection RegisterMapperConfig(IServiceCollection services)
        {
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            return services;
        }

    }
}
