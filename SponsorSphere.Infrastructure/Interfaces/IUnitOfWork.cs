using SponsorSphere.Infrastructure.Interfaces;

namespace SponsorSphere.Application.Interfaces
{
    public interface IUnitOfWork
    {
        public IAthleteRepository AthletesRepository { get; }
        public IAchievementRepository AchievementsRepository { get; }
        public IBlogPostRepository BlogPostRepository { get; }
        public IGoalRepository GoalRepository { get; }
        public ISponsorCompanyRepository SponsorCompanyRepository { get; }
        public ISponsorIndividualRepository SponsorIndividualRepository { get; }
        public ISponsorRepository SponsorRepository { get; }
        public ISponsorshipRepository SponsorshipRepository { get; }
        public ISportEventRepository SportEventRepository { get; }

        Task SaveAsync();
        Task BeginTransactionAsync();
        Task CommitTransactionAsync();
        Task RollbackTransactionAsync();
    }
}
