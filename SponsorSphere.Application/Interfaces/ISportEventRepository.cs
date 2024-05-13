using SponsorSphere.Application.App.SportEvents.Dtos;
using SponsorSphere.Domain.Enums;
using SponsorSphere.Domain.Models;

namespace SponsorSphere.Application.Interfaces
{
    public interface ISportEventRepository
    {
        Task<SportEvent> CreateAsync(SportEvent sportEvent);
        Task<SportEvent> GetByIdAsync(int sportEventId);
        Task<List<SportEvent>> GetBySportAsync(SportsEnum sport, int pageNumber, int pageSize);
        Task<List<SportEvent>> GetFinishedSportEventsAsync(SportsEnum sport, int pageNumber, int pageSize);
        Task<List<SportEvent>> GetUnfinishedSportEventsAsync(SportsEnum sport, int pageNumber, int pageSize);
        Task<SportEvent?> SearchAsync(SportEvent sportEvent);
        Task<SportEventDto> UpdateAsync(SportEventDto sportEvent);
        Task<int> DeleteAsync(int sportEventId);
    }
}
