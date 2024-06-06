using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SponsorSphere.Domain.Enums;
using SponsorSphere.Domain.Models;
using SponsorSphere.Infrastructure.Constants;
using System.Text;

namespace SponsorSphere.Infrastructure.Extensions
{
    internal static class ModelBuilderExtensions
    {
        internal static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdentityUserRole<int>>().HasData(
                new IdentityUserRole<int>
                {
                    RoleId = 1,
                    UserId = 1
                });

            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    Name = UserConstants.AdminName,
                    Email = UserConstants.AdminEmail,
                    NormalizedEmail = UserConstants.AdminEmail.ToUpper(),
                    UserName = UserConstants.AdminEmail,
                    NormalizedUserName = UserConstants.AdminEmail.ToUpper(),
                    SecurityStamp = UserConstants.AdminSecurityStamp,
                    ConcurrencyStamp = UserConstants.AdminConcurrencyStamp,
                    Country = CountryEnum.BGR,
                    PhoneNumber = UserConstants.PhoneNumber,
                    PasswordHash = UserConstants.PasswordHash,
                });

            modelBuilder.Entity<Athlete>().HasData(
                
                new Athlete
                {
                    Id = 5,
                    Name = "Petar",
                    LastName = "Petrov",
                    Email = "test@mail.bg",
                    UserName = "test@mail.bg",
                    NormalizedEmail = "test@mail.bg".ToUpper(),
                    NormalizedUserName = "test@mail.bg".ToUpper(),
                    Country = CountryEnum.BGR,
                    PhoneNumber = UserConstants.PhoneNumber,
                    BirthDate = new DateTime(1983, 9, 30),
                    Sport = SportsEnum.TrailRunning,
                    StravaLink = "www.strava.co/userpetar",
                    PasswordHash = UserConstants.PasswordHash,
                },
                new Athlete
                {
                    Id = 6,
                    Name = "Georgi",
                    LastName = "Petkov",
                    Email = "5kov@mail.mail",
                    UserName = "5kov@mail.mail",
                    Country = CountryEnum.BGR,
                    PhoneNumber = UserConstants.PhoneNumber,
                    BirthDate = new DateTime(2005, 3, 30),
                    Sport = SportsEnum.DownhillMountainBiking,
                    PasswordHash = UserConstants.PasswordHash,
                }
            );

            modelBuilder.Entity<SponsorCompany>().HasData(
                new SponsorCompany
                {
                    Id = 3,
                    Name = "Lidl",
                    Email = "lidl@mail.bg",
                    UserName = "lidl@mail.bg",
                    NormalizedEmail = "lidl@mail.bg".ToUpper(),
                    NormalizedUserName = "lidl@mail.bg".ToUpper(),
                    Country = CountryEnum.BGR,
                    PhoneNumber = UserConstants.PhoneNumber,
                    PasswordHash = UserConstants.PasswordHash,
                    Iban = "BG12345"
                },
                new SponsorCompany
                {
                    Id = 4,
                    Name = "Kaufland",
                    Email = "kaufland@bg.gb",
                    Country = CountryEnum.DEU,
                    PhoneNumber = UserConstants.PhoneNumber,
                    Iban = "DE32215",
                    PasswordHash = UserConstants.PasswordHash,
                }
            );

            modelBuilder.Entity<SponsorIndividual>().HasData(
                new SponsorIndividual
                {
                    Id = 8,
                    Name = "Lazar",
                    Email = "anonimen@bg.gb",
                    Country = CountryEnum.BGR,
                    PhoneNumber = UserConstants.PhoneNumber,
                    LastName = "Randov",
                    BirthDate = new DateTime(1990, 1, 3),
                    PasswordHash = UserConstants.PasswordHash,
                },
                new SponsorIndividual
                {
                    Id = 7,
                    Name = "Michael",
                    Email = "michael@bg.gb",
                    Country = CountryEnum.AUS,
                    PhoneNumber = "1223",
                    LastName = "Uzunov",
                    BirthDate = new DateTime(1975, 3, 5),
                    PasswordHash = UserConstants.PasswordHash,
                }
            );

            modelBuilder.Entity<Sponsorship>().HasData(
                new Sponsorship
                {
                    AthleteId = 6,
                    SponsorId = 3,
                    Amount = 2000,
                    Level = SponsorshipLevel.SinglePayment,
                });

            modelBuilder.Entity<SportEvent>().HasData(
                new SportEvent
                {
                    Id = 1,
                    Sport = SportsEnum.TrailRunning,
                    Name = "Persenk ultra 2020",
                    Finished = true,
                    EventType = EventsEnum.Race,
                    EventDate = new DateTime(2020, 8, 16),
                    Country = CountryEnum.BGR,
                    Status = SportEventStatus.Approved
                },
                new SportEvent
                {
                    Id = 2,
                    Sport = SportsEnum.SkyRunning,
                    Name = "Zegama Aizkori 2024",
                    Finished = false,
                    EventType = EventsEnum.Race,
                    EventDate = new DateTime(2024, 8, 16),
                    Country = CountryEnum.ESP,
                    Status = SportEventStatus.Pending
                },
                new SportEvent
                {
                    Id = 3,
                    Sport = SportsEnum.TrailRunning,
                    Name = "Vitosha 24h challenge 2019",
                    Finished = true,
                    EventType = EventsEnum.Training,
                    EventDate = new DateTime(2019, 9, 9),
                    Country = CountryEnum.BGR,
                    Status = SportEventStatus.Approved
                }
            );

            modelBuilder.Entity<Achievement>().HasData(
                new Achievement
                {
                    AthleteId = 6,
                    Sport = SportsEnum.SkyRunning,
                    SportEventId = 1,
                    PlaceFinished = 1
                },
                new Achievement
                {
                    AthleteId = 5,
                    Sport = SportsEnum.TrailRunning,
                    SportEventId = 1,
                    PlaceFinished = 2
                },
                new Achievement
                {
                    AthleteId = 5,
                    Sport = SportsEnum.TrailRunning,
                    SportEventId = 3,
                    Description = "I was able to run 24 hours in Vitosha mountain"
                }
                );

            modelBuilder.Entity<Goal>().HasData(
                new Goal
                {
                    AthleteId = 6,
                    Sport = SportsEnum.SkyRunning,
                    SportEventId = 2,
                    Date = new DateTime(2024, 8, 16),
                    AmountNeeded = 5000
                });

            modelBuilder.Entity<Picture>().HasData(
                new Picture
                {
                    Id = 1,
                    Modified = DateTime.UtcNow,
                    Content = Encoding.ASCII.GetBytes(UserConstants.PictureContent)
                },
                new Picture
                {
                    Id = 2,
                    Modified = DateTime.UtcNow,
                    Content = Encoding.ASCII.GetBytes(UserConstants.PictureContent),
                });

            modelBuilder.Entity<BlogPost>().HasData(
                new BlogPost
                {
                    Id = 1,
                    Content = "A very interesting post about a sport achievement",
                    Created = new DateTime(2016, 6, 12),
                    AuthorId = 6
                },
                new BlogPost
                {
                    Id = 2,
                    Content = @"I want to share about my experience as a downhill mountain biker. I was born in 1997 and grew up in a small villeag in the Swiss Alps. The name of the village is Zinal and it has some quite nice mountians around, which have fascinated me throughout my life!",
                    Created = new DateTime(2023, 12,6),
                    AuthorId = 6
                }
                );

            modelBuilder.Entity<BlogPostPicture>().HasData(
                new BlogPostPicture
                {
                    PictureId = 1,
                    BlogPostId = 1
                },
                new BlogPostPicture
                {
                    PictureId = 2,
                    BlogPostId = 1
                });

            modelBuilder.Entity<UserRole>().HasData(
                 new UserRole
                 {
                     Id = 1,
                     Name = RolesEnum.Admin.ToString(),
                     NormalizedName = RolesEnum.Admin.ToString().ToUpper()
                 },
                new UserRole
                {
                    Id = 3,
                    Name = RolesEnum.Sponsor.ToString(),
                    NormalizedName = RolesEnum.Sponsor.ToString().ToUpper()
                },
               new UserRole
               {
                   Id = 2,
                   Name = RolesEnum.Athlete.ToString(),
                   NormalizedName = RolesEnum.Athlete.ToString().ToUpper()
               }
            );
        }
    }
}
