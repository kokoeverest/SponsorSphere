using SponsorSphere.Application.App.SponsorCompanies.Dtos;
using SponsorSphere.Domain.Enums;
using SponsorSphere.Domain.Models;

namespace SponsorSphere.Application.Interfaces
{
    public interface ISponsorCompanyRepository
    {
        /// <summary>
        /// Asynchronously creates a new sponsor company.
        /// </summary>
        /// <param name="user">The sponsor company entity to be created.</param>
        /// <returns>The created <see cref="SponsorCompany"/> entity.</returns>
        Task<SponsorCompany> CreateAsync(SponsorCompany user);

        /// <summary>
        /// Asynchronously retrieves a paginated list of all sponsor companies.
        /// </summary>
        /// <param name="pageNumber">The page number to retrieve.</param>
        /// <param name="pageSize">The number of items per page.</param>
        /// <returns>A paginated list of <see cref="SponsorCompany"/> entities.</returns>
        Task<List<SponsorCompany>> GetAllAsync(int pageNumber, int pageSize);

        Task<int> GetSponsorCompaniesCount();

        /// <summary>
        /// Asynchronously retrieves a sponsor company by its unique identifier.
        /// </summary>
        /// <param name="userId">The unique identifier of the sponsor company.</param>
        /// <returns>The <see cref="SponsorCompany"/> entity if found, otherwise null.</returns>
        Task<SponsorCompany> GetByIdAsync(int userId);

        /// <summary>
        /// Asynchronously retrieves a paginated list of sponsor companies by country.
        /// </summary>
        /// <param name="country">The country to filter sponsor companies by.</param>
        /// <param name="pageNumber">The page number to retrieve.</param>
        /// <param name="pageSize">The number of items per page.</param>
        /// <returns>A paginated list of <see cref="SponsorCompany"/> entities filtered by country.</returns>
        Task<List<SponsorCompany>> GetByCountryAsync(CountryEnum country, int pageNumber, int pageSize);

        /// <summary>
        /// Asynchronously deletes a sponsor company by its unique identifier.
        /// </summary>
        /// <param name="userId">The unique identifier of the sponsor company to be deleted.</param>
        /// <returns>The number of state entries written to the database.</returns>
        Task<int> DeleteAsync(int userId);

        /// <summary>
        /// Asynchronously updates an existing sponsor company.
        /// </summary>
        /// <param name="user">The updated sponsor company DTO containing the new values.</param>
        /// <returns>The updated <see cref="SponsorCompanyDto"/>.</returns>
        Task<SponsorCompanyDto> UpdateAsync(SponsorCompanyDto user);
    }
}
