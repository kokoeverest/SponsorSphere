using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SponsorSphere.Domain.Models;
using SponsorSphere.Infrastructure.Extensions;
using System.Reflection;

namespace SponsorSphere.Infrastructure
{
    public class SponsorSphereDbContext : IdentityDbContext<User, UserRole, int>
    {
        public override DbSet<User> Users { get; set; } = default!;
        public DbSet<Athlete> Athletes { get; set; } = default!;
        public DbSet<BlogPost> BlogPosts { get; set; } = default!;
        public DbSet<Goal> Goals { get; set; } = default!;
        public DbSet<Picture> Pictures { get; set; } = default!;
        public DbSet<SponsorCompany> SponsorCompanies { get; set; } = default!;
        public DbSet<SponsorIndividual> SponsorIndividuals { get; set; } = default!;
        public DbSet<Sponsor> Sponsors { get; set; } = default!;
        public DbSet<Sponsorship> Sponsorships { get; set; } = default!;
        public DbSet<SportEvent> SportEvents { get; set; } = default!;
        public DbSet<Achievement> Achievements { get; set; } = default!;

        public SponsorSphereDbContext(DbContextOptions options) : base(options) { }

        public SponsorSphereDbContext() { }

//        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        {
//            optionsBuilder
//                .UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=SponsorsphereTest")
//#if DEBUG
//                .LogTo(Console.WriteLine, new[] { DbLoggerCategory.Database.Command.Name },
//                    LogLevel.Information)
//#endif
//                    ;
//        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>().UseTptMappingStrategy();
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            modelBuilder.Seed();

        }
    }
}
