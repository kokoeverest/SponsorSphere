using SponsorSphere.Domain.Enums;

namespace SponsorSphere.Application.App.Sponsorships.Dtos
{
    public class SponsorshipDto
    {
        public DateTime Created { get; set; }
        public SponsorshipLevel Level { get; set; }
        public decimal Amount { get; set; }
        public required int AthleteId { get; set; }
        public required int SponsorId { get; set; }
    }
}
