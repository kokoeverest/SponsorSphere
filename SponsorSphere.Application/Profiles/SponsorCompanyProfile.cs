using AutoMapper;
using SponsorSphere.Application.App.SponsorCompanies.Responses;
using SponsorSphere.Domain.Models;

namespace SponsorSphere.Application.Profiles
{
    public class SponsorCompanyProfile : Profile
    {
        public SponsorCompanyProfile()
        {
            CreateMap<SponsorCompany, SponsorCompanyDto>();
        }
    }
}
