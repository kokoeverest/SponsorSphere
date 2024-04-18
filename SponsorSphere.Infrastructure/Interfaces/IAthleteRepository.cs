using SponsorSphere.Domain.Enums;
using SponsorSphere.Domain.Models;

namespace SponsorSphere.Infrastructure.Interfaces
{
    public interface IAthleteRepository
    {
        Athlete Create(Athlete user);
        Athlete Update(int userId);
        bool Delete(int userId);
        List<Athlete> GetAll();
        Task<User?> GetById(int userId, SponsorSphereDbContext context);
        List<Athlete> GetByCountry(string country);
        int GetLastId();
        public List<Athlete> GetByAge(int age);
        public List<Athlete> GetBySport(SportsEnum sport);
        public List<Athlete> GetByUrgentNeed();
        public List<Athlete> GetByAchievements();

    }
}
