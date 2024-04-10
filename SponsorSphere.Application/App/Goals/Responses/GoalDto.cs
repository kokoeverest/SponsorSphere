using SponsorSphere.Domain.Enums;
using SponsorSphere.Domain.Models;

namespace SponsorSphere.Application.App.Goals.Responses
{
    public class GoalDto
    {
        public int? Id { get; set; }
        public DateTime Date { get; set; }
        public required SportEvent EventType { get; set; }
        public SportsEnum Sport { get; set; }
        public decimal AmountNeeded { get; set; }
        public required User Athlete { get; set; }
    }
}
