using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SponsorSphere.Application.App.Goals.Commands;
using SponsorSphere.Application.App.Goals.Responses;
using SponsorSphere.Application.App.SportEvents.Queries;
using SponsorSphere.Domain.Models;
using SponsorSphereWebAPI.Filters;

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
        [ValidateModel]
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

            try
            {
                var result = await _mediator.Send(new CreateGoalCommand(model, loggedInUser.Id));
                return Created(string.Empty, result);
            }

            catch (InvalidDataException ex)
            {
                return BadRequest(ex.Message);
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
                await _mediator.Send(new DeleteGoalCommand(sportEventId, athleteId));
                return NoContent();
            }

            catch (Exception ex)
            {
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

            try
            {
                var result = await _mediator.Send(new UpdateGoalCommand(updatedGoal));
                return Ok(result);
            }

            catch (InvalidDataException exc)
            {
                return BadRequest(exc.Message);
            }

            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}