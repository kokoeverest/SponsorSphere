using Microsoft.EntityFrameworkCore;
using SponsorSphere.Application.App.Achievements.Dtos;
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

    public async Task<int> DeleteAsync(int sportEventId, int athleteId) =>
        
        await _context.Achievements
                .Where(ach => ach.AthleteId == athleteId &&
                              ach.SportEventId == sportEventId)
                .ExecuteDeleteAsync();

    public async Task<List<Achievement>> GetAllAsync(int athleteId, int pageNumber, int pageSize) => 

        await _context.Achievements
            .Where(ach => ach.AthleteId == athleteId)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

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