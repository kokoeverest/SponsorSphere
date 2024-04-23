using SponsorSphere.Domain.Models;

namespace SponsorSphere.Application.Interfaces
{
    public interface ISponsorRepository
    {
        Task<List<Sponsor>> GetByMoneyProvidedAsync();
        Task<List<Sponsor>> GetByMostAthletesAsync();
    }
}
