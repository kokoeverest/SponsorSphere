using Azure.Core;
using Microsoft.EntityFrameworkCore;
using SponsorSphere.Application.App.Athletes.Responses;
using SponsorSphere.Application.Interfaces;
using SponsorSphere.Domain.Enums;
using SponsorSphere.Domain.Models;
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
                await _context.Athletes.AddAsync(athlete);
                await _context.SaveChangesAsync();
                return athlete;
            }
            catch (DbUpdateException)
            {
                throw new InvalidDataException("User with that email already exists");
            }
        }

        public async Task<int> DeleteAsync(int athleteId)
        {
            await _context.Users
                .Where(user => user.Id.Equals(athleteId))
                .ExecuteUpdateAsync(setters => setters
                .SetProperty(ath => ath.IsDeleted, true)
                .SetProperty(ath => ath.DeletedOn, DateTime.UtcNow));
            
            return 1;
        }
        public async Task<Athlete> GetByIdAsync(int athleteId)
        {
            var athlete = await _context.Athletes.FirstOrDefaultAsync(athlete => athlete.Id == athleteId);

            return athlete is not null ? athlete : throw new ApplicationException($"Athlete with id {athleteId} not found");
        }
        public async Task<List<Athlete>> GetAllAsync()
        {
            return await _context.Athletes
                .OrderBy(athlete => athlete.Name)
                .ToListAsync();
        }
        public async Task<List<Athlete>> GetAllAsync(int pageNumber, int pageSize)
        {
            return await _context.Athletes
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .OrderBy(athlete => athlete.Name)
                .ToListAsync();
        }

        public async Task<List<Athlete>> GetByCountryAsync(CountryEnum country)
        {
            return await _context.Athletes
                .Where(athlete => athlete.Country == country)
                .OrderBy(athlete => athlete.Name)
                .ToListAsync();
        }

        public async Task<List<Athlete>> GetByAgeAsync(int age)
        {
            return await _context.Athletes
                .Where(athlete => athlete.Age <= age)
                .OrderBy(athlete => athlete.Name)
                .ToListAsync();
        }

        public async Task<List<Athlete>> GetBySportAsync(SportsEnum sport)
        {
            return await _context.Athletes
                .Where(athlete => athlete.Sport == sport)
                .OrderBy(athlete => athlete.Name)
                .ToListAsync();
        }

        public async Task<List<object>> GetByAmountAsync()
        {
            var sponsorships = await _context.Athletes
                .Join(_context.Sponsorships,
                      athlete => athlete.Id,
                      sponsorship => sponsorship.AthleteId,
                      (athlete, sponsorship) => new { Athlete = athlete, Sponsorship = sponsorship })
                .GroupBy(x => x.Athlete.Id,
                         x => x.Sponsorship.Amount,
                        (athleteId, amounts) => new
                        {
                            AthleteId = athleteId,
                            TotalAmount = amounts.Sum()
                        })
                .ToListAsync();
            return sponsorships.Cast<object>().ToList();
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

        public async Task<AthleteDto> UpdateAsync(AthleteDto updatedAthlete)
        {
            await _context.Users.Where(u => u.Id == updatedAthlete.Id)
                .ExecuteUpdateAsync(setters => setters
                .SetProperty(ath => ath.Name, updatedAthlete.Name)
                .SetProperty(ath => ath.Email, updatedAthlete.Email)
                .SetProperty(ath => ath.Country, updatedAthlete.Country)
                .SetProperty(ath => ath.PhoneNumber, updatedAthlete.PhoneNumber)
                .SetProperty(ath => ath.PictureId, updatedAthlete.PictureId)
                .SetProperty(ath => ath.Website, updatedAthlete.Website)
                .SetProperty(ath => ath.FaceBookLink, updatedAthlete.FaceBookLink)
                .SetProperty(ath => ath.InstagramLink, updatedAthlete.InstagramLink)
                .SetProperty(ath => ath.TwitterLink, updatedAthlete.TwitterLink)
                .SetProperty(ath => ath.StravaLink, updatedAthlete.StravaLink)
                );

            await _context.Athletes.Where(ath => ath.Id == updatedAthlete.Id)
                .ExecuteUpdateAsync(setters => setters
                .SetProperty(ath => ath.LastName, updatedAthlete.LastName)
                .SetProperty(ath => ath.BirthDate, updatedAthlete.BirthDate)
                .SetProperty(ath => ath.Sport, updatedAthlete.Sport)
            );

            return updatedAthlete;
        }
    }
}
