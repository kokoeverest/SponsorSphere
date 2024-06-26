﻿using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SponsorSphere.Application.App.Achievements.Queries;
using SponsorSphere.Application.App.Goals.Commands;
using SponsorSphere.Application.App.Goals.Dtos;
using SponsorSphere.Application.App.Goals.Queries;
using SponsorSphere.Application.App.SportEvents.Queries;
using SponsorSphere.Domain.Models;

namespace SponsorSphereWebAPI.Controllers
{
    [Authorize(Roles = RoleConstants.Athlete)]
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

        [HttpGet]
        [Route("{athleteId}")]
        public async Task<IActionResult> GetAthleteGoals(int athleteId, int pageNumber = 1, int pageSize = 10)
        {
            var resultList = await _mediator.Send(new GetGoalsByAthleteIdQuery(athleteId, pageNumber, pageSize));
            return Ok(resultList);
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateGoal([FromForm] CreateGoalDto model)
        {
            var user = HttpContext.User?.Identity?.Name ?? string.Empty;
            var loggedInUser = await _userManager.FindByEmailAsync(user);

            var result = await _mediator.Send(new CreateGoalCommand(model, loggedInUser!.Id));
            return Created(string.Empty, result);
        }

        [HttpDelete]
        [Route("delete")]
        public async Task<IActionResult> DeleteGoal(int sportEventId, int athleteId)
        {
            var user = HttpContext.User?.Identity?.Name ?? string.Empty;
            var loggedInUser = await _userManager.FindByEmailAsync(user);

            if (loggedInUser!.Id != athleteId)
            {
                return Forbid("You are not the owner of this goal!");
            }

            await _mediator.Send(new DeleteGoalCommand(sportEventId, athleteId));
            return NoContent();
        }

        [HttpPatch]
        [Route("update")]
        public async Task<IActionResult> UpdateGoal([FromForm] GoalDto updatedGoal)
        {
            var user = HttpContext.User?.Identity?.Name ?? string.Empty;
            var loggedInUser = await _userManager.FindByEmailAsync(user);

            if (loggedInUser!.Id != updatedGoal.AthleteId)
            {
                return Forbid("You are not the owner of this goal!");
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