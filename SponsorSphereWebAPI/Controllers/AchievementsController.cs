using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SponsorSphere.Application.App.Achievements.Commands;
using SponsorSphere.Application.App.Achievements.Responses;
using SponsorSphere.Application.App.SportEvents.Queries;
using SponsorSphere.Application.Interfaces;
using SponsorSphere.Domain.Models;
using SponsorSphereWebAPI.RequestModels.Achievements;

namespace SponsorSphereWebAPI.Controllers
{
    [ApiController]
    [Route("achievements/")]
    public class AchievementsController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly IMediator _mediator;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<AchievementsController> _logger;

        public AchievementsController(UserManager<User> userManager, IMediator mediator, IUnitOfWork unitOfWork, ILogger<AchievementsController> logger)
        {
            _userManager = userManager;
            _mediator = mediator;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        [Authorize(Roles = RoleConstants.Athlete)]
        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateAchievement([FromForm] CreateAchievementRequestModel model)
        {
            var user = HttpContext.User?.Identity?.Name ?? string.Empty;
            var loggedInUser = await _userManager.FindByEmailAsync(user);

            if (user is null)
            {
                return NotFound("User not found!");
            }

            if (loggedInUser is null)
            {
                return Unauthorized("You are not authorised to do this!");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest("Please enter required fields!");
            }

            if (DateTime.UtcNow < DateTime.Parse(model.EventDate).ToUniversalTime())
            {
                return BadRequest("You can't create an achievement in the future");
            }

            var sportEvent = new SportEvent
            {
                Name = model.Name,
                Country = model.Country,
                EventDate = DateTime.Parse(model.EventDate).ToUniversalTime(),
                Finished = true,
                EventType = model.EventType,
                Sport = model.Sport
            };

            var achievement = new Achievement
            {
                Sport = model.Sport,
                SportEventId = sportEvent.Id,
                AthleteId = loggedInUser.Id,
                PlaceFinished = model.PlaceFinished
            };

            try
            {
                await _unitOfWork.BeginTransactionAsync();

                await _mediator.Send(new CreateAchievementCommand(sportEvent, achievement));

                await _unitOfWork.CommitTransactionAsync();
                return Created();
            }

            catch (Exception ex)
            {
                await _unitOfWork.RollbackTransactionAsync();
                return StatusCode(500, ex.Message);
            }

        }


        [Authorize(Roles = RoleConstants.Athlete)]
        [HttpDelete]
        [Route("delete")]
        public async Task<IActionResult> DeleteAchievement(int sportEventId, int athleteId)
        {
            var user = HttpContext.User?.Identity?.Name ?? string.Empty;
            var loggedInUser = await _userManager.FindByEmailAsync(user);

            if (user is null)
            {
                return NotFound("User not found!");
            }

            if (loggedInUser is null)
            {
                return Unauthorized("You are not authorised to do this!");
            }

            try
            {
                await _unitOfWork.BeginTransactionAsync();

                await _mediator.Send(new DeleteAchievementCommand(sportEventId, athleteId));

                await _unitOfWork.CommitTransactionAsync();
                return NoContent();
            }

            catch (Exception ex)
            {
                await _unitOfWork.RollbackTransactionAsync();
                return StatusCode(500, ex.Message);
            }

        }

        [Authorize(Roles = RoleConstants.Athlete)]
        [HttpPatch]
        [Route("update")]
        public async Task<IActionResult> UpdateAchievement([FromForm] AchievementDto updatedAchievement)
        {
            var user = HttpContext.User?.Identity?.Name ?? string.Empty;
            var loggedInUser = await _userManager.FindByEmailAsync(user);

            if (user is null)
            {
                return NotFound("User not found!");
            }

            if (loggedInUser is null)
            {
                return Unauthorized("You are not authorised to do this!");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest("The content should be at least 50 symbols!");
            }

            var sportEvent = await _mediator.Send(new GetSportEventByIdQuery(updatedAchievement.SportEventId));

            if (sportEvent is null)
            {
                return NotFound("Sport event not found. You should create it first.");
            }

            if (DateTime.UtcNow < sportEvent.EventDate.ToUniversalTime())
            {
                return BadRequest("The sport event can't be in the future");
            }

            try
            {
                await _unitOfWork.BeginTransactionAsync();
                var result = await _mediator.Send(new UpdateAchievementCommand(updatedAchievement));

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
