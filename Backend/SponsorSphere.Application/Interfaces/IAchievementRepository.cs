using SponsorSphere.Application.App.Achievements.Dtos;
using SponsorSphere.Domain.Models;

namespace SponsorSphere.Application.Interfaces
{
    public interface IAchievementRepository
    {
        /// <summary>
        /// Asynchronously creates a new achievement in the database.
        /// </summary>
        /// <param name="achievement">The achievement entity to be created.</param>
        /// <returns>The created <see cref="Achievement"/> entity.</returns>
        Task<Achievement> CreateAsync(Achievement achievement);

        /// <summary>
        /// Asynchronously retrieves a paginated list of achievements for a specific athlete.
        /// </summary>
        /// <param name="athleteId">The ID of the athlete.</param>
        /// <param name="pageNumber">The page number to retrieve.</param>
        /// <param name="pageSize">The number of achievements per page.</param>
        /// <returns>A list of <see cref="Achievement"/> entities.</returns>
        Task<List<Achievement>> GetAllAsync(int athleteId, int pageNumber, int pageSize);

        /// <summary>
        /// Asynchronously deletes an achievement based on the athlete ID and sport event ID.
        /// </summary>
        /// <param name="sportEventId">The ID of the sport event.</param>
        /// <param name="athleteId">The ID of the athlete.</param>
        /// <returns>The number of state entries written to the database.</returns>
        Task<int> DeleteAsync(int sportEventId, int athleteId);

        /// <summary>
        /// Asynchronously updates an existing achievement.
        /// </summary>
        /// <param name="updatedAchievement">The updated achievement DTO containing the new values.</param>
        /// <returns>The updated <see cref="AchievementDto"/>.</returns>
        Task<AchievementDto> UpdateAsync(AchievementDto updatedAchievement);
    }
}
