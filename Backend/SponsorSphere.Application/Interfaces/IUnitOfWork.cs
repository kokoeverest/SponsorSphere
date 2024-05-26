namespace SponsorSphere.Application.Interfaces
{
    public interface IUnitOfWork
    {
        public IAthleteRepository AthletesRepository { get; }
        public IAchievementRepository AchievementsRepository { get; }
        public IBlogPostRepository BlogPostsRepository { get; }
        public IGoalRepository GoalsRepository { get; }
        public IPictureRepository PicturesRepository { get; }
        public ISponsorCompanyRepository SponsorCompaniesRepository { get; }
        public ISponsorIndividualRepository SponsorIndividualsRepository { get; }
        public ISponsorRepository SponsorsRepository { get; }
        public ISponsorshipRepository SponsorshipsRepository { get; }
        public ISportEventRepository SportEventsRepository { get; }

        /// <summary>
        /// Asynchronously saves all changes made in this unit of work.
        /// </summary>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task SaveAsync();

        /// <summary>
        /// Asynchronously begins a new transaction.
        /// </summary>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task BeginTransactionAsync();

        /// <summary>
        /// Asynchronously commits the current transaction.
        /// </summary>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task CommitTransactionAsync();

        /// <summary>
        /// Asynchronously rolls back the current transaction.
        /// </summary>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task RollbackTransactionAsync();
    }
}
