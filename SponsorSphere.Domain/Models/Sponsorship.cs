﻿using SponsorSphere.Domain.Enums;

namespace SponsorSphere.Domain.Models
{
    public class Sponsorship
    {
        public DateTime Created { get; set; } = DateTime.Now;
        public SponsorshipLevel Level { get; set; }
        public decimal Amount { get; set; }
        public int AthleteId { get; set; }
        public required Athlete Athlete { get; set; }
        public int? SponsorCompanyId { get; set; }
        public SponsorCompany? SponsorCompany { get; set; }
        public int? SponsorIndividualId { get; set; }
        public SponsorIndividual? SponsorIndividual { get; set; }
    }
}
