using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SponsorSphere.Application.App.SponsorIndividuals.Commands;
using SponsorSphere.Application.App.SponsorIndividuals.Dtos;
using SponsorSphere.Application.App.SponsorIndividuals.Queries;
using SponsorSphere.Domain.Enums;
using SponsorSphere.Domain.Models;

namespace SponsorSphereWebAPI.Controllers
{
    [ApiController]
    [Route("users/sponsors/individuals")]
    public class SponsorIndividualsController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly IMediator _mediator;

        public SponsorIndividualsController(UserManager<User> userManager, IMediator mediator)
        {
            _userManager = userManager;
            _mediator = mediator;
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetAllSponsorIndividuals(int pageNumber = 1, int pageSize = 10)
        {
            var resultList = await _mediator.Send(new GetAllSponsorIndividualsQuery(pageNumber, pageSize));
            return Ok(resultList);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetSponsorIndividualById(int id)
        {
            var result = await _mediator.Send(new GetSponsorIndividualByIdQuery(id));
            return Ok(result);
        }

        [HttpGet]
        [Route("country")]
        public async Task<IActionResult> GetSponsorIndividualsByCountry(CountryEnum country, int pageNumber = 1, int pageSize = 10)
        {
            var resultList = await _mediator.Send(new GetSponsorIndividualsByCountryQuery(country, pageNumber, pageSize));
            return Ok(resultList);
        }

        [HttpGet]
        [Route("age")]
        public async Task<IActionResult> GetSponsorIndividualsByAge(int age, int pageNumber = 1, int pageSize = 10)
        {
            var resultList = await _mediator.Send(new GetSponsorIndividualsByAgeQuery(age, pageNumber, pageSize));
            return Ok(resultList);
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> RegisterSponsorIndividual([FromForm] RegisterSponsorIndividualDto model)
        {
            var result = await _mediator.Send(new CreateSponsorIndividualCommand(model));
            return Created(string.Empty, result);
        }

        [Authorize(Roles = RoleConstants.Sponsor)]
        [HttpPatch]
        [Route("update")]
        public async Task<IActionResult> UpdateSponsorIndividual([FromForm] SponsorIndividualDto updatedSponsorIndividual)
        {
            var sponsorIndividual = HttpContext.User?.Identity?.Name ?? string.Empty;
            var loggedInUser = await _userManager.FindByEmailAsync(sponsorIndividual);

            if (sponsorIndividual is null)
            {
                return NotFound("User not found");
            }

            if (loggedInUser is null)
            {
                return Unauthorized("You have to log in first!");
            }

            var result = await _mediator.Send(new UpdateSponsorIndividualCommand(updatedSponsorIndividual));
            return Accepted(result);
        }

        [Authorize(Roles = RoleConstants.Admin)]
        [HttpDelete]
        [Route("delete")]
        public async Task<IActionResult> DeleteSponsorIndividual()
        {
            var sponsorIndividual = HttpContext.User?.Identity?.Name ?? string.Empty;
            var loggedInUser = await _userManager.FindByEmailAsync(sponsorIndividual);

            if (loggedInUser is null)
            {
                return NotFound("User not found");
            }

            await _mediator.Send(new DeleteSponsorIndividualCommand(loggedInUser.Id));
            return NoContent();
        }
    }
}
