using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SponsorSphere.Application.App.Sponsorships.Commands;
using SponsorSphere.Application.App.Sponsorships.Dtos;
using SponsorSphere.Application.App.Sponsorships.Queries;
using SponsorSphere.Domain.Enums;
using SponsorSphere.Domain.Models;

namespace SponsorSphereWebAPI.Controllers
{
    [ApiController]
    [Route("sponsorships/")]
    public class SponsorshipsController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly IMediator _mediator;

        public SponsorshipsController(UserManager<User> userManager, IMediator mediator)
        {
            _userManager = userManager;
            _mediator = mediator;
        }

        [HttpGet]
        [Route("level")]
        public async Task<IActionResult> GetSponsorshipsByLevel(SponsorshipLevel level)
        {
            var sponsorships = await _mediator.Send(new GetSponsorshipsByLevelQuery(level));
            return Ok(sponsorships);
        }

        [Authorize(Roles = RoleConstants.Sponsor)]
        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateSponsorship([FromForm] CreateSponsorshipDto model)
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
            model.SponsorId = loggedInUser.Id;

            var result = await _mediator.Send(new CreateSponsorshipCommand(model));
            return Created(string.Empty, result);
        }

        [Authorize(Roles = RoleConstants.Sponsor)]
        [HttpDelete]
        [Route("delete")]
        public async Task<IActionResult> DeleteSponsorship(int athleteId)
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

            var existingSponsorship = await _mediator.Send(new GetSponsorshipQuery(athleteId, loggedInUser.Id));

            if (loggedInUser.Id != existingSponsorship.SponsorId)
            {
                return Forbid("You are not authorised to do this!");
            }

            await _mediator.Send(new DeleteSponsorshipCommand(athleteId, loggedInUser.Id));
            return NoContent();
        }

        [Authorize(Roles = RoleConstants.Sponsor)]
        [HttpPatch]
        [Route("update")]
        public async Task<IActionResult> UpdateSponsorship([FromForm] SponsorshipDto updatedSponsorship)
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

            if (loggedInUser.Id != updatedSponsorship.SponsorId)
            {
                return Forbid("You are not authorised to do this!");
            }

            var result = await _mediator.Send(new UpdateSponsorshipCommand(updatedSponsorship));
            return Ok(result);
        }
    }
}