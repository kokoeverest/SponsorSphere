using Azure;
using Microsoft.EntityFrameworkCore;
using SponsorSphere.Application.App.Goals.Dtos;
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

        public async Task<List<Goal>> GetAllAsync(int athleteId, int pageNumber, int pageSize)
        {
            return await _context.Goals
            .Where(goal => goal.AthleteId == athleteId)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
        }

        public async Task<GoalDto> UpdateAsync(GoalDto updatedGoal)
        {
            await _context.Goals
                .Where(g => g.AthleteId.Equals(updatedGoal.AthleteId))
                .ExecuteUpdateAsync(setters => setters
                .SetProperty(g => g.Sport, updatedGoal.Sport)
                .SetProperty(g => g.AmountNeeded, updatedGoal.AmountNeeded)
                );

            return updatedGoal;
        }
    }
}
