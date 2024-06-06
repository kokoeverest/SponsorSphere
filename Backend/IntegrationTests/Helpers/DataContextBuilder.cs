using Microsoft.EntityFrameworkCore;
using SponsorSphere.Domain.Enums;
using SponsorSphere.Domain.Models;
using SponsorSphere.Infrastructure;
using SponsorSphere.Infrastructure.Constants;

namespace SponsorSphere.IntegrationTests.Helpers
{
    public class SponsorSphereDbContextBuilder : IDisposable
    {
        private readonly SponsorSphereDbContext _dataContext;

        public SponsorSphereDbContextBuilder(string dbName = "TestDatabase")
        {
            var options = new DbContextOptionsBuilder<SponsorSphereDbContext>()
                .UseInMemoryDatabase(databaseName: dbName)
                .EnableSensitiveDataLogging()
                .Options;

            var context = new SponsorSphereDbContext(options);

            _dataContext = context;
        }

        public SponsorSphereDbContext GetContext()
        {
            _dataContext.Database.EnsureCreated();
            return _dataContext;
        }

        public void SeedData(int number = 1)
        {
            var athletes = new List<Athlete>();

            for (int i = 0; i < number; i++)
            {
                var id = i + 1;

                athletes.Add(new Athlete
                {
                    Id = id,
                    Name = $"Athlete-{id}",
                    Email = $"athlete{i}@example.com",
                    UserName = $"athlete{i}@example.com",
                    Country = CountryEnum.BGR,
                    PhoneNumber = UserConstants.PhoneNumber,
                    BirthDate = new DateTime(2005, 3, 30),
                    Sport = SportsEnum.DownhillMountainBiking,
                    PasswordHash = UserConstants.PasswordHash,
                });
            }

            _dataContext.Athletes.AddRange(athletes);
            _dataContext.SaveChanges();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _dataContext.Dispose();
            }
        }
    }
}