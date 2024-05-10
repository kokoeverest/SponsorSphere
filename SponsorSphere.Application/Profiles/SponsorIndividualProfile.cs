using AutoMapper;
using SponsorSphere.Application.App.SponsorIndividuals.Dtos;
using SponsorSphere.Application.Common.Converters;
using SponsorSphere.Domain.Models;

namespace SponsorSphere.Application.Profiles
{
    public class SponsorIndividualProfile : Profile
    {
        public SponsorIndividualProfile()
        {
            CreateMap<SponsorIndividual, SponsorIndividualDto>();
            CreateMap<RegisterSponsorIndividualDto, SponsorIndividual>()
                .ForMember(dest => dest.BirthDate, opt => opt.ConvertUsing(new StringToDateConverter()))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email));
        }
    }
}
