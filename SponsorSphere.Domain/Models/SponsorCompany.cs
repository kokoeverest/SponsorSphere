using SponsorSphere.Domain.Interfaces;

namespace SponsorSphere.Domain.Models
{
    public class SponsorCompany(
         string name,
         string email,
         string password,
         string country,
         string phoneNumber,
         string iban
        ) : User(name, email, password, country, phoneNumber), ISponsor
    {
        public string IBAN { get; set; } = iban;
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
