using Microsoft.EntityFrameworkCore;
using SponsorSphere.Application.Interfaces;
using SponsorSphere.Domain.Models;

namespace SponsorSphere.Infrastructure.Repositories
{
    public class SportEventRepository : ISportEventRepository
    {
        private readonly SponsorSphereDbContext _context;

        public SportEventRepository(SponsorSphereDbContext context)
        {
            _context = context;
        }

        public async Task<SportEvent> CreateAsync(SportEvent sportEvent)
        {
            var existingSportEvent = await SearchAsync(sportEvent);

            if (existingSportEvent is not null)
            {
                return existingSportEvent;
            }

            await _context.SportEvents.AddAsync(sportEvent);
            await _context.SaveChangesAsync();
            return sportEvent;
        }

        public async Task<int> DeleteAsync(SportEvent sportEvent)
        {
            return await _context.SportEvents.Where(se => se.Id == sportEvent.Id).ExecuteDeleteAsync();
        }

        public async Task<SportEvent?> GetByIdAsync(int sportEventId)
        {
            var sportEvent = await _context.SportEvents.FirstOrDefaultAsync(se => se.Id == sportEventId);

            if (sportEvent is not null)
            {
                return sportEvent;
            }
            throw new ApplicationException($"SportEvent with id {sportEventId} not found");
        }

        public async Task<SportEvent?> SearchAsync(SportEvent sportEvent)
        {
            return await _context.SportEvents
                            .FirstOrDefaultAsync(se => se.Name == sportEvent.Name &&
                                   se.Sport == sportEvent.Sport &&
                                   se.EventDate == sportEvent.EventDate &&
                                   se.Country == sportEvent.Country);
        }

        public void Update(SportEvent sportEventToUpdate)
        {
            var result = _context.SportEvents.Update(sportEventToUpdate);
            _context.SaveChanges();
            Console.WriteLine(result.ToString());
        }
    }
}
