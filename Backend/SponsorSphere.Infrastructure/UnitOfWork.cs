
using SponsorSphere.Application.Interfaces;

namespace SponsorSphere.Infrastructure
{
    public class UnitOfWork(SponsorSphereDbContext context,
                      IAthleteRepository athleteRepository,
                      IAchievementRepository achievementRepository,
                      IBlogPostRepository blogPostRepository,
                      IBlogPostPictureRepository blogPostPictureRepository,
                      IGoalRepository goalRepository,
                      IPictureRepository pictureRepository,
                      ISponsorCompanyRepository sponsorCompanyRepository,
                      ISponsorIndividualRepository sponsorIndividualRepository,
                      ISponsorRepository sponsorRepository,
                      ISponsorshipRepository sponsorshipRepository,
                      ISportEventRepository sportEventRepository) : IUnitOfWork
    {

        private readonly SponsorSphereDbContext _context = context;

        public IAthleteRepository AthletesRepository { get; private set; } = athleteRepository;
        public IAchievementRepository AchievementsRepository { get; private set; } = achievementRepository;
        public IBlogPostRepository BlogPostsRepository { get; private set; } = blogPostRepository;
        public IBlogPostPictureRepository BlogPostPictureRepository { get; private set; } = blogPostPictureRepository;
        public IGoalRepository GoalsRepository { get; private set; } = goalRepository;
        public IPictureRepository PicturesRepository { get; private set; } = pictureRepository;
        public ISponsorCompanyRepository SponsorCompaniesRepository { get; private set; } = sponsorCompanyRepository;
        public ISponsorIndividualRepository SponsorIndividualsRepository { get; private set; } = sponsorIndividualRepository;
        public ISponsorRepository SponsorsRepository { get; private set; } = sponsorRepository;
        public ISponsorshipRepository SponsorshipsRepository { get; private set; } = sponsorshipRepository;
        public ISportEventRepository SportEventsRepository { get; private set; } = sportEventRepository;

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
