using SponsorSphere.Domain.Enums;

namespace SponsorSphere.Domain.Models
{
    public class SportEvent
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required CountryEnum Country { get; set; }
        public DateTime EventDate { get; set; }
        public bool Finished { get; set; }
        public EventsEnum EventType { get; set; }
        public SportsEnum Sport { get; set; }
    }
}
