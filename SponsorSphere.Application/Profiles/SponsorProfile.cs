using AutoMapper;
using SponsorSphere.Application.App.Sponsors.Responses;
using SponsorSphere.Domain.Models;

namespace SponsorSphere.Application.Profiles
{
    public class SponsorProfile : Profile
    {
        public SponsorProfile()
        {
            CreateMap<Sponsor, SponsorDto>();
        }
    }
}
