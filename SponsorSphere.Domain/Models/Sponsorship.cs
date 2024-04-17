using SponsorSphere.Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace SponsorSphere.Domain.Models
{
    public class Sponsorship
    {
        public DateTime Created { get; set; } = DateTime.Now;
        public SponsorshipLevel Level { get; set; }
        public decimal Amount { get; set; }
        public int AthleteId { get; set; }
        public int SponsorId { get; set; }

        [NotMapped]
        public required User Athlete { get; set; }

        [NotMapped]
        public required User Sponsor { get; set; }

        public Sponsorship() { }
        public Sponsorship(SponsorshipLevel level, User sponsor, User athlete, decimal amount)
        {
            Level = level;
            Amount = amount;
            Athlete = athlete;
            Sponsor = sponsor;
        }

    }
}
