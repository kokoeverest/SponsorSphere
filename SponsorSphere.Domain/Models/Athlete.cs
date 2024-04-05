namespace SponsorSphere.Domain.Models
{
    public class Athlete(
        string name,
        string lastName,
        string email,
        string password,
        string country,
        string phone,
        string birthDate,
        string sport
        ) : User(name,
        email,
        password,
        country
        )
    {
        public string Sport { get; set; } = sport;
        public string LastName { get; set; } = lastName;
        public string PhoneNumber { get; set; } = phone;
        public DateTime BirthDate { get; set; } = DateTime.Parse(birthDate);

        public List<User> Sponsors { get; set; } = [];
        public List<Achievement> Achievements { get; set; } = [];
        public List<Goal> Goals { get; set; } = [];

        public int Age => (int)(DateTime.Now.Subtract(BirthDate).TotalDays / 365);
    }
}
