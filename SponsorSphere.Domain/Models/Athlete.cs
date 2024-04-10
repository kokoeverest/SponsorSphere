using SponsorSphere.Domain.Enums;

namespace SponsorSphere.Domain.Models
{
    public class Athlete(
        string name,
        string lastName,
        string email,
        string password,
        string country,
        string phoneNumber,
        string birthDate,
        SportsEnum sport
        ) : User(name,
        email,
        password,
        country,
        phoneNumber
        )
    {
        public string LastName { get; set; } = lastName;
        public SportsEnum Sport { get; set; } = sport;
        public DateTime BirthDate { get; set; } = DateTime.Parse(birthDate);

        public List<Achievement> Achievements { get; set; } = [];
        public List<Goal> Goals { get; set; } = [];

        public int Age => (int)(DateTime.Now.Subtract(BirthDate).TotalDays / 365);
    }
}
