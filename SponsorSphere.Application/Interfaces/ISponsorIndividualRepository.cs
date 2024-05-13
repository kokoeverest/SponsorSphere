using SponsorSphere.Application.App.SponsorIndividuals.Dtos;
using SponsorSphere.Domain.Enums;
using SponsorSphere.Domain.Models;

namespace SponsorSphere.Application.Interfaces
{
    public interface ISponsorIndividualRepository
    {
        Task<SponsorIndividual> CreateAsync(SponsorIndividual user);
        Task<List<SponsorIndividual>> GetAllAsync(int pageNumber, int pageSize);
        Task<SponsorIndividual> GetByIdAsync(int userId);
        Task<List<SponsorIndividual>> GetByCountryAsync(CountryEnum country, int pageNumber, int pageSize);
        Task<List<SponsorIndividual>> GetByAgeAsync(int age, int pageNumber, int pageSize);
        Task<int> DeleteAsync(int userId);
        Task<SponsorIndividualDto> UpdateAsync(SponsorIndividualDto userId);

    }
}
