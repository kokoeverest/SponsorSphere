using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SponsorSphere.Application.App.Goals.Commands;
using SponsorSphere.Application.App.Goals.Responses;
using SponsorSphere.Application.App.SportEvents.Queries;
using SponsorSphere.Application.Interfaces;
using SponsorSphere.Domain.Models;
using SponsorSphereWebAPI.Filters;
using SponsorSphereWebAPI.RequestModels.Goals;

namespace SponsorSphereWebAPI.Controllers
{
    [ApiController]
    [Route("goals/")]
    public class GoalsController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly IMediator _mediator;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<AchievementsController> _logger;

        public GoalsController(UserManager<User> userManager, IMediator mediator, IUnitOfWork unitOfWork, ILogger<AchievementsController> logger)
        {
            _userManager = userManager;
            _mediator = mediator;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        [Authorize(Roles = RoleConstants.Athlete)]
        [HttpPost]
        [Route("create")]
        [ValidateModel]
        public async Task<IActionResult> CreateGoal([FromForm] CreateGoalRequestModel model)
        {
            var user = HttpContext.User?.Identity?.Name ?? string.Empty;
            var loggedInUser = await _userManager.FindByEmailAsync(user);

            if (user is null)
            {
                return NotFound("User not found!");
            }

            if (loggedInUser is null)
            {
                return Unauthorized("You have to log in first!");
            }

            if (DateTime.UtcNow > DateTime.Parse(model.EventDate).ToUniversalTime())
            {
                throw new InvalidDataException("You can't create a goal in the past");
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

            var goal = new Goal
            {
                Sport = model.Sport,
                SportEventId = sportEvent.Id,
                AthleteId = loggedInUser.Id,
                Date = sportEvent.EventDate,
                AmountNeeded = model.AmountNeeded
            };

            try
            {
                await _mediator.Send(new CreateGoalCommand(sportEvent, goal));
                return Created();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }


        [Authorize(Roles = RoleConstants.Athlete)]
        [HttpDelete]
        [Route("delete")]
        public async Task<IActionResult> DeleteGoal(int sportEventId, int athleteId)
        {
            var user = HttpContext.User?.Identity?.Name ?? string.Empty;
            var loggedInUser = await _userManager.FindByEmailAsync(user);

            if (user is null)
            {
                return NotFound("User not found!");
            }

            if (loggedInUser is null)
            {
                return Unauthorized("You have to log in first!");
            }

            try
            {
                await _unitOfWork.BeginTransactionAsync();

                await _mediator.Send(new DeleteGoalCommand(sportEventId, athleteId));

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
        public async Task<IActionResult> UpdateGoal([FromForm] GoalDto updatedGoal)
        {
            var user = HttpContext.User?.Identity?.Name ?? string.Empty;
            var loggedInUser = await _userManager.FindByEmailAsync(user);

            if (user is null)
            {
                return NotFound("User not found!");
            }

            if (loggedInUser is null)
            {
                return Unauthorized("You have to log in first!");
            }

            var sportEvent = await _mediator.Send(new GetSportEventByIdQuery(updatedGoal.SportEventId));

            if (sportEvent is null)
            {
                return NotFound("Sport event not found. You should create it first.");
            }

            if (DateTime.UtcNow > updatedGoal.Date.ToUniversalTime())
            {
                return BadRequest("You can't create a goal in the past");
            }

            try
            {
                await _unitOfWork.BeginTransactionAsync();
                var result = await _mediator.Send(new UpdateGoalCommand(updatedGoal));
                await _unitOfWork.CommitTransactionAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackTransactionAsync();
                return StatusCode(500, ex.Message);
            }
        }
    }
}