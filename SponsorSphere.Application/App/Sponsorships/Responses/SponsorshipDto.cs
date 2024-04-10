using SponsorSphere.Domain.Enums;
using SponsorSphere.Domain.Models;

namespace SponsorSphere.Application.App.Sponsorships.Responses
{
    public class SponsorshipDto
    {
        public int? Id { get; set; }
        public DateTime Created { get; set; }
        public SponsorshipLevel Level { get; set; }
        public decimal Amount { get; set; }
        public required User Athlete { get; set; }
        public required User Sponsor { get; set; }
    }
}
