using SponsorSphere.Application.App.SponsorCompanies.Dtos;
using SponsorSphere.Domain.Enums;
using SponsorSphere.Domain.Models;

namespace SponsorSphere.Application.Interfaces
{
    public interface ISponsorCompanyRepository
    {
        Task<SponsorCompany> CreateAsync(SponsorCompany user);
        Task<List<SponsorCompany>> GetAllAsync(int pageNumber, int pageSize);
        Task<SponsorCompany> GetByIdAsync(int userId);
        Task<List<SponsorCompany>> GetByCountryAsync(CountryEnum country);
        Task<int> DeleteAsync(int userId);
        Task<SponsorCompanyDto> UpdateAsync(SponsorCompanyDto userId);
    }
}
