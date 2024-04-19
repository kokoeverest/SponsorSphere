namespace SponsorSphere.Domain.Models
{
    public class SponsorCompany : User
    {
        public required string IBAN { get; set; }
        public ICollection<Sponsorship> Sponsorships { get; set; } = [];
    }
}
