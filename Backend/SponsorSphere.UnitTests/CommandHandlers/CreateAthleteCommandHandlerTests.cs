using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using NSubstitute;
using SponsorSphere.Application.App.Athletes.Commands;
using SponsorSphere.Application.App.Athletes.Dtos;
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
            var fakeAthleteDto = _mapperMock.Map<AthleteDto>(TestData.FakeAthlete);
            var registerAthleteMock = Substitute.For<RegisterAthleteDto>();
            registerAthleteMock.Password = TestData.FakePassword;

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
            Assert.Equal(fakeAthleteDto, actualResult);

            await _userManagerMock.Received(1)
                .CreateAsync(Arg.Is<User>(TestData.FakeAthlete), Arg.Is<string>(TestData.FakePassword));
        }
    }
}
