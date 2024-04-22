using Microsoft.EntityFrameworkCore;
using SponsorSphere.Domain.Enums;
using SponsorSphere.Domain.Models;
using SponsorSphere.Infrastructure.Interfaces;
using System.Data;

namespace SponsorSphere.Infrastructure.Repositories
{
    public class AthleteRepository : IAthleteRepository
    {
        private readonly SponsorSphereDbContext _context;

        public AthleteRepository(SponsorSphereDbContext context)
        {
            _context = context;
        }

        public async Task<Athlete> CreateAsync(Athlete athlete)
        {
            try
            {
                _context.Athletes.Add(athlete);
                await _context.SaveChangesAsync();
                return athlete;
            }
            catch (DbUpdateException)
            {
                //await Console.Out.WriteLineAsync(e.GetType().ToString());
                throw new InvalidDataException("User with that email already exists");
            }
        }

        public async Task<int> DeleteAsync(int userId)
        {
            //return await _context.Users.Where(u => u.Id == userId).ExecuteDeleteAsync(); 
            // ExecuteDelete and ExecuteUpdate doesn't apply to TpT mapping strategy
            var athleteToDelete = await GetByIdAsync(userId);

            if (athleteToDelete is not null)
            {
                _context.Users.Remove(athleteToDelete);
                return await _context.SaveChangesAsync();
            }
            return 0;
        }
        public async Task<Athlete?> GetByIdAsync(int userId)
        {
            var athlete = await _context.Athletes.FirstOrDefaultAsync(athlete => athlete.Id == userId);

            if (athlete is not null)
            {
                return athlete;
            }
            throw new ApplicationException($"Athlete with id {userId} not found");
        }
        public async Task<List<Athlete>> GetAllAsync()
        {
            return await _context.Athletes.ToListAsync();
        }
        public async Task<List<Athlete>> GetAllAsync(int pageNumber, int pageSize)
        {
            return await _context.Athletes
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<List<Athlete>> GetByCountryAsync(string country)
        {
            return await _context.Athletes.Where(athlete => athlete.Country == country).ToListAsync();
        }

        public async Task<List<Athlete>> GetByAgeAsync(int age)
        {
            return await _context.Athletes.Where(athlete => athlete.Age <= age).ToListAsync();
        }

        public async Task<List<Athlete>> GetBySportAsync(SportsEnum sport)
        {
            return await _context.Athletes.Where(athlete => athlete.Sport == sport).ToListAsync();
        }

        public async Task<List<Athlete>> GetByUrgentNeedAsync()
        {
            // to be modified
            return await _context.Athletes.ToListAsync();
        }

        public async Task<List<Athlete>> GetByAchievementsAsync()
        {
            // to be modified
            return await _context.Athletes.ToListAsync();
        }

        public void Update(Athlete athleteToUpdate)
        {
            
            var result = _context.Athletes.Update(athleteToUpdate);
            Console.WriteLine(result.ToString());
        }
    }
}
