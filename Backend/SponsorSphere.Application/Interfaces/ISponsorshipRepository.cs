using SponsorSphere.Application.App.Sponsorships.Dtos;
using SponsorSphere.Domain.Enums;
using SponsorSphere.Domain.Models;

namespace SponsorSphere.Application.Interfaces
{
    public interface ISponsorshipRepository
    {
        /// <summary>
        /// Asynchronously creates a new sponsorship.
        /// </summary>
        /// <param name="sponsorship">The sponsorship entity to create.</param>
        /// <returns>The created <see cref="Sponsorship"/> entity.</returns>
        Task<Sponsorship> CreateAsync(Sponsorship sponsorship);

        /// <summary>
        /// Asynchronously updates an existing sponsorship.
        /// </summary>
        /// <param name="sponsorship">The <see cref="SponsorshipDto"/> containing updated sponsorship information.</param>
        /// <returns>The updated <see cref="SponsorshipDto"/>.</returns>
        Task<SponsorshipDto> UpdateAsync(SponsorshipDto sponsorship);

        /// <summary>
        /// Asynchronously deletes a sponsorship based on athlete and sponsor IDs.
        /// </summary>
        /// <param name="athleteId">The ID of the athlete associated with the sponsorship.</param>
        /// <param name="sponsorId">The ID of the sponsor associated with the sponsorship.</param>
        /// <returns>The number of records deleted.</returns>
        Task<int> DeleteAsync(int athleteId, int sponsorId);

        /// <summary>
        /// Asynchronously retrieves a specific sponsorship based on athlete and sponsor IDs.
        /// </summary>
        /// <param name="athleteId">The ID of the athlete associated with the sponsorship.</param>
        /// <param name="sponsorId">The ID of the sponsor associated with the sponsorship.</param>
        /// <returns>The <see cref="Sponsorship"/> entity if found; otherwise, null.</returns>
        Task<Sponsorship?> GetSponsorshipAsync(int athleteId, int sponsorId);

        /// <summary>
        /// Asynchronously retrieves a paginated list of sponsorships for a specific athlete.
        /// </summary>
        /// <param name="athleteId">The ID of the athlete whose sponsorships to retrieve.</param>
        /// <param name="pageNumber">The page number to retrieve.</param>
        /// <param name="pageSize">The number of items per page.</param>
        /// <returns>A paginated list of <see cref="Sponsorship"/> entities.</returns>
        Task<List<Sponsorship>> GetByAthleteIdAsync(int athleteId, int pageNumber, int pageSize);

        /// <summary>
        /// Asynchronously retrieves a paginated list of sponsorships for a specific sponsor.
        /// </summary>
        /// <param name="sponsorId">The ID of the sponsor whose sponsorships to retrieve.</param>
        /// <param name="pageNumber">The page number to retrieve.</param>
        /// <param name="pageSize">The number of items per page.</param>
        /// <returns>A paginated list of <see cref="Sponsorship"/> entities.</returns>
        Task<List<Sponsorship>> GetBySponsorIdAsync(int sponsorId, int pageNumber, int pageSize);

        /// <summary>
        /// Asynchronously retrieves a paginated list of sponsorships by their level.
        /// </summary>
        /// <param name="level">The <see cref="SponsorshipLevel"/> to filter by.</param>
        /// <param name="pageNumber">The page number to retrieve.</param>
        /// <param name="pageSize">The number of items per page.</param>
        /// <returns>A paginated list of <see cref="Sponsorship"/> entities.</returns>
        Task<List<Sponsorship>> GetByLevelAsync(SponsorshipLevel level, int pageNumber, int pageSize);
    }
}
