using SponsorSphere.Application.App.Sponsorships.Responses;
using SponsorSphere.Domain.Enums;
using SponsorSphere.Domain.Models;

namespace SponsorSphere.Application.Interfaces
{
    public interface ISponsorshipRepository
    {
        Task<Sponsorship> CreateAsync(Sponsorship sponsorship);
        Task<SponsorshipDto> UpdateAsync(SponsorshipDto sponsorship);
        Task<int> DeleteAsync(int athleteId, int sponsorId);
        Task<List<Sponsorship>> GetByAthleteIdAsync(int athleteId);
        Task<List<Sponsorship>> GetBySponsorIdAsync(int sponsorId);
        Task<List<Sponsorship>> GetByLevelAsync(SponsorshipLevel level);
    }
}
