using Microsoft.VisualStudio.TestPlatform.Common;
using SponsorSphere.Application.App.Achievements.Dtos;
using SponsorSphere.Domain.Models;

namespace SponsorSphere.UnitTests.Helpers
{
    public class TestData
    {
        private static int fakeAthleteId = 2;

        private static SportEvent fakeSportEvent = new()
        {
            Id = 3,
            Name = "Test sport event name",
            Sport = Domain.Enums.SportsEnum.Football,
            Country = Domain.Enums.CountryEnum.BGR,
            EventDate = new DateTime(year: 2020, month: 9, day: 20),
            Finished = true,
            Status = Domain.Enums.SportEventStatus.Approved
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

        public static SportEvent FakeSportEvent { get => fakeSportEvent; set => fakeSportEvent = value; }
        public static int FakeAthleteId { get => fakeAthleteId; set => fakeAthleteId = value; }
        public static CreateAchievementDto FakeCreateAchievementDto { get => fakeCreateAchievementDto; set => fakeCreateAchievementDto = value; }
        public static Achievement FakeAchievement { get => fakeAchievement; set => fakeAchievement = value; }
        public static AchievementDto FakeAchievementDto { get => fakeAchievementDto; set => fakeAchievementDto = value; }
    }
}
