using AutoMapper;
using SponsorSphere.Application.App.SponsorCompanies.Dtos;
using SponsorSphere.Domain.Models;

namespace SponsorSphere.Application.Profiles
{
    public class SponsorCompanyProfile : Profile
    {
        public SponsorCompanyProfile()
        {
            CreateMap<SponsorCompany, SponsorCompanyDto>();
            CreateMap<RegisterSponsorCompanyDto, SponsorCompany>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email));
        }
    }
}
