using SponsorSphere.Application.App.Goals.Dtos;
using SponsorSphere.Domain.Models;

namespace SponsorSphere.Application.Interfaces
{
    public interface IGoalRepository
    {
        /// <summary>
        /// Asynchronously creates a new goal.
        /// </summary>
        /// <param name="goal">The goal entity to be created.</param>
        /// <returns>The created <see cref="Goal"/> entity.</returns>
        Task<Goal> CreateAsync(Goal goal);

        /// <summary>
        /// Asynchronously retrieves a paginated list of all goals for a specific athlete.
        /// </summary>
        /// <param name="athleteId">The ID of the athlete to retrieve goals for.</param>
        /// <param name="pageNumber">The page number to retrieve.</param>
        /// <param name="pageSize">The number of goals per page.</param>
        /// <returns>A list of <see cref="Goal"/> entities.</returns>
        Task<List<Goal>> GetAllAsync(int athleteId, int pageNumber, int pageSize);

        /// <summary>
        /// Asynchronously deletes a goal by the sport event ID and athlete ID.
        /// </summary>
        /// <param name="sportEventId">The ID of the sport event to delete the goal for.</param>
        /// <param name="athleteId">The ID of the athlete to delete the goal for.</param>
        /// <returns>The number of state entries written to the database.</returns>
        Task<int> DeleteAsync(int sportEventId, int athleteId);

        /// <summary>
        /// Asynchronously updates an existing goal.
        /// </summary>
        /// <param name="goal">The updated goal DTO containing the new values.</param>
        /// <returns>The updated <see cref="GoalDto"/>.</returns>
        Task<GoalDto> UpdateAsync(GoalDto goal);
    }
}
