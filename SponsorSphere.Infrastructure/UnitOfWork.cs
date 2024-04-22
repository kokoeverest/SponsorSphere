
using SponsorSphere.Application.Interfaces;
using SponsorSphere.Infrastructure.Interfaces;

namespace SponsorSphere.Infrastructure
{
    public class UnitOfWork(SponsorSphereDbContext context,
                      IAthleteRepository athleteRepository,
                      IAchievementRepository achievementRepository,
                      IBlogPostRepository blogPostRepository,
                      IGoalRepository goalRepository,
                      ISponsorCompanyRepository sponsorCompanyRepository,
                      ISponsorIndividualRepository sponsorIndividualRepository,
                      ISponsorRepository sponsorRepository,
                      ISponsorshipRepository sponsorshipRepository,
                      ISportEventRepository sportEventRepository) : IUnitOfWork
    {

        private readonly SponsorSphereDbContext _context = context;

        public IAthleteRepository AthletesRepository { get; private set; } = athleteRepository;
        public IAchievementRepository AchievementsRepository { get; private set; } = achievementRepository;
        public IBlogPostRepository BlogPostRepository { get; private set; } = blogPostRepository;
        public IGoalRepository GoalRepository { get; private set; } = goalRepository;
        public ISponsorCompanyRepository SponsorCompanyRepository { get; private set; } = sponsorCompanyRepository;
        public ISponsorIndividualRepository SponsorIndividualRepository { get; private set; } = sponsorIndividualRepository;
        public ISponsorRepository SponsorRepository { get; private set; } = sponsorRepository;
        public ISponsorshipRepository SponsorshipRepository { get; private set; } = sponsorshipRepository;
        public ISportEventRepository SportEventRepository { get; private set; } = sportEventRepository;

        public async Task BeginTransactionAsync()
        {
            await _context.Database.BeginTransactionAsync();
        }
        public async Task CommitTransactionAsync()
        {
            await _context.Database.CommitTransactionAsync();
        }
        public async Task RollbackTransactionAsync()
        {
            await _context.Database.RollbackTransactionAsync();
        }
        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
