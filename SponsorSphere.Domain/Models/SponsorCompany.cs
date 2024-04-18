using SponsorSphere.Domain.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace SponsorSphere.Domain.Models
{
    public class SponsorCompany : User
    {
        public required string IBAN { get; set; }
        public ICollection<Sponsorship> Sponsorships { get; set; } = [];

        public SponsorCompany() { }

        [SetsRequiredMembers]
        public SponsorCompany(
             string name,
             string email,
             string password,
             string country,
             string phoneNumber,
             string iban
        ) : base(name, email, password, country, phoneNumber)
        {
            IBAN = iban;
        }

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
