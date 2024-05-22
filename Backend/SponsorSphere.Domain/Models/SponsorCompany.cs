namespace SponsorSphere.Domain.Models
{
    public class SponsorCompany : Sponsor
    {
        public required string IBAN { get; set; }
    }
}
