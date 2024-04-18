using SponsorSphere.Domain.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace SponsorSphere.Domain.Models
{
    public class SponsorIndividual : User
    {
        public required string LastName { get; set; }
        public required DateTime BirthDate { get; set; }

        public ICollection<Sponsorship> Sponsorships { get; set; } = [];

        [NotMapped]
        public int Age => (int)(DateTime.Now.Subtract(BirthDate).TotalDays / 365);

        public SponsorIndividual() { }
        
        [SetsRequiredMembers]
        public SponsorIndividual(
         string name,
         string lastName,
         string email,
         string password,
         string country,
         string phoneNumber,
         string birthDate
        ) : base(name, email, password, country, phoneNumber)
        {
            LastName = lastName;
            BirthDate = DateTime.Parse(birthDate);
        }
    }
}
