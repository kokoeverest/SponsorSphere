using SponsorSphere.Domain.Models;

namespace SponsorSphere.Domain.Interfaces
{
    public interface ISponsor
    {
        public ICollection<Sponsorship> Sponsorships { get; set; }
        public Task? BecomeSponsor();
        public Task? CancelSponsorship();
    }
}
