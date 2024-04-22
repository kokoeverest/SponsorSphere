using Microsoft.EntityFrameworkCore;
using SponsorSphere.Domain.Models;
using SponsorSphere.Infrastructure.Interfaces;

namespace SponsorSphere.Infrastructure.Repositories
{
    public class SponsorRepository : ISponsorRepository
    {
        private readonly SponsorSphereDbContext _context;

        public SponsorRepository(SponsorSphereDbContext context)
        {
            _context = context;
        }
        public async Task<List<Sponsor>> GetByMostAthletesAsync()
        {
            var sponsors = await _context.Sponsors
                .OrderByDescending(s => s.Sponsorships.Count)
                .ToListAsync();

            return sponsors;
        }

        public async Task<List<Sponsor>> GetByMoneyProvidedAsync()
        {
            throw new NotImplementedException();
        }
    }
}
