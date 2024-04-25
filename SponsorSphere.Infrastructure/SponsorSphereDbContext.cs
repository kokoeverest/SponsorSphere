using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SponsorSphere.Domain.Enums;
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
        public DbSet<Picture> Pictures { get; set; } = default!;
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

            modelBuilder.Entity<Athlete>().HasData(
                new Athlete
                {
                    Id = 1,
                    Name = "Petar",
                    LastName = "Petrov",
                    Email = "5rov@mail.mail",
                    Password = "dd",
                    Country = "bg",
                    PhoneNumber = "09198",
                    BirthDate = DateTime.Parse("30/09/1983"),
                    Sport = SportsEnum.MountainRunning
                },
                new Athlete
                {
                    Id = 2,
                    Name = "Georgi",
                    LastName = "Petkov",
                    Email = "5kov@mail.mail",
                    Password = "ss",
                    Country = "bg",
                    PhoneNumber = "09198",
                    BirthDate = DateTime.Parse("30/03/2005"),
                    Sport = SportsEnum.Golf
                }
            );

            modelBuilder.Entity<SponsorCompany>().HasData(
                new SponsorCompany
                {
                    Id = 3,
                    Name = "Lidl",
                    Email = "lidl@bg.gb",
                    Password = "ll",
                    Country = "bg",
                    PhoneNumber = "1223",
                    IBAN = "BG12345"
                },
                new SponsorCompany
                {
                    Id = 4,
                    Name = "Kaufland",
                    Email = "kaufland@bg.gb",
                    Password = "kk",
                    Country = "de",
                    PhoneNumber = "1223",
                    IBAN = "DE32215"
                }
            );

            modelBuilder.Entity<Sponsorship>().HasData(
                new Sponsorship
                {
                    AthleteId = 1,
                    SponsorId = 3,
                    Amount = 2000,
                    Level = SponsorshipLevel.SinglePayment,
                });

            modelBuilder.Entity<SportEvent>().HasData(
                new SportEvent
                {
                    Id = 1,
                    Sport = SportsEnum.MountainRunning,
                    Name = "Persenk ultra",
                    Finished = true,
                    EventType = EventsEnum.Race,
                    EventDate = DateTime.Parse("2020/08/16"),
                    Country = "Bulgaria"
                },
                new SportEvent
                {
                    Id = 2,
                    Sport = SportsEnum.SkyRunning,
                    Name = "Zegama Aizkori",
                    Finished = false,
                    EventType = EventsEnum.Race,
                    EventDate = DateTime.Parse("2024/08/16"),
                    Country = "Spain"
                }
            );

            modelBuilder.Entity<Achievement>().HasData(
                new Achievement
                {
                    AthleteId = 2,
                    Sport = SportsEnum.SkyRunning,
                    SportEventId = 1,
                    PlaceFinished = 1
                });

            modelBuilder.Entity<Goal>().HasData(
                new Goal
                {
                    AthleteId = 2,
                    Sport = SportsEnum.SkyRunning,
                    SportEventId = 2,
                    Date = DateTime.Parse("2024/08/16"),
                    AmountNeeded = 5000
                });

            modelBuilder.Entity<Picture>().HasData(
                new Picture
                {
                    Id = 1,
                    Url = @"https://drive.google.com/file/d/1PVTg8DDjnKEu2L_M2Oe4YBicC_Cvpy4C/view?usp=sharing",
                    Modified = DateTime.Today
                },
                new Picture
                {
                    Id = 2,
                    Url = @"https://drive.google.com/file/d/1QLGlPj9PCHBU1Lc-TQNajmHlvueoaoUG/view?usp=sharing",
                    Modified = DateTime.Today
                });

            modelBuilder.Entity<BlogPost>().HasData(
                new BlogPost
                {
                    Id = 1,
                    Content = "A very interesting post about a sport achievement",
                    Created = DateTime.Parse("2023.12.06"),
                    AuthorId = 4
                    
                });

            base.OnModelCreating(modelBuilder);

        }
    }
}
