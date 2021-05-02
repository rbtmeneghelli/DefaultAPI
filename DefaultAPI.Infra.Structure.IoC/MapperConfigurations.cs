using AutoMapper;
using DefaultAPI.Domain.Dto;
using DefaultAPI.Domain.Entities;

namespace DefaultAPI.Infra.Structure.IoC.MapEntitiesXDto
{
    public static class MapperConfigurations
    {
        public static MapperConfiguration CreateMap()
        {
            return new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<UserSendDto, User>()
                .ForMember(dest => dest.Id, act => act.MapFrom(src => src.Id.HasValue ? src.Id : null))
                .ForMember(dest => dest.IsActive, act => act.MapFrom(src => src.IsActive))
                .ForMember(dest => dest.IsAuthenticated, act => act.MapFrom(src => src.IsAuthenticated))
                .ForMember(dest => dest.LastPassword, act => act.MapFrom(src => src.LastPassword))
                .ForMember(dest => dest.Login, act => act.MapFrom(src => src.Login))
                .ForMember(dest => dest.Password, act => act.MapFrom(src => src.Password))
                .ForMember(dest => dest.IdProfile, act => act.MapFrom(src => src.IdProfile));

                cfg.CreateMap<User, UserReturnedDto>()
                .ForMember(dest => dest.IsActive, act => act.MapFrom(src => src.IsActive))
                .ForMember(dest => dest.IsAuthenticated, act => act.MapFrom(src => src.IsAuthenticated))
                .ForMember(dest => dest.LastPassword, act => act.MapFrom(src => src.LastPassword))
                .ForMember(dest => dest.Login, act => act.MapFrom(src => src.Login))
                .ForMember(dest => dest.Password, act => act.MapFrom(src => src.Password));

                cfg.CreateMap<Log, LogReturnedDto>()
                .ForMember(dest => dest.Class, act => act.MapFrom(src => src.Class))
                .ForMember(dest => dest.Method, act => act.MapFrom(src => src.Method))
                .ForMember(dest => dest.MessageError, act => act.MapFrom(src => src.MessageError))
                .ForMember(dest => dest.Object, act => act.MapFrom(src => src.Object))
                .ForMember(dest => dest.UpdateTime, act => act.MapFrom(src => src.UpdateTime))
                .ForMember(dest => dest.Id, act => act.MapFrom(src => src.Id));

                cfg.CreateMap<Audit, AuditReturnedDto>()
                .ForMember(dest => dest.TableName, act => act.MapFrom(src => src.TableName))
                .ForMember(dest => dest.ActionName, act => act.MapFrom(src => src.ActionName))
                .ForMember(dest => dest.Id, act => act.MapFrom(src => src.Id))
                .ForMember(dest => dest.KeyValues, act => act.MapFrom(src => src.KeyValues))
                .ForMember(dest => dest.NewValues, act => act.MapFrom(src => src.NewValues))
                .ForMember(dest => dest.OldValues, act => act.MapFrom(src => src.OldValues));
            });
        }
    }
}
