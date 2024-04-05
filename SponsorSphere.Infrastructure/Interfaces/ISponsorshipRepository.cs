using SponsorSphere.Domain.Enums;
using SponsorSphere.Domain.Models;

namespace SponsorSphere.Infrastructure.Interfaces
{
    public interface ISponsorshipRepository
    {
        Sponsorship Create(Sponsorship sponsorship);
        Sponsorship Update(Sponsorship sponsorship);
        bool Delete(Sponsorship sponsorship);
        Sponsorship GetById(int id);
        List<Sponsorship> GetByAthleteId(int athleteId);
        List<Sponsorship> GetBySponsorId(int sponsorId);
        List<Sponsorship> GetByLevel(SponsorshipLevel level);
        List<Sponsorship> GetByAmount(decimal amount);

    }
}
