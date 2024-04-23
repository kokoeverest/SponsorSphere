using SponsorSphere.Domain.Enums;
using SponsorSphere.Domain.Models;

namespace SponsorSphere.ConsolePresentation
{
    public class Seeder
    {
        public static List<Athlete> SeedAthletes()
        {
            return [
                new Athlete
                {
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
                    Name = "Georgi",
                    LastName = "Petkov",
                    Email = "5kov@mail.mail",
                    Password = "ss",
                    Country = "bg",
                    PhoneNumber = "09198",
                    BirthDate = DateTime.Parse("30/03/2005"),
                    Sport = SportsEnum.Golf
                }
            ];
        }

        public static List<SponsorCompany> SeedSponsorCompanies()
        {
            return [
                new SponsorCompany
                {
                    Name = "Lidl",
                    Email = "lidl@bg.gb",
                    Password = "ll",
                    Country = "bg",
                    PhoneNumber = "1223",
                    IBAN = "BG12345"
                },
                new SponsorCompany
                {
                    Name = "Kaufland",
                    Email = "kaufland@bg.gb",
                    Password = "kk",
                    Country = "de",
                    PhoneNumber = "1223",
                    IBAN = "DE32215"
                }
            ];
        }

        public static List<SportEvent> SeedSportEvents()
        {
            return [
                new SportEvent
                {
                    Sport = SportsEnum.MountainRunning,
                    Name = "Persenk ultra",
                    EventType = EventsEnum.Race,
                    EventDate = DateTime.Parse("2020/08/16"),
                    Country = "Bulgaria"
                },
                new SportEvent
                {
                    Sport = SportsEnum.SkyRunning,
                    Name = "Zegama Aizkori",
                    EventType = EventsEnum.Race,
                    EventDate = DateTime.Parse("2024/08/16"),
                    Country = "Spain"
                }
            ];
        }
    }
}
