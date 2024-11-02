using Application.ViewModels.AuthenDTO;
using Application.ViewModels.FriendDTO;
using Application.ViewModels.UserDTO;
using AutoMapper;
using Domain.Models;

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
            CreateMap<User, ViewUserDTO>()
           .ForMember(dest => dest.NameZodiac, opt => opt.MapFrom(src => src.Zodiac.NameZodiac))
           .ForMember(dest => dest.Decription, opt => opt.MapFrom(src => src.Zodiac.DesZodiac));
            CreateMap<User, ViewUserDTO>().ReverseMap();
            CreateMap<Zodiac, ViewUserDTO>().ReverseMap();
            CreateMap<User, UpdateUserDTO>().ReverseMap();
            CreateMap<Friend, FriendReqDTO>().ReverseMap();
            CreateMap<Friend, FriendResDTO>()
            .ForMember(dest => dest.FriendName, opt => opt.MapFrom(src => src.FriendNavigation.FullName))
            .ReverseMap();

        }
    }
}
