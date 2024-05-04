using Microsoft.EntityFrameworkCore;
using SponsorSphere.Domain.Enums;
using SponsorSphere.Domain.Models;

namespace SponsorSphere.Infrastructure.Extensions
{
    internal static class ModelBuilderExtensions
    {
        internal static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Athlete>().HasData(
                new Athlete
                {
                    Id = 1,
                    Name = "Petar",
                    LastName = "Petrov",
                    Email = "5rov@mail.mail",
                    UserName = "5rov@mail.mail",
                    Country = CountryEnum.BGR,
                    PhoneNumber = "09198",
                    BirthDate = DateTime.Parse("30/09/1983").ToUniversalTime(),
                    Sport = SportsEnum.MountainRunning
                },
                new Athlete
                {
                    Id = 2,
                    Name = "Georgi",
                    LastName = "Petkov",
                    Email = "5kov@mail.mail",
                    UserName = "5kov@mail.mail",
                    Country = CountryEnum.BGR,
                    PhoneNumber = "09198",
                    BirthDate = DateTime.Parse("30/03/2005").ToUniversalTime(),
                    Sport = SportsEnum.Golf
                },
                new Athlete
                {
                    Id = 7,
                    Name = "Kaloyan",
                    LastName = "Peychev",
                    Email = "kalo@mail.bg",
                    NormalizedEmail = "kalo@mail.bg".ToUpper(),
                    UserName = "kalo@mail.bg",
                    NormalizedUserName = "kalo@mail.bg".ToUpper(),
                    Country = CountryEnum.BGR,
                    PhoneNumber = "123456",
                    PasswordHash = "A1B2C3",
                    BirthDate = DateTime.Parse("15/08/1997").ToUniversalTime(),
                    Sport = SportsEnum.DownhillMountainBiking,
                    PictureId = 1,
                    StravaLink = "www.strava.co/userkaloyan"
                }
            );

            modelBuilder.Entity<SponsorCompany>().HasData(
                new SponsorCompany
                {
                    Id = 3,
                    Name = "Lidl",
                    Email = "lidl@bg.gb",
                    Country = CountryEnum.BGR,
                    PhoneNumber = "1223",
                    IBAN = "BG12345"
                },
                new SponsorCompany
                {
                    Id = 4,
                    Name = "Kaufland",
                    Email = "kaufland@bg.gb",
                    Country = CountryEnum.DEU,
                    PhoneNumber = "1223",
                    IBAN = "DE32215"
                }
            );

            modelBuilder.Entity<SponsorIndividual>().HasData(
                new SponsorIndividual
                {
                    Id = 5,
                    Name = "Lazar",
                    Email = "anonimen@bg.gb",
                    Country = CountryEnum.BGR,
                    PhoneNumber = "1223",
                    LastName = "Randov",
                    BirthDate = DateTime.Parse("03/01/1990").ToUniversalTime(),
                },
                new SponsorIndividual
                {
                    Id = 6,
                    Name = "Michael",
                    Email = "michael@bg.gb",
                    Country = CountryEnum.AUS,
                    PhoneNumber = "1223",
                    LastName = "Uzunov",
                    BirthDate = DateTime.Parse("30/03/1975").ToUniversalTime(),
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
                    EventDate = DateTime.Parse("2020/08/16").ToUniversalTime(),
                    Country = CountryEnum.BGR
                },
                new SportEvent
                {
                    Id = 2,
                    Sport = SportsEnum.SkyRunning,
                    Name = "Zegama Aizkori",
                    Finished = false,
                    EventType = EventsEnum.Race,
                    EventDate = DateTime.Parse("2024/08/16").ToUniversalTime(),
                    Country = CountryEnum.ESP
                }
            );

            modelBuilder.Entity<Achievement>().HasData(
                new Achievement
                {
                    AthleteId = 2,
                    Sport = SportsEnum.SkyRunning,
                    SportEventId = 1,
                    PlaceFinished = 1
                },
                new Achievement
                {
                    AthleteId = 1,
                    Sport = SportsEnum.MountainRunning,
                    SportEventId = 1,
                    PlaceFinished = 2
                }
                );

            modelBuilder.Entity<Goal>().HasData(
                new Goal
                {
                    AthleteId = 2,
                    Sport = SportsEnum.SkyRunning,
                    SportEventId = 2,
                    Date = DateTime.Parse("2024/08/16").ToUniversalTime(),
                    AmountNeeded = 5000
                });

            modelBuilder.Entity<Picture>().HasData(
                new Picture
                {
                    Id = 1,
                    Url = @"https://drive.google.com/file/d/1PVTg8DDjnKEu2L_M2Oe4YBicC_Cvpy4C/view?usp=sharing",
                    Modified = DateTime.UtcNow
                },
                new Picture
                {
                    Id = 2,
                    Url = @"https://drive.google.com/file/d/1QLGlPj9PCHBU1Lc-TQNajmHlvueoaoUG/view?usp=sharing",
                    Modified = DateTime.UtcNow
                });

            modelBuilder.Entity<BlogPost>().HasData(
                new BlogPost
                {
                    Id = 1,
                    Content = "A very interesting post about a sport achievement",
                    Created = DateTime.Parse("2023.12.06").ToUniversalTime(),
                    AuthorId = 4
                },
                new BlogPost
                {
                    Id = 2,
                    Content = @"I want to share about my experience as a downhill mountain biker. I was born in 1997 and grew up in a small villeag in the Swiss Alps. The name of the village is Zinal and it has some quite nice mountians around, which have fascinated me throughout my life!",
                    Created = DateTime.Parse("2023.12.06").ToUniversalTime(),
                    AuthorId = 4
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
