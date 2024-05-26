using SponsorSphere.Application.App.SportEvents.Dtos;
using SponsorSphere.Domain.Enums;
using SponsorSphere.Domain.Models;

namespace SponsorSphere.Application.Interfaces
{
    public interface ISportEventRepository
    {
        /// <summary>
        /// Asynchronously creates a new sport event.
        /// </summary>
        /// <param name="sportEvent">The sport event entity to create.</param>
        /// <returns>The created <see cref="SportEvent"/> entity.</returns>
        Task<SportEvent> CreateAsync(SportEvent sportEvent);

        /// <summary>
        /// Asynchronously retrieves a sport event by its ID.
        /// </summary>
        /// <param name="sportEventId">The ID of the sport event to retrieve.</param>
        /// <returns>The <see cref="SportEvent"/> entity if found; otherwise, null.</returns>
        Task<SportEvent> GetByIdAsync(int sportEventId);

        /// <summary>
        /// Asynchronously retrieves a paginated list of pending sport events.
        /// </summary>
        /// <param name="pageNumber">The page number to retrieve.</param>
        /// <param name="pageSize">The number of items per page.</param>
        /// <returns>A paginated list of pending <see cref="SportEvent"/> entities.</returns>
        Task<List<SportEvent>> GetPendingSportEventsAsync(int pageNumber, int pageSize);

        /// <summary>
        /// Asynchronously retrieves the count of pending sport events.
        /// </summary>
        /// <returns>The count of pending sport events.</returns>
        Task<int> GetPendingSportEventsCountAsync();

        /// <summary>
        /// Asynchronously retrieves a paginated list of finished sport events for a specific sport.
        /// </summary>
        /// <param name="sport">The <see cref="SportsEnum"/> to filter by.</param>
        /// <param name="pageNumber">The page number to retrieve.</param>
        /// <param name="pageSize">The number of items per page.</param>
        /// <returns>A paginated list of finished <see cref="SportEvent"/> entities.</returns>
        Task<List<SportEvent>> GetFinishedSportEventsAsync(SportsEnum sport, int pageNumber, int pageSize);

        /// <summary>
        /// Asynchronously retrieves a paginated list of unfinished sport events for a specific sport.
        /// </summary>
        /// <param name="sport">The <see cref="SportsEnum"/> to filter by.</param>
        /// <param name="pageNumber">The page number to retrieve.</param>
        /// <param name="pageSize">The number of items per page.</param>
        /// <returns>A paginated list of unfinished <see cref="SportEvent"/> entities.</returns>
        Task<List<SportEvent>> GetUnfinishedSportEventsAsync(SportsEnum sport, int pageNumber, int pageSize);

        /// <summary>
        /// Asynchronously searches for a specific sport event.
        /// </summary>
        /// <param name="sportEvent">The sport event entity to search for.</param>
        /// <returns>The <see cref="SportEvent"/> entity if found; otherwise, null.</returns>
        Task<SportEvent?> SearchAsync(SportEvent sportEvent);

        /// <summary>
        /// Asynchronously updates an existing sport event.
        /// </summary>
        /// <param name="sportEvent">The <see cref="SportEventDto"/> containing updated sport event information.</param>
        /// <returns>The updated <see cref="SportEventDto"/>.</returns>
        Task<SportEventDto> UpdateAsync(SportEventDto sportEvent);

        /// <summary>
        /// Asynchronously deletes a sport event by its ID.
        /// </summary>
        /// <param name="sportEventId">The ID of the sport event to delete.</param>
        /// <returns>The number of records deleted.</returns>
        Task<int> DeleteAsync(int sportEventId);
    }
}
