using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SponsorSphere.Domain.Models;
using System.Reflection;

namespace SponsorSphere.Infrastructure
{
    public class SponsorSphereDbContext : DbContext
    {
        public DbSet<User> Users { get; set; } = default!;
        public DbSet<Athlete> Athletes { get; set; } = default!;
        public DbSet<BlogPost> BlogPosts { get; set; } = default!;
        public DbSet<Goal> Goals { get; set; } = default!;
        public DbSet<SponsorCompany> SponsorCompanies { get; set; } = default!;
        public DbSet<SponsorIndividual> SponsorIndividuals { get; set; } = default!;
        public DbSet<Sponsor> Sponsors { get; set; } = default!;
        public DbSet<Sponsorship> Sponsorships { get; set; } = default!;
        public DbSet<SportEvent> SportEvents { get; set; } = default!;
        public DbSet<Achievement> Achievements { get; set; } = default!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=SponsorsphereTest")
#if DEBUG
                .LogTo(Console.WriteLine, new[] { DbLoggerCategory.Database.Command.Name },
                    LogLevel.Information)
#endif
                    ;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().UseTptMappingStrategy();
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(modelBuilder);

        }
    }
}
