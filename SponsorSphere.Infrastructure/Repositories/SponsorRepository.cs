using Microsoft.EntityFrameworkCore;
using SponsorSphere.Application.Interfaces;
using SponsorSphere.Domain.Models;

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
                .OrderByDescending(sponsor => sponsor.Sponsorships.Count)
                .OrderBy(sponsor => sponsor.Name)
                .ToListAsync();

            return sponsors;
        }

        public async Task<List<Sponsor>> GetByMoneyProvidedAsync()
        {
            throw new NotImplementedException();
        }
    }
}
