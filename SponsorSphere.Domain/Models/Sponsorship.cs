using SponsorSphere.Domain.Enums;

namespace SponsorSphere.Domain.Models
{

    public class Sponsorship(SponsorshipLevel level, User sponsor, User athlete, decimal amount)
    {
        public int? Id { get; set; } = null;
        public DateTime Created { get; set; } = DateTime.Now;
        public SponsorshipLevel Level { get; set; } = level;
        public decimal Amount { get; set; } = amount;
        public User Athlete { get; set; } = athlete;
        public User Sponsor { get; set; } = sponsor;
    }
}
