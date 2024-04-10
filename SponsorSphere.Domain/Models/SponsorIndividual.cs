using SponsorSphere.Domain.Interfaces;

namespace SponsorSphere.Domain.Models
{
    public class SponsorIndividual(
     string name,
     string lastName,
     string email,
     string password,
     string country,
     string phoneNumber,
     string birthDate
        ) : User(name, email, password, country, phoneNumber), ISponsor
    {
        public string LastName { get; set; } = lastName;
        public DateTime BirthDate { get; set; } = DateTime.Parse(birthDate);
        public int Age => (int)(DateTime.Now.Subtract(BirthDate).TotalDays / 365);
        public Task? BecomeSponsor()
        {
            return null;
        }
        public Task? CancelSponsorship()
        {
            return null;
        }
    }
}
