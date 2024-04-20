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

        public async Task<Athlete> AddAsync(Athlete athlete)
        {
            try
            {
                _context.Athletes.Add(athlete);
                await _context.SaveChangesAsync();
                return athlete;
            }
            catch (DbUpdateException e)
            {
                await Console.Out.WriteLineAsync(e.GetType().ToString());
                throw new InvalidDataException("User with that email already exists");
            }
        }

        public async Task DeleteAsync(int userId)
        {
            var athleteToDelete = await GetByIdAsync(userId);
            //var result = await _context.Users.ExecuteDeleteAsync(athleteToDelete);

            if (athleteToDelete is not null)
            {
                _context.Users.Remove(athleteToDelete);
                await _context.SaveChangesAsync();
            }
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


        public async Task<Athlete?> UpdateAsync(int userId)
        {
            // to be modified
            var athlete = await GetByIdAsync(userId);
            if (athlete is not null)
            {
                return athlete;
            }
            return null;
        }

        public int GetLastIdAsync()
        {
            // to be modified
            return 1;
        }
    }
}
