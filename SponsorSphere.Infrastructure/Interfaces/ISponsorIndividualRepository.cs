using SponsorSphere.Domain.Enums;
using SponsorSphere.Domain.Models;

namespace SponsorSphere.Infrastructure.Interfaces
{
    public interface ISponsorIndividualRepository
    {
        SponsorIndividual Create(SponsorIndividual user);
        SponsorIndividual Update(int userId);
        bool Delete(int userId);
        List<SponsorIndividual> GetAll();
        SponsorIndividual GetById(int userId);
        List<SponsorIndividual> GetByCountry(string country);
        int GetLastId();
        public List<SponsorIndividual> GetByAge(int age);

    }
}
