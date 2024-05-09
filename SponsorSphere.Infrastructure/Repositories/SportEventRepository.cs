using Microsoft.EntityFrameworkCore;
using SponsorSphere.Application.App.SportEvents.Responses;
using SponsorSphere.Application.Common.Exceptions;
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
            return await _context.SportEvents
                .Where(se => se.Id == sportEventId)
                .ExecuteDeleteAsync();
        }

        public async Task<SportEvent> GetByIdAsync(int sportEventId)
        {
            var sportEvent = await _context.SportEvents.FirstOrDefaultAsync(se => se.Id == sportEventId);

            if (sportEvent is not null)
            {
                return sportEvent;
            }
            throw new NotFoundException($"SportEvent with id {sportEventId} not found");
        }

        public async Task<SportEvent?> SearchAsync(SportEvent sportEvent)
        {
            var existing = await _context.SportEvents
                .FirstOrDefaultAsync(se => se.Equals(sportEvent));

            return await _context.SportEvents
                .FirstOrDefaultAsync(se => se.Name == sportEvent.Name &&
                        se.Sport == sportEvent.Sport &&
                        se.EventDate == sportEvent.EventDate &&
                        se.Country == sportEvent.Country);
        }

        public async Task<SportEventDto> UpdateAsync(SportEventDto sportEvent)
        {
            await _context.SportEvents
                .Where(se => se.Id == sportEvent.Id)
                .ExecuteUpdateAsync(setters => setters
                .SetProperty(se => se.Name, sportEvent.Name)
                .SetProperty(se => se.Sport, sportEvent.Sport)
                .SetProperty(se => se.Country, sportEvent.Country)
                .SetProperty(se => se.EventType, sportEvent.EventType)
                .SetProperty(se => se.EventDate, sportEvent.EventDate)
            );

            return sportEvent;
        }
    }
}
