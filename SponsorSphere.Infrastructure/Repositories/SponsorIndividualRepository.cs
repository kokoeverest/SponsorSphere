using Microsoft.EntityFrameworkCore;
using SponsorSphere.Application.Interfaces;
using SponsorSphere.Domain.Models;

namespace SponsorSphere.Infrastructure.Repositories
{
    public class SponsorIndividualRepository : ISponsorIndividualRepository
    {
        private readonly SponsorSphereDbContext _context;

        public SponsorIndividualRepository(SponsorSphereDbContext context)
        {
            _context = context;
        }
        public async Task<SponsorIndividual> CreateAsync(SponsorIndividual sponsorIndividual)
        {
            try
            {
                await _context.SponsorIndividuals.AddAsync(sponsorIndividual);
                await _context.SaveChangesAsync();
                return sponsorIndividual;
            }
            catch (DbUpdateException)
            {
                throw new InvalidDataException("User with that email already exists");
            }
        }

        public async Task<int> DeleteAsync(int userId)
        {
            var sponsorIndividualToDelete = await GetByIdAsync(userId);

            if (sponsorIndividualToDelete is not null)
            {
                _context.Users.Remove(sponsorIndividualToDelete);
                return await _context.SaveChangesAsync();
            }
            return 0;
        }

        public async Task<SponsorIndividual?> GetByIdAsync(int userId)
        {
            var sponsorIndividual = await _context.SponsorIndividuals.FirstOrDefaultAsync(si => si.Id == userId);

            if (sponsorIndividual is not null)
            {
                return sponsorIndividual;
            }
            throw new ApplicationException($"Sponsor with id {userId} not found");
        }

        public async Task<List<SponsorIndividual>> GetAllAsync()
        {
            return await _context.SponsorIndividuals
                .OrderBy(sponsor => sponsor.Name)
                .ToListAsync();
        }

        public async Task<List<SponsorIndividual>> GetByAgeAsync(int age)
        {
            return await _context.SponsorIndividuals
                .Where(sponsorIndividual => sponsorIndividual.Age <= age)
                .OrderBy(sponsor => sponsor.Name)
                .ToListAsync();
        }

        public async Task<List<SponsorIndividual>> GetByCountryAsync(string country)
        {
            return await _context.SponsorIndividuals
                .Where(sponsorIndividual => sponsorIndividual.Country == country)
                .OrderBy(sponsor => sponsor.Name)
                .ToListAsync();
        }
        public async void Update(int userId)
        {
            var sponsorIndividualToUpdate = await GetByIdAsync(userId);
            var result = _context.SponsorIndividuals.Update(sponsorIndividualToUpdate);
            _context.SaveChanges();
            Console.WriteLine(result.ToString());
        }
    }
}
