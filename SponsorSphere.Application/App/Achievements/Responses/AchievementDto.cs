using SponsorSphere.Application.App.SportEvents.Responses;
using SponsorSphere.Domain.Enums;
using SponsorSphere.Domain.Models;

namespace SponsorSphere.Application.App.Achievements.Responses
{
    public class AchievementDto
    {
        public SportsEnum Sport { get; set; }
        public int SportEventId { get; set; }
        //public required SportEventDto EventType { get; set; }
        public ushort? PlaceFinished { get; set; }
        public int AthleteId { get; set; }
        //public required User Athlete { get; set; }
    }
}
