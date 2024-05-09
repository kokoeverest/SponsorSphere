using SponsorSphere.Domain.Enums;

namespace SponsorSphere.Application.App.Achievements.Dtos
{
    public class AchievementDto
    {
        public SportsEnum Sport { get; set; }
        public int SportEventId { get; set; }
        public ushort? PlaceFinished { get; set; }
        public int AthleteId { get; set; }
    }
}
