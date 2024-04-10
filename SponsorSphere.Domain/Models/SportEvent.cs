using SponsorSphere.Domain.Enums;

namespace SponsorSphere.Domain.Models
{
    public class SportEvent(SportsEnum sport, string eventName, EventsEnum eventType, string dateOfEvent, string country)
    {
        public int? Id { get; set; }
        public string Name { get; set; } = eventName;
        public string Country { get; set; } = country;
        public DateTime EventDate { get; set; } = DateTime.Parse(dateOfEvent);
        public EventsEnum EventType { get; set; } = eventType;
        public SportsEnum Sport { get; set; } = sport;
    }
}
