using AutoMapper;
using SponsorSphere.Application.App.Goals.Responses;
using SponsorSphere.Domain.Models;

namespace SponsorSphere.Application.Profiles
{
    public class GoalProfile : Profile
    {
        public GoalProfile()
        {
            CreateMap<Goal, GoalDto>();
        }
    }
}
