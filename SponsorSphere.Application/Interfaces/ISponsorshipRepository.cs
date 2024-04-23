using SponsorSphere.Domain.Enums;
using SponsorSphere.Domain.Models;

namespace SponsorSphere.Application.Interfaces
{
    public interface ISponsorshipRepository
    {
        Task<Sponsorship> CreateAsync(Sponsorship sponsorship);
        Task<Sponsorship> Update(Sponsorship sponsorship);
        Task<int> Delete(Sponsorship sponsorship);
        Task<Sponsorship> GetByIdAsync(int id);
        Task<List<Sponsorship>> GetByAthleteIdAsync(int athleteId);
        Task<List<Sponsorship>> GetBySponsorIdAsync(int sponsorId);
        Task<List<Sponsorship>> GetByLevelAsync(SponsorshipLevel level);
        Task<List<Sponsorship>> GetByAmountAsync(decimal amount);

    }
}
