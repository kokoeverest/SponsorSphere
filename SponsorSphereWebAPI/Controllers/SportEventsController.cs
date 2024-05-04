using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SponsorSphere.Application.App.SportEvents.Commands;
using SponsorSphere.Application.App.SportEvents.Queries;
using SponsorSphere.Application.App.SportEvents.Responses;
using SponsorSphere.Application.Interfaces;
using SponsorSphere.Domain.Models;
using SponsorSphereWebAPI.RequestModels.SportEvents;

namespace SponsorSphereWebAPI.Controllers
{
    [ApiController]
    [Route("sportEvents/")]
    public class SportEventsController : ControllerBase
    {

        private readonly UserManager<User> _userManager;
        private readonly IMediator _mediator;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<AchievementsController> _logger;

        public SportEventsController(UserManager<User> userManager, IMediator mediator, IUnitOfWork unitOfWork, ILogger<AchievementsController> logger)
        {
            _userManager = userManager;
            _mediator = mediator;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetSportEventById(int id)
        {
            var sportEvents = await _mediator.Send(new GetSportEventByIdQuery(id));

            return Ok(sportEvents);
        }

        [Authorize(Roles = RoleConstants.Athlete)]
        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateSportEvent([FromForm] CreateSportEventRequestModel model)
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

            var sportEvent = new SportEvent
            {
                Name = model.Name,
                Country = model.Country,
                EventDate = DateTime.Parse(model.EventDate).ToUniversalTime(),
                EventType = model.EventType,
                Sport = model.Sport
            };

            try
            {
                await _mediator.Send(new CreateSportEventCommand(sportEvent));
                return Created();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [Authorize(Roles = RoleConstants.Admin)]
        [HttpDelete]
        [Route("delete")]
        public async Task<IActionResult> DeleteSportEvent(int sportEventId)
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
                var existingSportEvent = await _mediator.Send(new GetSportEventByIdQuery(sportEventId));
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }

            try
            {
                await _unitOfWork.BeginTransactionAsync();

                await _mediator.Send(new DeleteSportEventCommand(sportEventId));

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
        public async Task<IActionResult> UpdateSportEvent([FromForm] SportEventDto updatedSportEvent)
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
                var result = await _mediator.Send(new UpdateSportEventCommand(updatedSportEvent));
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
