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

    public Task<Achievement> CreateAsync(Achievement achievement)
    {
        throw new NotImplementedException();
    }

    public Task<int> DeleteAsync(Achievement achievement)
    {
        throw new NotImplementedException();
    }

    public Task<List<Achievement>> GetAllAsync(Athlete athlete)
    {
        throw new NotImplementedException();
    }

    public void Update(Achievement achievement)
    {
        _context.SaveChanges();
        throw new NotImplementedException();
    }
}