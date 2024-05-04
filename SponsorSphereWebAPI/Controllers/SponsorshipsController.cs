using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SponsorSphere.Application.App.Sponsorships.Commands;
using SponsorSphere.Application.App.Sponsorships.Queries;
using SponsorSphere.Application.App.Sponsorships.Responses;
using SponsorSphere.Application.Interfaces;
using SponsorSphere.Domain.Enums;
using SponsorSphere.Domain.Models;
using SponsorSphereWebAPI.RequestModels.Sponsorships;

namespace SponsorSphereWebAPI.Controllers
{
    [ApiController]
    [Route("sponsorships/")]
    public class SponsorshipsController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly IMediator _mediator;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<AchievementsController> _logger;

        public SponsorshipsController(UserManager<User> userManager, IMediator mediator, IUnitOfWork unitOfWork, ILogger<AchievementsController> logger)
        {
            _userManager = userManager;
            _mediator = mediator;
            _unitOfWork = unitOfWork;
            _logger = logger;
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
        public async Task<IActionResult> CreateSponsorship([FromForm] CreateSponsorshipRequestModel model)
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

            var sponsorship = new Sponsorship
            {
                AthleteId = model.AthleteId,
                SponsorId = loggedInUser.Id,
                Level = model.Level,
                Amount = model.Amount
            };

            try
            {
                // Add a check - if the sponsorship level is Single payment => check if the athlete has a
                // Goal and if he has => reduce the AmountNeeded of the Goal with the sponsorship amount
                await _mediator.Send(new CreateSponsorshipCommand(sponsorship));
                return Created();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
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

            try
            {
                var existingSponsorship = await _mediator.Send(new GetSponsorshipQuery(athleteId, loggedInUser.Id));

                if (loggedInUser.Id != existingSponsorship.SponsorId)
                {
                    return Unauthorized("You are not authorised to do this!");
                }
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }

            try
            {
                await _unitOfWork.BeginTransactionAsync();

                await _mediator.Send(new DeleteSponsorshipCommand(athleteId, loggedInUser.Id));

                await _unitOfWork.CommitTransactionAsync();
                return NoContent();
            }

            catch (Exception ex)
            {
                await _unitOfWork.RollbackTransactionAsync();
                return StatusCode(500, ex.Message);
            }
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
                return Unauthorized("You are not authorised to do this!");
            }

            try
            {
                await _unitOfWork.BeginTransactionAsync();
                var result = await _mediator.Send(new UpdateSponsorshipCommand(updatedSponsorship));
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