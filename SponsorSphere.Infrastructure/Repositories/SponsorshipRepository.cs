using Microsoft.EntityFrameworkCore;
using SponsorSphere.Application.App.Sponsorships.Dtos;
using SponsorSphere.Application.Common.Exceptions;
using SponsorSphere.Application.Interfaces;
using SponsorSphere.Domain.Enums;
using SponsorSphere.Domain.Models;

namespace SponsorSphere.Infrastructure.Repositories
{
    public class SponsorshipRepository : ISponsorshipRepository
    {
        private readonly SponsorSphereDbContext _context;

        public SponsorshipRepository(SponsorSphereDbContext context)
        {
            _context = context;
        }

        public async Task<Sponsorship> CreateAsync(Sponsorship sponsorship)
        {
            await _context.Sponsorships.AddAsync(sponsorship);
            await _context.SaveChangesAsync();
            return sponsorship;
        }

        public async Task<int> DeleteAsync(int athleteId, int sponsorId)
        {
            return await _context.Sponsorships
                .Where(sh => sh.AthleteId == athleteId && 
                             sh.SponsorId == sponsorId)
                .ExecuteDeleteAsync();
        }

        public async Task<Sponsorship?> GetSponsorshipAsync(int athleteId, int sponsorId)
        {
            var sponsorship = await _context.Sponsorships
                .FirstOrDefaultAsync(sh => sh.AthleteId == athleteId &&
                             sh.SponsorId == sponsorId);

            if (sponsorship is null)
            {
                throw new NotFoundException("Sponsorship not found");
            }
            return sponsorship;
        }

        public async Task<List<Sponsorship>> GetByAthleteIdAsync(int athleteId)
        {
            return await _context.Sponsorships.Where(sh => sh.AthleteId == athleteId).ToListAsync();
        }

        public async Task<List<Sponsorship>> GetByLevelAsync(SponsorshipLevel level)
        {
            return await _context.Sponsorships.Where(sh => sh.Level == level).ToListAsync();
        }

        public async Task<List<Sponsorship>> GetBySponsorIdAsync(int sponsorId)
        {
            return await _context.Sponsorships.Where(sh => sh.SponsorId == sponsorId).ToListAsync();
        }

        public async Task<SponsorshipDto> UpdateAsync(SponsorshipDto sponsorship)
        {
            await _context.Sponsorships
                .Where(sh => sh.AthleteId == sponsorship.AthleteId &&
                             sh.SponsorId == sponsorship.SponsorId)
                .ExecuteUpdateAsync(setters => setters
                .SetProperty(sh => sh.Amount, sponsorship.Amount)
                .SetProperty(sh => sh.Level, sponsorship.Level)
            );
            return sponsorship;
        }
    }
}
