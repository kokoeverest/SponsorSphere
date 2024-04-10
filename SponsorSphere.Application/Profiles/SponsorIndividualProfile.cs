using AutoMapper;
using SponsorSphere.Application.App.SponsorCompanies.Responses;
using SponsorSphere.Domain.Models;

namespace SponsorSphere.Application.Profiles
{
    public class SponsorIndividualProfile : Profile
    {
        public SponsorIndividualProfile()
        {
            CreateMap<SponsorIndividual, SponsorCompanyDto>();
        }
    }
}
