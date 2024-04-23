using Microsoft.EntityFrameworkCore;
using SponsorSphere.Application.Interfaces;
using SponsorSphere.Domain.Models;
using System.Diagnostics.Metrics;

namespace SponsorSphere.Infrastructure.Repositories
{
    public class SponsorCompanyRepository : ISponsorCompanyRepository
    {
        private readonly SponsorSphereDbContext _context;

        public SponsorCompanyRepository(SponsorSphereDbContext context)
        {
            _context = context;
        }

        public async Task<SponsorCompany> CreateAsync(SponsorCompany sponsorCompany)
        {
            try
            {
                await _context.SponsorCompanies.AddAsync(sponsorCompany);
                await _context.SaveChangesAsync();
                return sponsorCompany;
            }
            catch (DbUpdateException)
            {
                throw new InvalidDataException("User with that email already exists");
            }
        }

        public async Task<int> DeleteAsync(int userId)
        {
            var sponsorCompanyToDelete = await GetByIdAsync(userId);

            if (sponsorCompanyToDelete is not null)
            {
                _context.Users.Remove(sponsorCompanyToDelete);
                return await _context.SaveChangesAsync();
            }
            return 0;
        }
        public async Task<SponsorCompany?> GetByIdAsync(int userId)
        {
            var sponsorCompany = await _context.SponsorCompanies.FirstOrDefaultAsync(sc => sc.Id == userId);

            if (sponsorCompany is not null)
            {
                return sponsorCompany;
            }
            throw new ApplicationException($"Sponsor with id {userId} not found");
        }

        public async Task<List<SponsorCompany>> GetAllAsync()
        {
            return await _context.SponsorCompanies
                .OrderBy(sponsor => sponsor.Name)
                .ToListAsync();
        }

        public async Task<List<SponsorCompany>> GetByCountryAsync(string country)
        {
            return await _context.SponsorCompanies
                .Where(sponsorCompany => sponsorCompany.Country == country)
                .OrderBy(sponsor => sponsor.Name)
                .ToListAsync();
        }
        public async void Update(int userId)
        {
            var sponsorCompanyToUpdate = await GetByIdAsync(userId);
            var result = _context.SponsorCompanies.Update(sponsorCompanyToUpdate);
            _context.SaveChanges();
            Console.WriteLine(result.ToString());
        }
    }
}
