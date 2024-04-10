using SponsorSphere.Application.App.Users.Responses;
using SponsorSphere.Application.Interfaces;
using SponsorSphere.Domain.Enums;
using SponsorSphere.Domain.Models;

namespace SponsorSphere.Application.App.Athletes.Responses
{
    public class AthleteDto : UserDto, IUserDto
    {
        public string LastName { get; set; } = string.Empty;
        public int Age { get; set; }
        public SportsEnum Sport { get; set; }
        public DateTime BirthDate { get; set; }
        public List<Achievement> Achievements { get; set; } = [];
        public List<Goal> Goals { get; set; } = [];
    }
}