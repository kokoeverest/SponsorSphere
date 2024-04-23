using SponsorSphere.Domain.Enums;

namespace SponsorSphere.Domain.Models
{
    public class Achievement
    {
        public SportsEnum Sport { get; set; }
        public int SportEventId { get; set; }
        public SportEvent? SportEvent { get; set; }
        public ushort? PlaceFinished { get; set; }
        public int AthleteId { get; set; }
        public Athlete? Athlete { get; set; }
    }
}