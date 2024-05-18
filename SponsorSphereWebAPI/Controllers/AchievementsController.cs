using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SponsorSphere.Application.App.Achievements.Commands;
using SponsorSphere.Application.App.Achievements.Dtos;
using SponsorSphere.Domain.Models;

namespace SponsorSphereWebAPI.Controllers
{
    [Authorize(Roles = RoleConstants.Athlete)]
    [ApiController]
    [Route("achievements/")]
    public class AchievementsController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly IMediator _mediator;

        public AchievementsController(UserManager<User> userManager, IMediator mediator)
        {
            _userManager = userManager;
            _mediator = mediator;
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateAchievement([FromForm] CreateAchievementDto model)
        {
            var user = HttpContext.User?.Identity?.Name ?? string.Empty;
            var loggedInUser = await _userManager.FindByEmailAsync(user);

            var achievement = await _mediator.Send(new CreateAchievementCommand(model, loggedInUser!.Id));
            return Created(string.Empty, achievement);
        }

        [HttpDelete]
        [Route("delete")]
        public async Task<IActionResult> DeleteAchievement(int sportEventId, int athleteId)
        {
            var user = HttpContext.User?.Identity?.Name ?? string.Empty;
            var loggedInUser = await _userManager.FindByEmailAsync(user);

            if (loggedInUser!.Id != athleteId)
            {
                return Forbid("You are not the owner of this achievement!");
            }

            await _mediator.Send(new DeleteAchievementCommand(sportEventId, athleteId));
            return NoContent();
        }

        [HttpPatch]
        [Route("update")]
        public async Task<IActionResult> UpdateAchievement([FromForm] AchievementDto updatedAchievement)
        {
            var user = HttpContext.User?.Identity?.Name ?? string.Empty;
            var loggedInUser = await _userManager.FindByEmailAsync(user);

            if (loggedInUser!.Id != updatedAchievement.AthleteId)
            {
                return Forbid("You are not the owner of this achievement!");
            }

            var result = await _mediator.Send(new UpdateAchievementCommand(updatedAchievement));
            return Ok(result);
        }
    }
}
