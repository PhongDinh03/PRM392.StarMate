using Application.ViewModels.AuthenDTO;
using Application.ViewModels.UserDTO;
using AutoMapper;
using Infrastructure.Models;

namespace Infrastructure.Mappers
{
    public class MapperConfigurationsProfile : Profile
    {
        public MapperConfigurationsProfile() 
        {
            CreateMap<User, RegisterDTO>().ReverseMap();
            CreateMap<User, LoginDTO>().ReverseMap();
            CreateMap<ResetPassDTO, User>()
                 .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Password));
            CreateMap<User, ResetPassDTO>();
            CreateMap<User, ViewUserDTO>().ReverseMap();
            CreateMap<User, UpdateUserDTO>().ReverseMap();
        }
    }
}
