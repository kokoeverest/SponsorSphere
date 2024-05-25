namespace SponsorSphere.Application.Interfaces
{
    public interface ISponsorRepository
    {
        /// <summary>
        /// Asynchronously retrieves a paginated list of sponsors ordered by the amount of money provided.
        /// </summary>
        /// <param name="pageNumber">The page number to retrieve.</param>
        /// <param name="pageSize">The number of items per page.</param>
        /// <returns>A paginated list of objects representing sponsors ordered by the amount of money provided.</returns>
        Task<List<object>> GetByMoneyProvidedAsync(int pageNumber, int pageSize);

        /// <summary>
        /// Asynchronously retrieves a paginated list of sponsors ordered by the number of athletes they sponsor.
        /// </summary>
        /// <param name="pageNumber">The page number to retrieve.</param>
        /// <param name="pageSize">The number of items per page.</param>
        /// <returns>A paginated list of objects representing sponsors ordered by the number of athletes they sponsor.</returns>
        Task<List<object>> GetByMostAthletesAsync(int pageNumber, int pageSize);
    }
}
