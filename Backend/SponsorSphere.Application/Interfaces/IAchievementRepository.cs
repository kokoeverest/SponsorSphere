using SponsorSphere.Application.App.Achievements.Dtos;
using SponsorSphere.Domain.Models;

namespace SponsorSphere.Application.Interfaces
{
    public interface IAchievementRepository
    {
        /// <summary>
        /// This method creates a new achievemnet
        /// </summary>
        /// <param name="achievement"></param>
        /// <returns></returns>
        Task<Achievement> CreateAsync(Achievement achievement);
        Task<List<Achievement>> GetAllAsync(int athleteId, int pageNumber, int pageSize);
        Task<int> DeleteAsync(int sportEventId, int athleteId);
        Task<AchievementDto> UpdateAsync(AchievementDto updatedAchievement);
    }
}
