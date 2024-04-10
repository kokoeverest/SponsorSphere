using SponsorSphere.Domain.Models;
using SponsorSphere.Infrastructure.Interfaces;
using System.Diagnostics.Metrics;

namespace SponsorSphere.Infrastructure.Repositories
{
    public class SponsorCompanyRepository : ISponsorCompanyRepository
    {
        private readonly List<SponsorCompany> _sponsorCompanies = [];
        public SponsorCompany Create(SponsorCompany user)
        {
            _sponsorCompanies.Add(user);
            return user;
        }

        public bool Delete(int userId)
        {
            throw new NotImplementedException();
        }
        public SponsorCompany GetById(int userId)
        {
            for (int i = 0; i < _sponsorCompanies.Count; i++)
            {
                if (_sponsorCompanies[i].Id == userId)
                {
                    return _sponsorCompanies.ElementAt(i);
                }
            }
            throw new ApplicationException($"Sponsor with id {userId} not found");
        }

        public List<SponsorCompany> GetAll()
        {
            return _sponsorCompanies;
        }

        public List<SponsorCompany> GetByCountry(string country)
        {
            return _sponsorCompanies.Where(sponsorCompany => sponsorCompany.Country == country).ToList();
        }

        public List<SponsorCompany> GetByMostAthletes()
        {
            throw new NotImplementedException();
        }

        public List<SponsorCompany> GetByMoneyProvided()
        {
            throw new NotImplementedException();
        }

        public int GetLastId()
        {
            int firstId = 1;
            return _sponsorCompanies.Max(sponsorCompany => sponsorCompany.Id) + 1 ?? firstId;
        }

        public SponsorCompany Update(int userId)
        {
            throw new NotImplementedException();
        }
    }
}
