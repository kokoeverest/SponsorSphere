using System.ComponentModel.DataAnnotations.Schema;

namespace SponsorSphere.Domain.Models
{
    public class SponsorIndividual : Sponsor
    {
        public required string LastName { get; set; }
        public required DateTime BirthDate { get; set; }

        //[NotMapped]
        public int Age => (int)(DateTime.UtcNow.Subtract(BirthDate).TotalDays / 365.2425);
    }
}
