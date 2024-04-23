﻿using SponsorSphere.Domain.Enums;

namespace SponsorSphere.Domain.Models
{
    public class Goal
    {
        public required DateTime Date { get; set; }
        public int SportEventId { get; set; }
        public SportEvent? SportEvent { get; set; }
        public required SportsEnum Sport { get; set; }
        public required decimal AmountNeeded { get; set; }
        public int AthleteId { get; set; }
        public Athlete? Athlete { get; set; }
    }
}
