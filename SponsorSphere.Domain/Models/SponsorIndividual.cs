using System.ComponentModel.DataAnnotations.Schema;

namespace SponsorSphere.Domain.Models
{
    public class SponsorIndividual : User
    {
        public required string LastName { get; set; }
        public required DateTime BirthDate { get; set; }

        [NotMapped]
        public int Age => (int)(DateTime.Now.Subtract(BirthDate).TotalDays / 365);
    }
}
