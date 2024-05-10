using AutoMapper;
using SponsorSphere.Application.App.Athletes.Dtos;
using SponsorSphere.Application.Common.Converters;
using SponsorSphere.Domain.Models;

namespace SponsorSphere.Application.Profiles
{
    public class AthleteProfile : Profile
    {
        public AthleteProfile()
        {
            CreateMap<Athlete, AthleteDto>();

            CreateMap<RegisterAthleteDto, Athlete>()
                .ForMember(dest => dest.BirthDate, opt => opt.ConvertUsing(new StringToDateConverter()))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email));
        }
    }
}
