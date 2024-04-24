using SponsorSphere.Application.App.Goals.Responses;
using SponsorSphere.Domain.Models;

namespace SponsorSphere.Application.Interfaces
{
    public interface IGoalRepository
    {
        Task<Goal> CreateAsync(Goal goal);
        Task<List<Goal>> GetAllAsync(int athleteId);
        Task<int> DeleteAsync(int sportEventId, int athleteId);
        void Update(GoalDto goal);
    }
}
