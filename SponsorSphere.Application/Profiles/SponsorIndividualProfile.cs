using AutoMapper;
using SponsorSphere.Application.App.SponsorIndividuals.Dtos;
using SponsorSphere.Domain.Models;

namespace SponsorSphere.Application.Profiles
{
    public class SponsorIndividualProfile : Profile
    {
        public SponsorIndividualProfile()
        {
            CreateMap<SponsorIndividual, SponsorIndividualDto>();
        }
    }
}
