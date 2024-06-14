using Microsoft.EntityFrameworkCore;
using SponsorSphere.Application.Common.Exceptions;
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


        public async Task<Sponsor> GetByIdAsync(int userId)
        {
            var sponsor = await _context.Sponsors
                .Include(sc => sc.BlogPosts)
                    .ThenInclude(bp => bp.Pictures)
                .Include(sc => sc.Sponsorships)
                .FirstOrDefaultAsync(sc => sc.Id == userId)
                    ?? throw new NotFoundException($"Sponsor with id {userId} not found");

            return sponsor;
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
