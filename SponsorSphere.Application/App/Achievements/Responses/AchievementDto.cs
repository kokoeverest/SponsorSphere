using SponsorSphere.Domain.Enums;
using SponsorSphere.Domain.Models;

namespace SponsorSphere.Application.App.Achievements.Responses
{
    public class AchievementDto
    {
        public int? Id { get; set; }
        public SportsEnum Sport { get; set; }
        public required SportEvent EventType { get; set; }
        public ushort? PlaceFinished { get; set; }
        public required User Athlete { get; set; }
    }
}
