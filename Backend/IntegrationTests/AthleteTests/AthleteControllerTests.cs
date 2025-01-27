using Microsoft.AspNetCore.Mvc;
using SponsorSphere.Application.App.Athletes.Dtos;
using SponsorSphere.Application.Common.Exceptions;
using SponsorSphere.Domain.Enums;
using SponsorSphere.Domain.Models;
using SponsorSphere.Infrastructure;
using SponsorSphere.Infrastructure.Repositories;
using SponsorSphere.IntegrationTests.Helpers;
using SponsorSphereWebAPI.Controllers;
using System.Collections;
using System.Diagnostics.Metrics;
using System.Net;

namespace SponsorSphere.IntegrationTests.AthleteTests
{
    public class AthleteControllerTests : IClassFixture<DatabaseFixture>
    {
        private readonly SponsorSphereDbContext _context;
        private readonly AthletesController _controller;

        public int DefaultPageNumber = 1;
        public int DefaultPageSize = 10;

        public AthleteControllerTests(DatabaseFixture fixture)
        {
            _context = fixture.Context;

            var achievementRepository = new AchievementsRepository(_context);
            var athleteRepository = new AthleteRepository(_context);
            var blogPostRepository = new BlogPostRepository(_context);
            var goalRepository = new GoalRepository(_context);
            var pictureRepository = new PictureRepository(_context);
            var sponsorCompanyRepository = new SponsorCompanyRepository(_context);
            var sponsorIndividualRepository = new SponsorIndividualRepository(_context);
            var sponsorRepository = new SponsorRepository(_context);
            var sponsorshipRepository = new SponsorshipRepository(_context);
            var sportEventRepository = new SportEventRepository(_context);

            var unitOfWork = new UnitOfWork(
                _context,
                athleteRepository,
                achievementRepository,
                blogPostRepository,
                goalRepository,
                pictureRepository,
                sponsorCompanyRepository,
                sponsorIndividualRepository,
                sponsorRepository,
                sponsorshipRepository,
                sportEventRepository);

            var mediator = TestHelpers.CreateMediator(unitOfWork);
            var userManager = TestHelpers.CreateUserManager();

            _controller = new AthletesController(userManager, mediator);
        }

        [Fact]
        public async Task AthletesController_GetAllAthletesFromDatabase_ReturnAllAthletes()
        {
            // Act
            var requestResult = await _controller.GetAllAthletes(DefaultPageNumber, DefaultPageSize);

            // Assert
            var result = requestResult as OkObjectResult;
            var athletes = result!.Value as List<AthleteDto>;

            Assert.NotNull(result);
            Assert.Equal((int)HttpStatusCode.OK, result.StatusCode);

            Assert.NotNull(athletes);
            Assert.Equal(2, athletes.Count);
        }

        [Fact]
        public async Task AthletesController_GetAthleteById_ReturnsAthleteDto_WithValidAthleteId()
        {
            // Arrange
            var validAthleteId = 5;

            // Act & Assert
            
            var requestResult = await _controller.GetAthleteById(validAthleteId);
            var result = requestResult as OkObjectResult;
            var athlete = result!.Value as AthleteDto;

            Assert.NotNull(result);
            Assert.Equal((int)HttpStatusCode.OK, result.StatusCode);

            Assert.NotNull(athlete);
            Assert.Equal(validAthleteId, athlete.Id);

        }

        [Fact]
        public async Task AthletesController_GetAthleteById_ThrowsNotFoundException_WithInvalidAthleteId()
        {
            // Arrange
            var invalidAthleteId = 1;

            // Act & Assert
            await Assert.ThrowsAsync<NotFoundException>(() => _controller.GetAthleteById(invalidAthleteId));
        }

