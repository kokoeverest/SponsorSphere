using Microsoft.EntityFrameworkCore;
using SponsorSphere.Application.App.Goals.Responses;
using SponsorSphere.Application.Interfaces;
using SponsorSphere.Domain.Models;

namespace SponsorSphere.Infrastructure.Repositories
{
    public class GoalRepository : IGoalRepository
    {
        private readonly SponsorSphereDbContext _context;

        public GoalRepository(SponsorSphereDbContext context)
        {
            _context = context;
        }

        public async Task<Goal> CreateAsync(Goal goal)
        {
            await _context.AddAsync(goal);
            await _context.SaveChangesAsync();
            return goal;
        }

        public async Task<int> DeleteAsync(int sportEventId, int athleteId)
        {
            return await _context.Goals
                .Where(goal => goal.AthleteId == athleteId &&
                               goal.SportEventId == sportEventId)
                .ExecuteDeleteAsync();
        }

        public async Task<List<Goal>> GetAllAsync(int athleteId)
        {
            return await _context.Goals
            .Where(goal => goal.AthleteId == athleteId)
            .ToListAsync();
        }

        public Task<Goal> UpdateAsync(GoalDto goal)
        {
            throw new NotImplementedException();
        }
    }
}
