using SponsorSphere.Application.App.SponsorCompanies.Responses;
using SponsorSphere.Domain.Models;

namespace SponsorSphere.Application.Interfaces
{
    public interface ISponsorCompanyRepository
    {
        Task<SponsorCompany> CreateAsync(SponsorCompany user);
        Task<List<SponsorCompany>> GetAllAsync();
        Task<SponsorCompany?> GetByIdAsync(int userId);
        Task<List<SponsorCompany>> GetByCountryAsync(string country);
        Task<int> DeleteAsync(int userId);
        void Update(SponsorCompanyDto userId);
    }
}
