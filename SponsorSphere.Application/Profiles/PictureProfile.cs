using AutoMapper;
using SponsorSphere.Application.App.Pictures.Responses;

namespace SponsorSphere.Application.Profiles
{
    public class PictureProfile : Profile
    {
        public PictureProfile()
        {
            CreateMap<PictureProfile, PictureDto>();
        }
    }
}