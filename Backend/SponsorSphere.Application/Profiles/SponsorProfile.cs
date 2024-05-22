using AutoMapper;
using SponsorSphere.Application.App.Sponsors.Dtos;
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
