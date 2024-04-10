using SponsorSphere.Domain.Enums;

namespace SponsorSphere.Domain.Models
{
    public class Achievement(Athlete athlete, SportEvent sportEvent, ushort placeFinished)
    {
        public int? Id { get; set; }
        public SportsEnum Sport { get; set; } = sportEvent.Sport;
        public SportEvent EventType { get; set; } = sportEvent;
        public ushort? PlaceFinished { get; set; } = placeFinished;
        public User Athlete { get; set; } = athlete;
    }
}