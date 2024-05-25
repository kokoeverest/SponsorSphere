using SponsorSphere.Application.App.Athletes.Dtos;
using SponsorSphere.Domain.Enums;
using SponsorSphere.Domain.Models;

namespace SponsorSphere.Application.Interfaces
{
    public interface IAthleteRepository
    {
        /// <summary>
        /// Asynchronously creates a new athlete.
        /// </summary>
        /// <param name="athlete">The athlete entity to be created.</param>
        /// <returns>The created <see cref="Athlete"/> entity.</returns>
        Task<Athlete> CreateAsync(Athlete athlete);

        /// <summary>
        /// Asynchronously deletes an athlete by their user ID.
        /// </summary>
        /// <param name="userId">The ID of the user to be deleted.</param>
        /// <returns>The number of state entries written to the database.</returns>
        Task<int> DeleteAsync(int userId);

        /// <summary>
        /// Asynchronously retrieves a paginated list of all athletes.
        /// </summary>
        /// <param name="pageNumber">The page number to retrieve.</param>
        /// <param name="pageSize">The number of athletes per page.</param>
        /// <returns>A list of <see cref="Athlete"/> entities.</returns>
        Task<List<Athlete>> GetAllAsync(int pageNumber, int pageSize);

        /// <summary>
        /// Asynchronously retrieves an athlete by their user ID.
        /// </summary>
        /// <param name="userId">The ID of the user to retrieve.</param>
        /// <returns>The <see cref="Athlete"/> entity.</returns>
        Task<Athlete> GetByIdAsync(int userId);

        /// <summary>
        /// Asynchronously retrieves a paginated list of athletes by country.
        /// </summary>
        /// <param name="country">The country to filter athletes by.</param>
        /// <param name="pageNumber">The page number to retrieve.</param>
        /// <param name="pageSize">The number of athletes per page.</param>
        /// <returns>A list of <see cref="Athlete"/> entities.</returns>
        Task<List<Athlete>> GetByCountryAsync(CountryEnum country, int pageNumber, int pageSize);

        /// <summary>
        /// Asynchronously retrieves a paginated list of athletes by age.
        /// </summary>
        /// <param name="age">The age to filter athletes by.</param>
        /// <param name="pageNumber">The page number to retrieve.</param>
        /// <param name="pageSize">The number of athletes per page.</param>
        /// <returns>A list of <see cref="Athlete"/> entities.</returns>
        Task<List<Athlete>> GetByAgeAsync(int age, int pageNumber, int pageSize);

        /// <summary>
        /// Asynchronously retrieves a paginated list of athletes by sport.
        /// </summary>
        /// <param name="sport">The sport to filter athletes by.</param>
        /// <param name="pageNumber">The page number to retrieve.</param>
        /// <param name="pageSize">The number of athletes per page.</param>
        /// <returns>A list of <see cref="Athlete"/> entities.</returns>
        Task<List<Athlete>> GetBySportAsync(SportsEnum sport, int pageNumber, int pageSize);

        /// <summary>
        /// Asynchronously retrieves a list of athletes with urgent needs.
        /// </summary>
        /// <returns>A list of <see cref="Athlete"/> entities.</returns>
        Task<List<Athlete>> GetByUrgentNeedAsync();

        /// <summary>
        /// Asynchronously retrieves a paginated list of athletes by achievements.
        /// </summary>
        /// <param name="pageNumber">The page number to retrieve.</param>
        /// <param name="pageSize">The number of athletes per page.</param>
        /// <returns>A list of <see cref="Athlete"/> entities.</returns>
        Task<List<Athlete>> GetByAchievementsAsync(int pageNumber, int pageSize);

        /// <summary>
        /// Asynchronously retrieves a paginated list of athletes by sponsorship amount.
        /// </summary>
        /// <param name="pageNumber">The page number to retrieve.</param>
        /// <param name="pageSize">The number of athletes per page.</param>
        /// <returns>A list of objects representing athletes and their sponsorship amounts.</returns>
        Task<List<object>> GetByAmountAsync(int pageNumber, int pageSize);

        /// <summary>
        /// Asynchronously updates an existing athlete.
        /// </summary>
        /// <param name="updatedAthlete">The updated athlete DTO containing the new values.</param>
        /// <returns>The updated <see cref="AthleteDto"/>.</returns>
        Task<AthleteDto> UpdateAsync(AthleteDto updatedAthlete);
    }
}
