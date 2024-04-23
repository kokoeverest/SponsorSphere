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

        public Task<int> Delete(Sponsorship sponsorship)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Sponsorship>> GetByAmountAsync(decimal amount)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Sponsorship>> GetByAthleteIdAsync(int athleteId)
        {
            throw new NotImplementedException();
        }

        public async Task<Sponsorship> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Sponsorship>> GetByLevelAsync(SponsorshipLevel level)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Sponsorship>> GetBySponsorIdAsync(int sponsorId)
        {
            throw new NotImplementedException();
        }

        public async Task<Sponsorship> Update(Sponsorship sponsorship)
        {
            _context.SaveChanges();
            throw new NotImplementedException();
        }
    }
}
