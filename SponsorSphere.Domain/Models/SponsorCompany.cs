using System.ComponentModel.DataAnnotations.Schema;

namespace SponsorSphere.Domain.Models
{
    public class SponsorCompany : User
    {
        public required string IBAN { get; set; }
    }
}
