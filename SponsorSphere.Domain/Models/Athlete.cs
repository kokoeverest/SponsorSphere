using SponsorSphere.Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace SponsorSphere.Domain.Models
{
    public class Athlete : User
    {
        public required string LastName { get; set; }
        public SportsEnum Sport { get; set; }
        public DateTime BirthDate { get; set; }

        //[NotMapped]
        public ICollection<Achievement> Achievements { get; set; } = [];

        //[NotMapped]
        public ICollection<Goal> Goals { get; set; } = [];

        public ICollection<Sponsorship> Sponsorships { get; set; } = [];

        [NotMapped]
        public int Age => (int)(DateTime.Now.Subtract(BirthDate).TotalDays / 365);

        public Athlete() { }

        [SetsRequiredMembers]
        public Athlete(
            string name,
            string lastName,
            string email,
            string password,
            string country,
            string phoneNumber,
            string birthDate,
            SportsEnum sport
        ) : base(name,
            email,
            password,
            country,
            phoneNumber
        )
        {
            LastName = lastName;
            Sport = sport;
            BirthDate = DateTime.Parse(birthDate);
        }
    }
}
