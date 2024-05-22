namespace SponsorSphere.Application.Interfaces
{
    public interface ISponsorRepository
    {
        Task<List<object>> GetByMoneyProvidedAsync(int pageNumber, int pageSize);
        Task<List<object>> GetByMostAthletesAsync(int pageNumber, int pageSize);
    }
}
