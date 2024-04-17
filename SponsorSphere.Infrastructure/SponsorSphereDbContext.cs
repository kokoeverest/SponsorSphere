using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SponsorSphere.Domain.Enums;
using SponsorSphere.Domain.Models;
using System.Data.SqlTypes;

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
        public DbSet<Sponsorship> Sponsorships { get; set; } = default!;
        public DbSet<SportEvent> SportEvents { get; set; } = default!;
        public DbSet<Achievement> Achievements { get; set; } = default!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=SponsorsphereTest")
                .LogTo(Console.WriteLine, new[] { DbLoggerCategory.Database.Command.Name },
                    LogLevel.Information);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Achievement>()
                .Property(e => e.Sport)
                .HasConversion<int>();

            modelBuilder.Entity<Athlete>()
                .Property(e => e.Sport)
                .HasConversion<int>();

            modelBuilder.Entity<Goal>()
                .Property(e => e.Sport)
                .HasConversion<int>();

            modelBuilder.Entity<Goal>()
                .Property(e => e.AmountNeeded)
                .HasConversion<decimal>();

            modelBuilder.Entity<Goal>()
                .HasKey(e => new { e.SportEventId, e.AthleteId});

            modelBuilder.Entity<Sponsorship>()
                .Property(e => e.Level)
                .HasConversion<int>();

            modelBuilder.Entity<Sponsorship>()
                .Property(e => e.Amount)
                .HasConversion<decimal>();

            modelBuilder.Entity<Sponsorship>()
                .HasKey(e => new { e.SponsorId, e.AthleteId });

            modelBuilder.Entity<SportEvent>()
                .Property(e => e.Sport)
                .HasConversion<int>();

            modelBuilder.Entity<SportEvent>()
                 .Property(e => e.EventType)
                 .HasConversion<int>();
        }
    }
}
