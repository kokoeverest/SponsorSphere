namespace SponsorSphere.Application.Interfaces
{
    public interface IUnitOfWork
    {
        public IAthleteRepository AthletesRepository { get; }
        public IAchievementRepository AchievementsRepository { get; }
        public IBlogPostRepository BlogPostsRepository { get; }
        public IGoalRepository GoalsRepository { get; }
        public IPictureRepository PictureRepository { get; }
        public ISponsorCompanyRepository SponsorCompaniesRepository { get; }
        public ISponsorIndividualRepository SponsorIndividualsRepository { get; }
        public ISponsorRepository SponsorsRepository { get; }
        public ISponsorshipRepository SponsorshipsRepository { get; }
        public ISportEventRepository SportEventsRepository { get; }

        Task SaveAsync();
        Task BeginTransactionAsync();
        Task CommitTransactionAsync();
        Task RollbackTransactionAsync();
    }
}
