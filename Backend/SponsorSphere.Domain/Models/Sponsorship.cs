﻿using SponsorSphere.Domain.Enums;

namespace SponsorSphere.Domain.Models
{
    public class Sponsorship
    {
        public DateTime Created { get; set; } = DateTime.UtcNow;
        public SponsorshipLevel Level { get; set; }
        public decimal Amount { get; set; }
        public User? Athlete { get; set; }
        public User? Sponsor { get; set; }
        public int AthleteId { get; set; }
        public int SponsorId { get; set; }
    }
}
