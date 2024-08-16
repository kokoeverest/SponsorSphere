using Microsoft.AspNetCore.Http;
using SponsorSphere.Application.App.Achievements.Dtos;
using SponsorSphere.Application.App.BlogPosts.Dtos;
using SponsorSphere.Application.Common.Constants;
using SponsorSphere.Domain.Enums;
using SponsorSphere.Domain.Models;
using SponsorSphere.Infrastructure.Constants;
using System.Text;

namespace SponsorSphere.UnitTests.Helpers
{
    public class TestData
    {
        private const string _fakePassword = "Password1";
        private const string _fakeBlogPostText = "Some random text which should be longer than 50 symbols.";
        private static int fakeBlogPostId = 1;
        private static int fakeAthleteId = 2;

        private static readonly byte[] _validFileSize = new byte[FileConstants.MaxFileSize - 1];
        private static readonly byte[] _tooLargeFileSize = new byte[FileConstants.MaxFileSize + 1];
        private static readonly byte[] _tooSmallFileSize = new byte[FileConstants.MinFileSize - 1];
        private static readonly MemoryStream ValidStream = new(_validFileSize);
        private static readonly MemoryStream InvalidStream = new(_tooLargeFileSize);
        private static readonly string _fileName = "filename.jpg";
        private static readonly string _name = "name";

        private static Picture samplePicture = new()
        {
            Id = 1,
            Modified = DateTime.UtcNow,
            Content = Encoding.ASCII.GetBytes(UserConstants.PictureContent)
        };

        internal static IFormFile fakeValidIFormFile = new FormFile(ValidStream, 0, ValidStream.Length, _name, _fileName)
        {
            Headers = new HeaderDictionary(),
            ContentType = "image/jpeg"
        };

        internal static IFormFile fakeTooLargeIFormFile = new FormFile(InvalidStream, 0, InvalidStream.Length, _name, _fileName)
        {
            Headers = new HeaderDictionary(),
            ContentType = "image/jpeg"
        };

        internal static IFormFile fakeTooSmallIFormFile = new FormFile(new MemoryStream(_tooSmallFileSize), 0, _tooSmallFileSize.Length, _name, _fileName)
        {
            Headers = new HeaderDictionary(),
            ContentType = "image/jpeg"
        };

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

        private static BlogPost fakeBlogPost = new()
        {
            Id = fakeBlogPostId,
            Content = FakeBlogPostText!,
            AuthorId = FakeAthlete.Id,
            Pictures = []
        };

        private static BlogPost fakeBlogPostWithPictures = new()
        {
            Id = fakeBlogPostId,
            Content = FakeBlogPostText!,
            AuthorId = FakeAthlete.Id,
            Pictures = [ samplePicture ]
        };

        private static BlogPostDto fakeBlogPostDto = new()
        {
            Id = fakeBlogPostId,
            Content = FakeBlogPostText!,
            AuthorId = FakeAthlete.Id,
            Pictures = []
        };

        private static BlogPostDto fakeBlogPostDtoWithPictures = new()
        {
            Id = fakeBlogPostId,
            Content = FakeBlogPostText!,
            AuthorId = FakeAthlete.Id,
            Pictures = [ samplePicture ]
        };

        public static string FakePassword = _fakePassword;
        public static string FakeBlogPostText = _fakeBlogPostText;

        public static SportEvent FakeSportEvent { get => fakeSportEvent; set => fakeSportEvent = value; }
        public static int FakeAthleteId { get => fakeAthleteId; set => fakeAthleteId = value; }
        public static CreateAchievementDto FakeCreateAchievementDto { get => fakeCreateAchievementDto; set => fakeCreateAchievementDto = value; }
        public static Achievement FakeAchievement { get => fakeAchievement; set => fakeAchievement = value; }
        public static AchievementDto FakeAchievementDto { get => fakeAchievementDto; set => fakeAchievementDto = value; }
        public static Athlete FakeAthlete { get => fakeAthlete; set => fakeAthlete = value; }
        public static BlogPost FakeBlogPost { get => fakeBlogPost; set => fakeBlogPost = value; }
        public static BlogPost FakeBlogPostWithPictures { get => fakeBlogPostWithPictures; set => fakeBlogPostWithPictures = value; }
        public static BlogPostDto FakeBlogPostDto { get => fakeBlogPostDto; set => fakeBlogPostDto = value; }
        public static BlogPostDto FakeBlogPostDtoWithPictures { get => fakeBlogPostDtoWithPictures; set => fakeBlogPostDtoWithPictures = value; }
    }
}
