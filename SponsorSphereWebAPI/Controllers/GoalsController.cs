using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SponsorSphere.Application.App.Goals.Commands;
using SponsorSphere.Application.App.Goals.Dtos;
using SponsorSphere.Application.App.SportEvents.Queries;
using SponsorSphere.Domain.Models;

namespace SponsorSphereWebAPI.Controllers
{
    [ApiController]
    [Route("goals/")]
    public class GoalsController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly IMediator _mediator;

        public GoalsController(UserManager<User> userManager, IMediator mediator)
        {
            _userManager = userManager;
            _mediator = mediator;
        }

        [Authorize(Roles = RoleConstants.Athlete)]
        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateGoal([FromForm] CreateGoalDto model)
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

            var result = await _mediator.Send(new CreateGoalCommand(model, loggedInUser.Id));
            return Created(string.Empty, result);
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

            await _mediator.Send(new DeleteGoalCommand(sportEventId, athleteId));
            return NoContent();
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

            var result = await _mediator.Send(new UpdateGoalCommand(updatedGoal));
            return Ok(result);
        }
    }
}