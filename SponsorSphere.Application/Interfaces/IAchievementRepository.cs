using SponsorSphere.Application.App.Achievements.Responses;
using SponsorSphere.Domain.Models;

namespace SponsorSphere.Application.Interfaces
{
    public interface IAchievementRepository
    {
        Task<Achievement> CreateAsync(Achievement achievement);
        Task<List<Achievement>> GetAllAsync(int athleteId);
        Task<int> DeleteAsync(int sportEventId, int athleteId);
        Task<AchievementDto> UpdateAsync(AchievementDto updatedAchievement);
    }
}
