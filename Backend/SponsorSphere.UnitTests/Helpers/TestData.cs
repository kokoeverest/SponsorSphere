using SponsorSphere.Application.App.Achievements.Dtos;
using SponsorSphere.Domain.Enums;
using SponsorSphere.Domain.Models;
using SponsorSphere.Infrastructure.Constants;

namespace SponsorSphere.UnitTests.Helpers
{
    public class TestData
    {
        private const string _fakePassword = "Password1";
        private static int fakeAthleteId = 2;

        private static SportEvent fakeSportEvent = new()
        {
            Id = 3,
            Name = "Test sport event name",
            Sport = SportsEnum.Football,
            Country = CountryEnum.BGR,
            EventDate = new DateTime(year: 2020, month: 9, day: 20),
            Finished = true,
            Status = SportEventStatus.Approved
        };

        private static CreateAchievementDto fakeCreateAchievementDto = new()
        {
            SportEventId = FakeSportEvent.Id,
            PlaceFinished = 1
        };

        private static Achievement fakeAchievement = new()
        {
            Sport = FakeSportEvent.Sport,
            SportEventId = FakeSportEvent.Id,
            AthleteId = fakeAthleteId,
            PlaceFinished = fakeCreateAchievementDto.PlaceFinished
        };

        private static AchievementDto fakeAchievementDto = new()
        {
            Sport = FakeSportEvent.Sport,
            SportEventId = FakeSportEvent.Id,
            AthleteId = fakeAthleteId,
            PlaceFinished = fakeCreateAchievementDto.PlaceFinished
        };

        private static Athlete fakeAthlete = new()
        {
            Id = fakeAthleteId,
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
            StravaLink = "www.strava.co/userpetar"
        };

        public static string FakePassword = _fakePassword;

        public static SportEvent FakeSportEvent { get => fakeSportEvent; set => fakeSportEvent = value; }
        public static int FakeAthleteId { get => fakeAthleteId; set => fakeAthleteId = value; }
        public static CreateAchievementDto FakeCreateAchievementDto { get => fakeCreateAchievementDto; set => fakeCreateAchievementDto = value; }
        public static Achievement FakeAchievement { get => fakeAchievement; set => fakeAchievement = value; }
        public static AchievementDto FakeAchievementDto { get => fakeAchievementDto; set => fakeAchievementDto = value; }
        public static Athlete FakeAthlete { get => fakeAthlete; set => fakeAthlete = value; }

    }
}
