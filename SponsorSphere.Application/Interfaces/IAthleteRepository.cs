using SponsorSphere.Application.App.Athletes.Dtos;
using SponsorSphere.Domain.Enums;
using SponsorSphere.Domain.Models;

namespace SponsorSphere.Application.Interfaces
{
    public interface IAthleteRepository
    {
        Task<Athlete> CreateAsync(Athlete user);
        Task<int> DeleteAsync(int userId);
        Task<List<Athlete>> GetAllAsync();
        Task<List<Athlete>> GetAllAsync(int pageNumber, int pageSize);
        Task<Athlete> GetByIdAsync(int userId);
        Task<List<Athlete>> GetByCountryAsync(CountryEnum country);
        Task<List<Athlete>> GetByAgeAsync(int age);
        Task<List<Athlete>> GetBySportAsync(SportsEnum sport);
        Task<List<Athlete>> GetByUrgentNeedAsync();
        Task<List<Athlete>> GetByAchievementsAsync();
        Task<List<object>> GetByAmountAsync();
        Task<AthleteDto> UpdateAsync(AthleteDto updatedAthlete);
    }
}
