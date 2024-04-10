using SponsorSphere.Domain.Enums;
using SponsorSphere.Domain.Models;

namespace SponsorSphere.Infrastructure.Interfaces
{
    public interface ISponsorCompanyRepository
    {
        SponsorCompany Create(SponsorCompany user);
        SponsorCompany Update(int userId);
        bool Delete(int userId);
        List<SponsorCompany> GetAll();
        SponsorCompany GetById(int userId);
        List<SponsorCompany> GetByCountry(string country);
        int GetLastId();
    }
}
