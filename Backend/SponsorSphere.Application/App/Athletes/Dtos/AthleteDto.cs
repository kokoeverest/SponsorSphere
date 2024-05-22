using SponsorSphere.Application.App.Achievements.Dtos;
using SponsorSphere.Application.App.Goals.Dtos;
using SponsorSphere.Application.App.Users.Dtos;
using SponsorSphere.Application.Interfaces;
using SponsorSphere.Domain.Enums;

namespace SponsorSphere.Application.App.Athletes.Dtos
{
    public class AthleteDto : UserDto, IUserDto
    {
        public string LastName { get; set; } = string.Empty;
        public int Age { get; set; }
        public SportsEnum Sport { get; set; }
        public DateTime BirthDate { get; set; }
        public ICollection<AchievementDto> Achievements { get; set; } = [];
        public ICollection<GoalDto> Goals { get; set; } = [];
    }
}