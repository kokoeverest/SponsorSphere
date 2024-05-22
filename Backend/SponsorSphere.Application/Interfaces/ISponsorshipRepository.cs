using SponsorSphere.Application.App.Sponsorships.Dtos;
using SponsorSphere.Domain.Enums;
using SponsorSphere.Domain.Models;

namespace SponsorSphere.Application.Interfaces
{
    public interface ISponsorshipRepository
    {
        Task<Sponsorship> CreateAsync(Sponsorship sponsorship);
        Task<SponsorshipDto> UpdateAsync(SponsorshipDto sponsorship);
        Task<int> DeleteAsync(int athleteId, int sponsorId);
        Task<Sponsorship?> GetSponsorshipAsync(int athleteId, int sponsorId);
        Task<List<Sponsorship>> GetByAthleteIdAsync(int athleteId, int pageNumber, int pageSize);
        Task<List<Sponsorship>> GetBySponsorIdAsync(int sponsorId, int pageNumber, int pageSize);
        Task<List<Sponsorship>> GetByLevelAsync(SponsorshipLevel level, int pageNumber, int pageSize);
    }
}
