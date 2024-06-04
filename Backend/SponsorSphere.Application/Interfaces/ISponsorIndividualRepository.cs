using SponsorSphere.Application.App.SponsorIndividuals.Dtos;
using SponsorSphere.Domain.Enums;
using SponsorSphere.Domain.Models;

namespace SponsorSphere.Application.Interfaces
{
    public interface ISponsorIndividualRepository
    {
        /// <summary>
        /// Asynchronously creates a new sponsor individual.
        /// </summary>
        /// <param name="user">The sponsor individual entity to be created.</param>
        /// <returns>The created <see cref="SponsorIndividual"/> entity.</returns>
        Task<SponsorIndividual> CreateAsync(SponsorIndividual user);

        /// <summary>
        /// Asynchronously retrieves a paginated list of all sponsor individuals.
        /// </summary>
        /// <param name="pageNumber">The page number to retrieve.</param>
        /// <param name="pageSize">The number of items per page.</param>
        /// <returns>A paginated list of <see cref="SponsorIndividual"/> entities.</returns>
        Task<List<SponsorIndividual>> GetAllAsync(int pageNumber, int pageSize);

        Task<int> GetSponsorIndividualsCount();

        /// <summary>
        /// Asynchronously retrieves a sponsor individual by its unique identifier.
        /// </summary>
        /// <param name="userId">The unique identifier of the sponsor individual.</param>
        /// <returns>The <see cref="SponsorIndividual"/> entity if found, otherwise null.</returns>
        Task<SponsorIndividual> GetByIdAsync(int userId);

        /// <summary>
        /// Asynchronously retrieves a paginated list of sponsor individuals by country.
        /// </summary>
        /// <param name="country">The country to filter sponsor individuals by.</param>
        /// <param name="pageNumber">The page number to retrieve.</param>
        /// <param name="pageSize">The number of items per page.</param>
        /// <returns>A paginated list of <see cref="SponsorIndividual"/> entities filtered by country.</returns>
        Task<List<SponsorIndividual>> GetByCountryAsync(CountryEnum country, int pageNumber, int pageSize);

        /// <summary>
        /// Asynchronously retrieves a paginated list of sponsor individuals by age.
        /// </summary>
        /// <param name="age">The age to filter sponsor individuals by.</param>
        /// <param name="pageNumber">The page number to retrieve.</param>
        /// <param name="pageSize">The number of items per page.</param>
        /// <returns>A paginated list of <see cref="SponsorIndividual"/> entities filtered by age.</returns>
        Task<List<SponsorIndividual>> GetByAgeAsync(int age, int pageNumber, int pageSize);

        /// <summary>
        /// Asynchronously deletes a sponsor individual by its unique identifier.
        /// </summary>
        /// <param name="userId">The unique identifier of the sponsor individual to be deleted.</param>
        /// <returns>The number of state entries written to the database.</returns>
        Task<int> DeleteAsync(int userId);

        /// <summary>
        /// Asynchronously updates an existing sponsor individual.
        /// </summary>
        /// <param name="user">The updated sponsor individual DTO containing the new values.</param>
        /// <returns>The updated <see cref="SponsorIndividualDto"/>.</returns>
        Task<SponsorIndividualDto> UpdateAsync(SponsorIndividualDto user);
    }
}
