using SponsorSphere.Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace SponsorSphere.Domain.Models
{
    public class Goal
    {
        public required DateTime Date { get; set; }
        public int SportEventId { get; set; }
        public required SportEvent EventType { get; set; }
        public required SportsEnum Sport { get; set; }
        public required decimal AmountNeeded { get; set; }
        public int AthleteId { get; set; }

        [NotMapped]
        public required User Athlete { get; set; }

        public Goal() { }

        public Goal(SportEvent eventType, decimal amount, User athlete)
        {
            Date = eventType.EventDate;
            EventType = eventType;
            Sport = eventType.Sport;
            AmountNeeded = amount;
            Athlete = athlete;
        }
    }
}
