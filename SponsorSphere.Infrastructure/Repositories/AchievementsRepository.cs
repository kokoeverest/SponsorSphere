using Microsoft.EntityFrameworkCore;
using SponsorSphere.Application.App.Achievements.Responses;
using SponsorSphere.Application.Interfaces;
using SponsorSphere.Domain.Models;
using SponsorSphere.Infrastructure;

public class AchievementsRepository : IAchievementRepository
{
    private readonly SponsorSphereDbContext _context;

    public AchievementsRepository(SponsorSphereDbContext context)
    {
        _context = context;
    }

    public async Task<Achievement> CreateAsync(Achievement achievement)
    {
        await _context.Achievements.AddAsync(achievement);
        await _context.SaveChangesAsync();
        return achievement;
    }

    public async Task<int> DeleteAsync(int sportEventId, int athleteId)
    {
        return await _context.Achievements
                .Where(ach => ach.AthleteId == athleteId &&
                              ach.SportEventId == sportEventId)
                .ExecuteDeleteAsync();
    }

    public IQueryable<Achievement> GetAllAsync(int athleteId) => 
        _context.Achievements
            .Where(ach => ach.AthleteId == athleteId);
    

    public async Task<AchievementDto> UpdateAsync(AchievementDto updatedAchievement)
    {
        await _context.Achievements
                .Where(ach => ach.AthleteId == updatedAchievement.AthleteId && 
                              ach.SportEventId == updatedAchievement.SportEventId)
                .ExecuteUpdateAsync(setters => setters
                .SetProperty(ach => ach.Sport, updatedAchievement.Sport)
                .SetProperty(bp => bp.PlaceFinished, updatedAchievement.PlaceFinished)
                );

        return updatedAchievement;
    }
}