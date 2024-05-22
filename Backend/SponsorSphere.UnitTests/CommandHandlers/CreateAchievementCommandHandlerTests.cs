using AutoMapper;
using Microsoft.Extensions.Logging;
using NSubstitute;
using SponsorSphere.Application.App.Achievements.Commands;
using SponsorSphere.Application.App.Achievements.Dtos;
using SponsorSphere.Application.Common.Exceptions;
using SponsorSphere.Application.Interfaces;
using SponsorSphere.Domain.Models;
using SponsorSphere.UnitTests.Helpers;

namespace SponsorSphere.UnitTests.CommandHandlers
{
    public class CreateAchievementCommandHandlerTests
    {
        private readonly IUnitOfWork _unitOfWorkMock;
        private readonly IMapper _mapperMock;
        private readonly ILogger<CreateAchievementCommandHandler> _loggerMock;

        public CreateAchievementCommandHandlerTests()
        {
            _unitOfWorkMock = Substitute.For<IUnitOfWork>();
            _mapperMock = Substitute.For<IMapper>();
            _loggerMock = Substitute.For<ILogger<CreateAchievementCommandHandler>>();
        }

        [Fact]
        public async Task CreateAchievement_ValidCommand_ShouldCreateAchievement()
        {
            // Arrange
            _unitOfWorkMock.SportEventsRepository
                .GetByIdAsync(Arg.Any<int>())
                .Returns(await Task.FromResult(TestData.FakeSportEvent));

            _unitOfWorkMock.AchievementsRepository
                .CreateAsync(Arg.Any<Achievement>())
                .Returns(await Task.FromResult(TestData.FakeAchievement));

            _mapperMock.Map<AchievementDto>(Arg.Any<Achievement>()).Returns(TestData.FakeAchievementDto);

            var handler = new CreateAchievementCommandHandler(_unitOfWorkMock, _mapperMock, _loggerMock);
            var command = new CreateAchievementCommand(TestData.FakeCreateAchievementDto, TestData.FakeAthleteId);

            // Act
            var actualResult = await handler.Handle(command, default);

            // Assert
            Assert.Equal(TestData.FakeAchievementDto, actualResult);
            Assert.Equal(TestData.FakeAchievementDto.PlaceFinished, actualResult.PlaceFinished);

            await _unitOfWorkMock.AchievementsRepository.Received(1)
                .CreateAsync(Arg.Is<Achievement>(ach =>
                    ach.PlaceFinished == TestData.FakeAchievement.PlaceFinished &&
                    ach.SportEventId == TestData.FakeAchievement.SportEventId));
        }

        [Fact]
        public async Task CreateAchievement_FutureSportEvent_RaisesBadRequestException()
        {
            // Arrange
            SportEvent futureSportEvent = new ()
            {
                Id = 3,
                Name = "Test sport event name",
                Sport = Domain.Enums.SportsEnum.Football,
                Country = Domain.Enums.CountryEnum.BGR,
                EventDate = new DateTime(year: 2025, month: 9, day: 20),
                Finished = true,
                Status = Domain.Enums.SportEventStatus.Approved
            };

            _unitOfWorkMock.SportEventsRepository
                .GetByIdAsync(Arg.Any<int>())
                .Returns(await Task.FromResult(futureSportEvent));

            var handler = new CreateAchievementCommandHandler(_unitOfWorkMock, _mapperMock, _loggerMock);
            var command = new CreateAchievementCommand(TestData.FakeCreateAchievementDto, TestData.FakeAthleteId);

            // Act & Assert
            await Assert.ThrowsAsync<BadRequestException>(() => handler.Handle(command, default));
        }
    }
}