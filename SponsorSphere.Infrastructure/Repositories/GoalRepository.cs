using SponsorSphere.Application.Interfaces;

namespace SponsorSphere.Infrastructure.Repositories
{
    public class GoalRepository : IGoalRepository
    {
        private readonly SponsorSphereDbContext _context;

        public GoalRepository(SponsorSphereDbContext context)
        {
            _context = context;
        }
    }
}
