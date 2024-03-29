using System.Linq;
using API.DTOs;
using API.Entities;
using API.Extensions;
using AutoMapper;

namespace API.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<AppUser, MemberDto>()
            .ForMember(dest => dest.PhotoUrl, opt => opt.MapFrom(src => src.Photos.FirstOrDefault().Url))
            .ForMember(dest => dest.Age, opt => opt.MapFrom(src => src.DateOfBirth.CalculateAge()));
            CreateMap<Photo, PhotoDto>()
            .ForMember(dest => dest.Nickname, opt => opt.MapFrom(src => src.AppUser.NickName));

            CreateMap<Commentt, CommentDto>();
            CreateMap<Like, LikeDto>();
        
            CreateMap<RegisterDto, AppUser>();
        }
    }
}