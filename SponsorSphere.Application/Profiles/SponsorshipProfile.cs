using AutoMapper;
using SponsorSphere.Application.App.Sponsorships.Dtos;
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
