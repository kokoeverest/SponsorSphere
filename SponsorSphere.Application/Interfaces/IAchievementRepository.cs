using SponsorSphere.Domain.Models;

namespace SponsorSphere.Application.Interfaces
{
    public interface IAchievementRepository
    {
        Task<Achievement> CreateAsync(Achievement achievement);
        Task<List<Achievement>> GetAllAsync(Athlete athlete);
        Task<int> DeleteAsync(Achievement achievement);
        void Update(Achievement achievement);
    }
}
