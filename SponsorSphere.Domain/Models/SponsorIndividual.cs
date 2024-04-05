using SponsorSphere.Domain.Interfaces;

namespace SponsorSphere.Domain.Models
{
    public class SponsorIndividual(
     string name,
     string lastName,
     string email,
     string password,
     string country,
     string birthDate
        ) : User(name, email, password, country), ISponsor
    {
        private DateTime _birthDate = DateTime.Parse(birthDate);
        public string LastName { get; set; } = lastName;
        public DateTime BirthDate => _birthDate;
        public int Age => (int)(DateTime.Now.Subtract(BirthDate).TotalDays / 365);
        public List<User> SponsoredAthletes { get; set; } = [];
        public Task BecomeSponsor()
        {
            return null;
        }
        public Task CancelSponsorship()
        {
            return null;
        }
    }
}
