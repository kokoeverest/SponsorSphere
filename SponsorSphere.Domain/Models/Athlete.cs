using SponsorSphere.Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace SponsorSphere.Domain.Models
{
    public class Athlete : User
    {
        public string LastName { get; set; } = string.Empty;
        public SportsEnum Sport { get; set; }
        public DateTime BirthDate { get; set; }
        public ICollection<Achievement> Achievements { get; set; } = [];
        public ICollection<Goal> Goals { get; set; } = [];
        public int Age => (int)(DateTime.UtcNow.Subtract(BirthDate).TotalDays / 365.2425);
    }
}
