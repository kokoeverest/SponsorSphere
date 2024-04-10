using SponsorSphere.Domain.Models;
using SponsorSphere.Infrastructure.Interfaces;

namespace SponsorSphere.Infrastructure.Repositories
{
    public class SponsorIndividualRepository : ISponsorIndividualRepository
    {
        private readonly List<SponsorIndividual> _sponsorIndividuals = [];
        public SponsorIndividual Create(SponsorIndividual user)
        {
            _sponsorIndividuals.Add(user);
            return user;
        }

        public bool Delete(int userId)
        {
            throw new NotImplementedException();
        }
       
        public SponsorIndividual GetById(int userId)
        {
            for (int i = 0; i < _sponsorIndividuals.Count; i++)
            {
                if (_sponsorIndividuals[i].Id == userId)
                {
                    return _sponsorIndividuals.ElementAt(i);
                }
            }
            throw new ApplicationException($"User with id {userId} not found");
        }

        public List<SponsorIndividual> GetAll()
        {
            return _sponsorIndividuals;
        }

        public List<SponsorIndividual> GetByAge(int age)
        {
            return _sponsorIndividuals.Where(sponsorIndividual => sponsorIndividual.Age <= age).ToList();
        }

        public List<SponsorIndividual> GetByCountry(string country)
        {
            return _sponsorIndividuals.Where(sponsorIndividual => sponsorIndividual.Country == country).ToList();
        }

        public List<SponsorIndividual> GetByMostAthletes()
        {
            throw new NotImplementedException();
        }

        public List<SponsorIndividual> GetByMoneyProvided()
        {
            throw new NotImplementedException();
        }

        public int GetLastId()
        {
            int firstId = 1;
            return _sponsorIndividuals.Max(sponsorIndividual => sponsorIndividual.Id) + 1 ?? firstId;
        }

        public SponsorIndividual Update(int userId)
        {
            throw new NotImplementedException();
        }
    }
}
