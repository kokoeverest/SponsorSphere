using SponsorSphere.Domain.Enums;

namespace SponsorSphere.Domain.Models
{
    public class Goal(string date, EventsEnum eventType, SportsEnum sport, decimal amount)
    {
        public DateTime Date { get; set; } = DateTime.Parse(date);
        public EventsEnum EventType { get; set; } = eventType;
        public SportsEnum Sport { get; set; } = sport;
        public decimal AmountNeeded { get; set; } = amount;
    }
}
