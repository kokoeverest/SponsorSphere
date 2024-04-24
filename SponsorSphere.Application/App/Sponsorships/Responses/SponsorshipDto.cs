using SponsorSphere.Application.App.Users.Responses;
using SponsorSphere.Domain.Enums;

namespace SponsorSphere.Application.App.Sponsorships.Responses
{
    public class SponsorshipDto
    {
        public int Id { get; set; }
        public DateTime Created { get; set; }
        public SponsorshipLevel Level { get; set; }
        public decimal Amount { get; set; }
        public required int AthleteId { get; set; }
        public required int SponsorId { get; set; }
    }
}
