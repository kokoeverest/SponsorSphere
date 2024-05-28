using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using NSubstitute;
using SponsorSphere.Application.App.Athletes.Commands;
using SponsorSphere.Application.App.Athletes.Dtos;
using SponsorSphere.Application.Common.Exceptions;
using SponsorSphere.Application.Interfaces;
using SponsorSphere.Domain.Models;
using SponsorSphere.UnitTests.Helpers;

namespace SponsorSphere.UnitTests.CommandHandlers
{
    public class CreateAthleteCommandHandlerTests
    {
        private readonly UserManager<User> _userManagerMock;
        private readonly IUnitOfWork _unitOfWorkMock;
        private readonly IMapper _mapperMock;
        private readonly ILogger<CreateAthleteCommandHandler> _loggerMock;

        public CreateAthleteCommandHandlerTests()
        {
            var store = Substitute.For<IUserStore<User>>();
            _userManagerMock = Substitute.For<UserManager<User>>(store, null, null, null, null, null, null, null, null);
            _unitOfWorkMock = Substitute.For<IUnitOfWork>();
            _mapperMock = Substitute.For<IMapper>();
            _loggerMock = Substitute.For<ILogger<CreateAthleteCommandHandler>>();
        }

        [Fact]
        public async Task CreateAthlete_ValidCommand_ShouldCreateAthlete()
        {
            // Arrange
            var fakeAthleteDto = Substitute.For<AthleteDto>();
            var registerAthleteMock = Substitute.For<RegisterAthleteDto>();

            _mapperMock.Map<AthleteDto>(Arg.Any<Athlete>()).Returns(fakeAthleteDto);

            _userManagerMock.CreateAsync(Arg.Any<User>(), Arg.Any<string>())
                .Returns(IdentityResult.Success);

            _userManagerMock.AddToRoleAsync(Arg.Any<User>(), Arg.Any<string>())
                .Returns(IdentityResult.Success);

            var handler = new CreateAthleteCommandHandler(_userManagerMock, _unitOfWorkMock, _mapperMock, _loggerMock);
            var command = new CreateAthleteCommand(registerAthleteMock);

            // Act
            var actualResult = await handler.Handle(command, default);

            // Assert
            Assert.Equal(fakeAthleteDto, actualResult);
        }

        [Fact]
        public async Task CreateAthlete_InvalidCommand_ShouldThrowInvalidDataException()
        {
            // Arrange
            var registerAthleteMock = Substitute.For<RegisterAthleteDto>();
            var error = new IdentityError { Description = "Error" };

            _userManagerMock.CreateAsync(Arg.Any<User>(), Arg.Any<string>())
                .Returns(IdentityResult.Failed(error));

            _userManagerMock.AddToRoleAsync(Arg.Any<User>(), Arg.Any<string>())
                .Returns(IdentityResult.Failed(error));

            var handler = new CreateAthleteCommandHandler(_userManagerMock, _unitOfWorkMock, _mapperMock, _loggerMock);
            var command = new CreateAthleteCommand(registerAthleteMock);

            // Act & Assert
            await Assert.ThrowsAsync<InvalidDataException>(() => handler.Handle(command, default));

        }
    }
}
