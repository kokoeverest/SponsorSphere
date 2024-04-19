using SponsorSphere.Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace SponsorSphere.Domain.Models
{
    public class Athlete : User
    {
        public required string LastName { get; set; }
        public SportsEnum Sport { get; set; }
        public DateTime BirthDate { get; set; }
        public ICollection<Achievement> Achievements { get; set; } = [];
        public ICollection<Goal> Goals { get; set; } = [];
        public ICollection<Sponsorship> Sponsorships { get; set; } = [];

        [NotMapped]
        public int Age => (int)(DateTime.Now.Subtract(BirthDate).TotalDays / 365);
    }
}
