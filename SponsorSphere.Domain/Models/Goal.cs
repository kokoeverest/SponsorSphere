using SponsorSphere.Domain.Enums;

namespace SponsorSphere.Domain.Models
{
    public class Goal(SportEvent eventType, decimal amount, User athlete)
    {
        public int? Id { get; set; } = null;
        public DateTime Date { get; set; } = eventType.EventDate;
        public SportEvent EventType { get; set; } = eventType;
        public SportsEnum Sport { get; set; } = eventType.Sport;
        public decimal AmountNeeded { get; set; } = amount;
        public User Athlete { get; set; } = athlete;
    }
}
