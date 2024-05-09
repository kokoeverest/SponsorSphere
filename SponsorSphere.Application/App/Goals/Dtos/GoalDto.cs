using SponsorSphere.Domain.Enums;

namespace SponsorSphere.Application.App.Goals.Dtos
{
    public class GoalDto
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public required int SportEventId { get; set; }
        public SportsEnum Sport { get; set; }
        public decimal AmountNeeded { get; set; }
        public required int AthleteId { get; set; }
    }
}
