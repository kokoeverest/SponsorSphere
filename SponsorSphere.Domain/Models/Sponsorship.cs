namespace SponsorSphere.Domain.Models
{

    public enum SponsorshipLevel
    {
        Monthly, Annual, SinglePayment // FundRaiser and Donation might be the same?
    }

    public class Sponsorship(SponsorshipLevel level, User sponsor, User athlete, decimal amount)
    {
        public DateTime Created { get; set; } = DateTime.Now;
        public SponsorshipLevel Level { get; set; } = level;
        public User Sponsor { get; set; } = sponsor;
        public User Athlete { get; set; } = athlete;
        public decimal Amount { get; set; } = amount;
    }
}
