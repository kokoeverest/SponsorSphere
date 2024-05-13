using SponsorSphere.Application.App.Athletes.Dtos;
using SponsorSphere.Domain.Enums;
using SponsorSphere.Domain.Models;

namespace SponsorSphere.Application.Interfaces
{
    public interface IAthleteRepository
    {
        Task<Athlete> CreateAsync(Athlete user);
        Task<int> DeleteAsync(int userId);
        Task<List<Athlete>> GetAllAsync(int pageNumber, int pageSize);
        Task<Athlete> GetByIdAsync(int userId);
        Task<List<Athlete>> GetByCountryAsync(CountryEnum country, int pageNumber, int pageSize);
        Task<List<Athlete>> GetByAgeAsync(int age, int pageNumber, int pageSize);
        Task<List<Athlete>> GetBySportAsync(SportsEnum sport, int pageNumber, int pageSize);
        Task<List<Athlete>> GetByUrgentNeedAsync();
        Task<List<Athlete>> GetByAchievementsAsync(int pageNumber, int pageSize);
        Task<List<object>> GetByAmountAsync(int pageNumber, int pageSize);
        Task<AthleteDto> UpdateAsync(AthleteDto updatedAthlete);
    }
}
