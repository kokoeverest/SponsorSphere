using SponsorSphere.Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace SponsorSphere.Domain.Models
{
    public class Sponsorship
    {
        public DateTime Created { get; set; } = DateTime.Now;
        public SponsorshipLevel Level { get; set; }
        public decimal Amount { get; set; }
        //public int AthleteId { get; set; }
        //public int SponsorId { get; set; }
        public Athlete AthleteSponsorship { get; set; }
        public SponsorCompany? SponsorCompany { get; set; }

        public SponsorIndividual? SponsorIndividual { get; set; }

        public Sponsorship() { }

        //[SetsRequiredMembers]
        //public Sponsorship(SponsorshipLevel level, User sponsor, Athlete athlete, decimal amount)
        //{
        //    Level = level;
        //    Amount = amount;
        //    Athlete = athlete;
        //    Sponsor = sponsor;
        //}

    }
}
