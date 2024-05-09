using AutoMapper;
using SponsorSphere.Application.App.Achievements.Dtos;
using SponsorSphere.Application.App.Goals.Dtos;
using SponsorSphere.Application.App.SportEvents.Dtos;
using SponsorSphere.Application.Common.Converters;
using SponsorSphere.Domain.Models;

namespace SponsorSphere.Application.Profiles
{
    public class SportEventProfile : Profile
    {
        public SportEventProfile()
        {
            CreateMap<SportEvent, SportEventDto>();
            CreateMap<CreateAchievementDto, SportEvent>();
            CreateMap<CreateGoalDto, SportEvent>();
            CreateMap<CreateSportEventDto, SportEvent>()
                .ForMember(dest => dest.EventDate, opt => opt.ConvertUsing(new StringToDateConverter()));
        }
    }
}
