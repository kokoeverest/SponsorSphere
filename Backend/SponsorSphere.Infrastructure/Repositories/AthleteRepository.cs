﻿using Microsoft.EntityFrameworkCore;
using SponsorSphere.Application.App.Athletes.Dtos;
using SponsorSphere.Application.Common.Exceptions;
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
            var athlete = await _context.Athletes
                .Include(ath => ath.BlogPosts)
                    .ThenInclude(bp => bp.Pictures)
                .Include(ath => ath.Sponsorships)
                .Include(ath => ath.Goals)
                .Include(ath => ath.Achievements)
                .FirstOrDefaultAsync(ath => ath.Id == athleteId)
                ?? throw new NotFoundException($"Athlete with id {athleteId} not found");

            return athlete;
        }

        public async Task<List<Athlete>> GetAllAsync(int pageNumber, int pageSize)
        {
            return await _context.Athletes
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .OrderBy(athlete => athlete.LastName)
                .ToListAsync();
        }

        public async Task<List<Athlete>> GetByCountryAsync(CountryEnum country, int pageNumber, int pageSize)
        {
            return await _context.Athletes
                .Where(athlete => athlete.Country == country)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .OrderBy(athlete => athlete.LastName)
                .ToListAsync();
        }

        public async Task<List<Athlete>> GetByAgeAsync(int age, int pageNumber, int pageSize)
        {
            var birthYearLimit = DateTime.UtcNow.Year - age;

            return await _context.Athletes
                .Where(athlete => birthYearLimit <= athlete.BirthDate.Year)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .OrderBy(athlete => athlete.LastName)
                .ToListAsync();
        }

        public async Task<List<Athlete>> GetBySportAsync(SportsEnum sport, int pageNumber, int pageSize)
        {
            return await _context.Athletes
                .Where(athlete => athlete.Sport == sport)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .OrderBy(athlete => athlete.LastName)
                .ToListAsync();
        }

        public async Task<List<object>> GetByAmountAsync(int pageNumber, int pageSize)
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
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .OrderByDescending(x => x.TotalAmount)
                .ToListAsync();

            return sponsorships.Cast<object>().ToList();
        }

        public async Task<List<Athlete>> GetByUrgentNeedAsync()
        {
            // to be modified
            return await _context.Athletes.ToListAsync();
        }

        public async Task<List<Athlete>> GetByAchievementsAsync(int pageNumber, int pageSize)
        {
            var result = await _context.Athletes
                .Join(_context.Achievements,
                ath => ath.Id,
                ach => ach.AthleteId,
                (athlete, achievement) => new { Athlete = athlete, Achievement = achievement })
                .Where(joinResult => joinResult.Achievement.PlaceFinished == 1)
                .Select(joinResult => joinResult.Athlete)
                .Include(a => a.Achievements)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .OrderBy(joinResult => joinResult.LastName)
                .ToListAsync();

            return result;
        }

        public async Task<AthleteDto> UpdateAsync(AthleteDto updatedAthlete)
        {
            await _context.Users.Where(u => u.Id == updatedAthlete.Id)
                .ExecuteUpdateAsync(setters => setters
                .SetProperty(ath => ath.Name, updatedAthlete.Name)
                .SetProperty(ath => ath.UserName, updatedAthlete.Email)
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

        public async Task<int> GetAthletesCount()
        {
            var result = await _context.Athletes.CountAsync();
            return result;
        }
    }
}
