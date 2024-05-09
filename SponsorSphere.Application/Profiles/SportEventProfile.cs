using AutoMapper;
using SponsorSphere.Application.App.SportEvents.Dtos;
using SponsorSphere.Domain.Models;

namespace SponsorSphere.Application.Profiles
{
    public class SportEventProfile : Profile
    {
        public SportEventProfile()
        {
            CreateMap<SportEvent, SportEventDto>();
        }
    }
}
