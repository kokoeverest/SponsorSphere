using Microsoft.AspNetCore.Mvc;
using SponsorSphere.Application.App.Athletes.Dtos;
using SponsorSphere.Application.Common.Exceptions;
using SponsorSphere.Domain.Enums;
using SponsorSphere.Infrastructure;
using SponsorSphere.Infrastructure.Repositories;
using SponsorSphere.IntegrationTests.Helpers;
using SponsorSphereWebAPI.Controllers;
using System.Diagnostics.Metrics;
using System.Net;

namespace SponsorSphere.IntegrationTests.AthleteTests
{
    public class AthleteControllerTests : IClassFixture<DatabaseFixture>
    {
        private readonly SponsorSphereDbContext _context;
        private readonly AthletesController _controller;

        public AthleteControllerTests(DatabaseFixture fixture)
        {
            _context = fixture.Context;

            var achievementRepository = new AchievementsRepository(_context);
            var athleteRepository = new AthleteRepository(_context);
            var blogPostRepository = new BlogPostRepository(_context);
            var blogPostPictureRepository = new BlogPostPictureRepository(_context);
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
                blogPostPictureRepository,
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
            var requestResult = await _controller.GetAllAthletes(1, 10);

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
            var requestResult = await _controller.GetAthletesByCountry(country, 1, 10);

            // Assert
            var result = requestResult as OkObjectResult;
            var athletes = result!.Value as List<AthleteDto>;

            Assert.NotNull(result);
            Assert.Equal((int)HttpStatusCode.OK, result.StatusCode);

            Assert.NotNull(athletes);
            Assert.True(athletes.All(a => a.Country == country));
        }
    }
}