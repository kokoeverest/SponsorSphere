using SponsorSphere.Domain.Models;

namespace SponsorSphere.Infrastructure.Interfaces
{
    public interface ISponsorRepository
    {
        Task<List<Sponsor>> GetByMoneyProvidedAsync();
        Task<List<Sponsor>> GetByMostAthletesAsync();
    }
}
