using SponsorSphere.Domain.Models;

namespace SponsorSphere.Application.Interfaces
{
    public interface ISponsorRepository
    {
        Task<List<object>> GetByMoneyProvidedAsync();
        Task<List<object>> GetByMostAthletesAsync();
    }
}
