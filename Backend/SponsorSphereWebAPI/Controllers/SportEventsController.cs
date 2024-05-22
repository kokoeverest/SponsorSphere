using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.AspNetCore.Mvc;
using SponsorSphere.Application.App.SportEvents.Commands;
using SponsorSphere.Application.App.SportEvents.Dtos;
using SponsorSphere.Application.App.SportEvents.Queries;
using SponsorSphere.Domain.Enums;
using SponsorSphere.Domain.Models;

namespace SponsorSphereWebAPI.Controllers
{
    [HttpLogging(HttpLoggingFields.All)]
    [ApiController]
    [Route("sportEvents/")]
    public class SportEventsController : ControllerBase
    {

        private readonly IMediator _mediator;

        public SportEventsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetSportEventById(int id)
        {
            var sportEvents = await _mediator.Send(new GetSportEventByIdQuery(id));
            return Ok(sportEvents);
        }

        [Authorize(Roles = RoleConstants.Athlete)]
        [HttpGet]
        [Route("finished")]
        public async Task<IActionResult> GetFinishedSportEvents(SportsEnum sport, int pageNumber = 1, int pageSize = 10)
        {
            var sportEvents = await _mediator.Send(new GetFinishedSportEventsQuery(sport, pageNumber, pageSize));
            return Ok(sportEvents);
        }

        [Authorize(Roles = RoleConstants.Athlete)]
        [HttpGet]
        [Route("unfinished")]
        public async Task<IActionResult> GetUnfinishedSportEvents(SportsEnum sport, int pageNumber = 1, int pageSize = 10)
        {
            var sportEvents = await _mediator.Send(new GetUnfinishedSportEventsQuery(sport, pageNumber, pageSize));
            return Ok(sportEvents);
        }

        [Authorize(Roles = RoleConstants.Admin)]
        [HttpGet]
        [Route("pending")]
        public async Task<IActionResult> GetPendingSportEvents(int pageNumber = 1, int pageSize = 10)
        {
            var sportEvents = await _mediator.Send(new GetPendingSportEventsQuery(pageNumber, pageSize));
            return Ok(sportEvents);
        }

        [Authorize(Roles = RoleConstants.Admin)]
        [HttpGet]
        [Route("pendingCount")]
        public async Task<IActionResult> GetPendingSportEventsCount()
        {
            var sportEventsCount = await _mediator.Send(new GetPendingSportEventsCountQuery());
            return Ok(sportEventsCount);
        }

        [Authorize(Roles = RoleConstants.Athlete)]
        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateSportEvent([FromForm] CreateSportEventDto model)
        {
            var result = await _mediator.Send(new CreateSportEventCommand(model));
            return Created(string.Empty, result);
        }

        [Authorize(Roles = RoleConstants.Admin)]
        [HttpDelete]
        [Route("delete")]
        public async Task<IActionResult> DeleteSportEvent(int sportEventId)
        {
            await _mediator.Send(new GetSportEventByIdQuery(sportEventId));

            await _mediator.Send(new DeleteSportEventCommand(sportEventId));
            return NoContent();
        }

        [Authorize(Roles = RoleConstants.Admin)]
        [HttpPatch]
        [Route("update")]
        public async Task<IActionResult> UpdateSportEvent([FromForm] SportEventDto updatedSportEvent)
        {
            var result = await _mediator.Send(new UpdateSportEventCommand(updatedSportEvent));
            return Ok(result);
        }
    }
}
