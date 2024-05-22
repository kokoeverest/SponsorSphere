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
        public async Task<List<object>> GetByMostAthletesAsync(int pageNumber, int pageSize)
        {
            var sponsors = await _context.Sponsors
                .Join(_context.Sponsorships,
                      sponsor => sponsor.Id,
                      sponsorship => sponsorship.SponsorId,
                      (sponsor, sponsorship) => new { Sponsor = sponsor, Sponsorship = sponsorship })
                .GroupBy(x => x.Sponsor.Id,
                         x => x.Sponsorship.Amount,
                        (sponsorId, amounts) => new
                        {
                            SponsorId = sponsorId,
                            TotalAmount = amounts.Count()
                        })
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .OrderByDescending(x => x.TotalAmount)
                .ToListAsync();

            return sponsors.Cast<object>().ToList();
        }

        public async Task<List<object>> GetByMoneyProvidedAsync(int pageNumber, int pageSize)
        {
            var sponsorships = await _context.Sponsors
                .Join(_context.Sponsorships,
                      sponsor => sponsor.Id,
                      sponsorship => sponsorship.SponsorId,
                      (sponsor, sponsorship) => new { Sponsor = sponsor, Sponsorship = sponsorship })
                .GroupBy(x => x.Sponsor.Id,
                         x => x.Sponsorship.Amount,
                        (sponsorId, amounts) => new
                        {
                            SponsorId = sponsorId,
                            TotalAmount = amounts.Sum()
                        })
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .OrderByDescending(x => x.TotalAmount)
                .ToListAsync();

            return sponsorships.Cast<object>().ToList();
        }
    }
}
