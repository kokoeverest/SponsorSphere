using AutoMapper;
using SponsorSphere.Application.App.Goals.Responses;

namespace SponsorSphere.Application.Profiles
{
    public class GoalProfile : Profile
    {
        public GoalProfile()
        {
            CreateMap<GoalProfile, GoalDto>();
        }
    }
}
