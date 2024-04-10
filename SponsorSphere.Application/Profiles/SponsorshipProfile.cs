using AutoMapper;
using SponsorSphere.Application.App.Sponsorships.Responses;
using SponsorSphere.Domain.Models;

namespace SponsorSphere.Application.Profiles
{
    public class SponsorshipProfile : Profile
    {
        public SponsorshipProfile()
        {
            CreateMap<Sponsorship, SponsorshipDto>();
        }
    }
}
