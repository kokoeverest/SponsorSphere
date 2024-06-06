using SponsorSphere.Infrastructure;

namespace SponsorSphere.IntegrationTests.Helpers
{
    public class DatabaseFixture : IDisposable
    {
        public SponsorSphereDbContext Context { get; private set; }

        public DatabaseFixture()
        {
            var contextBuilder = new SponsorSphereDbContextBuilder();
            Context = contextBuilder.GetContext();
        }

        public void Dispose()
        {
            Context.Dispose();
        }
    }
}
