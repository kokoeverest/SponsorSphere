using Microsoft.EntityFrameworkCore;
using SponsorSphere.Application.App.SportEvents.Responses;
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

        public async Task<int> DeleteAsync(int sportEventId)
        {
            return await _context.SportEvents.Where(se => se.Id == sportEventId).ExecuteDeleteAsync();
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
                            .FirstOrDefaultAsync(
                                se => se.Name == sportEvent.Name &&
                                    se.Sport == sportEvent.Sport &&
                                    se.EventDate == sportEvent.EventDate &&
                                    se.Country == sportEvent.Country);
        }

        public async void Update(SportEventDto sportEvent)
        {
            var sportEventToUpdate = await GetByIdAsync(sportEvent.Id);
            _context.SportEvents.Update(sportEventToUpdate);

            await _context.SaveChangesAsync();
        }
    }
}