        [Fact]
        public async Task AthletesController_GetAthletesByCountry_ReturnsAthletesFromCountry()
        {
            // Arrange
            var country = CountryEnum.BGR;

            // Act
            var requestResult = await _controller.GetAthletesByCountry(country, DefaultPageNumber, DefaultPageSize);

            // Assert
            var result = requestResult as OkObjectResult;
            var athletes = result!.Value as List<AthleteDto>;

            Assert.NotNull(result);
            Assert.Equal((int)HttpStatusCode.OK, result.StatusCode);

            Assert.NotNull(athletes);
            Assert.True(athletes.All(a => a.Country == country));
        }

        [Fact]
        public async Task AthletesController_GetAthletesCount_ReturnsInteger()
        {
            // Arrange & Act
            var resultCount = await _controller.GetAthletesCount();

            // Assert
            var result = resultCount as OkObjectResult;
            
            Assert.IsType<int>(result!.Value);
            Assert.Equal((int)HttpStatusCode.OK, result.StatusCode);

        }

        [Fact]
        public async Task AthletesController_GetAthletesBySport_ReturnsFilteredAthletes()
        {
            // Arrange
            var sport = SportsEnum.UltramarathonRunning;

            // Act
            var requestResult = await _controller.GetAthletesBySport(sport, DefaultPageNumber, DefaultPageSize);

            // Assert
            var result = requestResult as OkObjectResult;
            var athletes = result!.Value as List<AthleteDto>;

            Assert.NotNull(result);
            Assert.Equal((int)HttpStatusCode.OK, result.StatusCode);

            Assert.NotNull(athletes);
            Assert.True(athletes.All(a => a.Sport == sport));
        }

        [Fact]
        public async Task AthletesController_GetAthletesByAge_ReturnsAthletesFromAge()
        {
            // Arrange
            var age = 20;

            // Act
            var requestResult = await _controller.GetAthletesByAge(age, DefaultPageNumber, DefaultPageSize);

            // Assert
            var result = requestResult as OkObjectResult;
            var athletes = result!.Value as List<AthleteDto>;

            Assert.NotNull(result);
            Assert.Equal((int)HttpStatusCode.OK, result.StatusCode);

            Assert.NotNull(athletes);
            Assert.True(athletes.All(a => a.Age <= age));
        }

        [Fact]
        public async Task AthletesController_GetAthletesByAge_ReturnsEmptyCollection_IfNoResult()
        {
            // Arrange
            var tooLowAge = 10;

            // Act
            var requestResult = await _controller.GetAthletesByAge(tooLowAge, DefaultPageNumber, DefaultPageSize);

            // Assert
            var result = requestResult as OkObjectResult;
            var athletes = result!.Value as List<AthleteDto>;

            Assert.Empty(athletes!);
        }

        [Fact]
        public async Task AthletesController_GetAthletesByAchievement_ReturnsFilteredAthletes()
        {
            // Arrange & Act
            var requestResult = await _controller.GetAthletesByAchievements(DefaultPageNumber, DefaultPageSize);

            // Assert
            var result = requestResult as OkObjectResult;
            var athletes = result!.Value as List<AthleteDto>;

            Assert.NotNull(result);
            Assert.Equal((int)HttpStatusCode.OK, result.StatusCode);

            Assert.NotNull(athletes);
            Assert.NotEmpty(athletes[0].Achievements);
        }

        [Fact]
        public async Task AthletesController_GetAthletesByAmount_ReturnAthletesOrderedByAmountSponsored()
        {
            // Act
            var requestResult = await _controller.GetAthletesByAmount(DefaultPageNumber, DefaultPageSize);

            // Assert
            var result = requestResult as OkObjectResult;
            var athletes = result!.Value as List<object>;
            Assert.NotNull(result);
            Assert.Equal((int)HttpStatusCode.OK, result.StatusCode);
            Assert.NotNull(athletes);

            dynamic athleteInfo = athletes.First();
            var athleteType = athleteInfo.GetType();

            var athleteId = (int)athleteType.GetProperty("AthleteId")!.GetValue(athleteInfo)!;
            var totalAmount = (int)athleteType.GetProperty("TotalAmount")!.GetValue(athleteInfo)!;

            Assert.True(athleteId > 0);
            Assert.True(totalAmount > 0);
        }
    }
}