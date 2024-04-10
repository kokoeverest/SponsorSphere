using AutoMapper;
using SponsorSphere.Application.App.Achievements.Responses;
using SponsorSphere.Domain.Models;

namespace SponsorSphere.Application.Profiles
{
    public class AchievementProfile : Profile
    {
        public AchievementProfile()
        {
            CreateMap<Achievement, AchievementDto>();
        }
    }
}
