using Microsoft.EntityFrameworkCore;
using SponsorSphere.Domain.Enums;
using SponsorSphere.Domain.Models;
using SponsorSphere.Infrastructure.Interfaces;

namespace SponsorSphere.Infrastructure.Repositories
{
    public class AthleteRepository : IAthleteRepository
    {
        private readonly List<Athlete> _athletes = [];

        public Athlete Create(Athlete athlete)
        {
            _athletes.Add(athlete);
            return athlete;
        }

        public bool Delete(int userId)
        {
            throw new NotImplementedException();
        }

        public List<Athlete> GetAll()
        {
            return _athletes;
        }

        public List<Athlete> GetByCountry(string country)
        {
            return _athletes.Where(athlete => athlete.Country == country).ToList();
        }

        public List<Athlete> GetByAge(int age)
        {
            return _athletes.Where(athlete => athlete.Age <= age).ToList();
        }

        public List<Athlete> GetBySport(SportsEnum sport)
        {
            return _athletes.Where(athlete => athlete.Sport == sport).ToList();
        }

        public List<Athlete> GetByUrgentNeed()
        {
            throw new NotImplementedException();
        }

        public List<Athlete> GetByAchievements()
        {
            throw new NotImplementedException();
        }

        public async Task<User?> GetById(int userId, SponsorSphereDbContext context)
        {
            return await context.Users.FirstOrDefaultAsync(athlete => athlete.Id == userId);
            //for (int i = 0; i < _athletes.Count; i++)
            //{
            //    if (_athletes[i].Id == userId)
            //    {
            //        return _athletes.ElementAt(i);
            //    }
            //}
            
            throw new ApplicationException($"Athlete with id {userId} not found");
        }

        public int GetLastId()
        {
            int firstId = 1;
            return _athletes.Max(athlete => athlete.Id) + 1 ?? firstId;
        }

        public Athlete Update(int userId)
        {
            throw new NotImplementedException();
        }
    }
}
