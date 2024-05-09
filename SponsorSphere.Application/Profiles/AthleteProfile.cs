using AutoMapper;
using SponsorSphere.Application.App.Athletes.Dtos;
using SponsorSphere.Domain.Models;

namespace SponsorSphere.Application.Profiles
{
    public class AthleteProfile : Profile
    {
        public AthleteProfile()
        {
            CreateMap<Athlete, AthleteDto>();
                //.ForMember(
                //dest => dest.LastNameDto,
                //opt => opt.MapFrom(src => src.LastName)
                //);
        }
    }
}
