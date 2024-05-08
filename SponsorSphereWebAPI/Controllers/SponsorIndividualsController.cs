using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SponsorSphere.Application.App.SponsorIndividuals.Commands;
using SponsorSphere.Application.App.SponsorIndividuals.Queries;
using SponsorSphere.Application.App.SponsorIndividuals.Responses;
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
        public async Task<IActionResult> GetAllSponsorIndividuals(int pageNumber, int pageSize)
        {
            var resultList = await _mediator.Send(new GetAllSponsorIndividualsQuery(pageNumber, pageSize));

            return Ok(resultList);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetSponsorIndividualById(int id)
        {
            try
            {
                var result = await _mediator.Send(new GetSponsorIndividualByIdQuery(id));
                return Ok(result);
            }
            catch (ApplicationException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        [Route("country")]
        public async Task<IActionResult> GetSponsorIndividualsByCountry(CountryEnum country)
        {
            var resultList = await _mediator.Send(new GetSponsorIndividualsByCountryQuery(country));

            return Ok(resultList);
        }

        [HttpGet]
        [Route("age")]
        public async Task<IActionResult> GetSponsorIndividualsByAge(int age)
        {
            var resultList = await _mediator.Send(new GetSponsorIndividualsByAgeQuery(age));

            return Ok(resultList);
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> RegisterSponsorIndividual([FromForm] RegisterSponsorIndividualDto model)
        {
            try
            {
                var result = await _mediator.Send(new CreateSponsorIndividualCommand(model));
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

            try
            {
                var result = await _mediator.Send(new UpdateSponsorIndividualCommand(updatedSponsorIndividual));
                return Accepted(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
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
