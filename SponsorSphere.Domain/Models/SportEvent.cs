using SponsorSphere.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace SponsorSphere.Domain.Models
{
    public class SportEvent
    {
        public int? Id { get; set; }

        [MaxLength(200)]
        public required string Name { get; set; }

        [MaxLength(100)]
        public required string Country { get; set; }
        public DateTime EventDate { get; set; }
        public bool Finished { get; set; }
        public EventsEnum EventType { get; set; }
        public SportsEnum Sport { get; set; }

        public SportEvent() { }

        [SetsRequiredMembers]
        public SportEvent(SportsEnum sport, string eventName, EventsEnum eventType, string dateOfEvent, string country)
        {
            Name = eventName;
            Country = country;
            EventDate = DateTime.Parse(dateOfEvent);
            EventType = eventType;
            Sport = sport;
        }
    }
}
