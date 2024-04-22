using SponsorSphere.Domain.Models;

namespace SponsorSphere.Infrastructure.Interfaces
{
    public interface ISponsorIndividualRepository
    {
        Task<SponsorIndividual> CreateAsync(SponsorIndividual user);
        Task<List<SponsorIndividual>> GetAllAsync();
        Task<SponsorIndividual> GetByIdAsync(int userId);
        Task<List<SponsorIndividual>> GetByCountryAsync(string country);
        Task<List<SponsorIndividual>> GetByAgeAsync(int age);
        Task<int> DeleteAsync(int userId);
        void Update(SponsorIndividual userId);

    }
}
