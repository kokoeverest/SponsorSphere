using AutoMapper;
using SponsorSphere.Application.App.Users.Dtos;
using SponsorSphere.Domain.Models;

namespace SponsorSphere.Application.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDto>();

            CreateMap<UpdateUserDto, UserDto>();
        }
    }
}
