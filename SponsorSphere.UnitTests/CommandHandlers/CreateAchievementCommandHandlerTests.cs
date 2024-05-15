using AutoMapper;
using Castle.Core.Logging;
using Microsoft.Extensions.Logging;
using NSubstitute;
using SponsorSphere.Application.App.Achievements.Commands;
using SponsorSphere.Application.App.Achievements.Dtos;
using SponsorSphere.Application.Interfaces;
using SponsorSphere.Domain.Models;

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
            var athleteId = 2;
            var sportEvent = new SportEvent
            {
                Id = 3,
                Name = "Test sport event name",
                Sport = Domain.Enums.SportsEnum.Football,
                Country = Domain.Enums.CountryEnum.BGR,
                Finished = true,
                Status = Domain.Enums.SportEventStatus.Approved
            };


            _unitOfWorkMock.SportEventsRepository
                .GetByIdAsync(Arg.Any<int>())
                .Returns(Task.FromResult(sportEvent));

            var requestDto = new CreateAchievementDto
            {
                SportEventId = sportEvent.Id,
                PlaceFinished = 1
            };
            var expectedAchievement = new Achievement
            {
                Sport = sportEvent.Sport,
                SportEventId = sportEvent.Id,
                AthleteId = athleteId
            };

            var achievement = new Achievement
            {
                Sport = sportEvent.Sport,
                SportEventId = sportEvent.Id,
                AthleteId = athleteId,
                PlaceFinished = requestDto.PlaceFinished
            };

            _unitOfWorkMock.AchievementsRepository
                .CreateAsync(Arg.Any<Achievement>())
                .Returns(Task.FromResult(expectedAchievement));

            var handler = new CreateAchievementCommandHandler(_unitOfWorkMock, _mapperMock, _loggerMock);
            var command = new CreateAchievementCommand(requestDto, athleteId);

            // Act
            var actualResult = await handler.Handle(command, default);

            // Assert
            await _unitOfWorkMock.Received(1).SaveAsync();

            await _unitOfWorkMock.AchievementsRepository.Received(1)
                .CreateAsync(Arg.Is<Achievement>(ach =>
                    ach.PlaceFinished == expectedAchievement.PlaceFinished &&
                    ach.SportEventId == expectedAchievement.SportEventId));

            Assert.Equal(expectedAchievement.Sport, actualResult.Sport);
            Assert.Equal(requestDto.PlaceFinished, actualResult.PlaceFinished);
        }
    }
}