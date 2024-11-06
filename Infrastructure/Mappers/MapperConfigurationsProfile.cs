using Application.ViewModels.AuthenDTO;
using Application.ViewModels.FriendDTO;
using Application.ViewModels.LikeZodiacDTO;
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
           .ReverseMap();
            CreateMap<Zodiac, ViewUserDTO>().ReverseMap();
            CreateMap<User, UpdateUserDTO>().ReverseMap();
            CreateMap<Friend, FriendReqDTO>().ReverseMap();
            CreateMap<Friend, FriendResDTO>()
                .ForMember(dest => dest.FriendName, opt => opt.MapFrom(src => src.FriendNavigation.FullName))
                .ForMember(dest => dest.ZodiacName, opt => opt.MapFrom(src => src.FriendNavigation.Zodiac.NameZodiac)) // Assuming ZodiacName exists
                .ReverseMap();
            CreateMap<LikeZodiac, LikeZodiacDTO>()
                .ForMember(dest => dest.NameZodiac, opt => opt.MapFrom(src => src.ZodiacLike.NameZodiac))
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.User.FullName))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.User.Email))
                .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.User.Gender))
                .ForMember(dest => dest.TelephoneNumber, opt => opt.MapFrom(src => src.User.TelephoneNumber)).ReverseMap();
            CreateMap<CreateLikeZodiacDTO, LikeZodiac>().ReverseMap();


        }
    }
}
