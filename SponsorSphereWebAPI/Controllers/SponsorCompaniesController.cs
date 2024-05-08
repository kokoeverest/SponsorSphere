using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SponsorSphere.Application.App.SponsorCompanies.Commands;
using SponsorSphere.Application.App.SponsorCompanies.Queries;
using SponsorSphere.Application.App.SponsorCompanies.Responses;
using SponsorSphere.Application.App.Sponsors.Queries;
using SponsorSphere.Domain.Enums;
using SponsorSphere.Domain.Models;

namespace SponsorSphereWebAPI.Controllers
{
    [ApiController]
    [Route("users/sponsors/companies/")]
    public class SponsorCompaniesController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly IMediator _mediator;

        public SponsorCompaniesController(UserManager<User> userManager, IMediator mediator)
        {
            _userManager = userManager;
            _mediator = mediator;
        }

        [HttpGet]
        [Route("moneyProvided")]
        public async Task<IActionResult> GetSponsorsByMoneyProvided()
        {
            var resultList = await _mediator.Send(new GetSponsorsByMoneyProvidedQuery());
            return Ok(resultList);
        }

        [HttpGet]
        [Route("mostAthletes")]
        public async Task<IActionResult> GetSponsorsByMostAthletesSponsored()
        {
            var resultList = await _mediator.Send(new GetSponsorsByMostAthletesQuery());
            return Ok(resultList);
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetAllSponsorCompanies(int pageNumber, int pageSize)
        {
            var resultList = await _mediator.Send(new GetAllSponsorCompaniesQuery(pageNumber, pageSize));

            return Ok(resultList);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetSponsorCompanyById(int id)
        {
            try
            {
                var result = await _mediator.Send(new GetSponsorCompanyByIdQuery(id));
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
        public async Task<IActionResult> GetSponsorCompaniesByCountry(CountryEnum country)
        {
            var resultList = await _mediator.Send(new GetSponsorCompaniesByCountryQuery(country));

            return Ok(resultList);
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> RegisterSponsorCompany([FromForm] RegisterSponsorCompanyDto model)
        {
            try
            {
                var result = await _mediator.Send(new CreateSponsorCompanyCommand(model));
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
        public async Task<IActionResult> UpdateSponsorCompany([FromForm] SponsorCompanyDto updatedSponsorCompany)
        {
            var sponsorCompany = HttpContext.User?.Identity?.Name ?? string.Empty;
            var loggedInUser = await _userManager.FindByEmailAsync(sponsorCompany);

            if (sponsorCompany is null)
            {
                return NotFound("User not found");
            }

            if (loggedInUser is null)
            {
                return Unauthorized("You have to log in first!");
            }

            try
            {
                var result = await _mediator.Send(new UpdateSponsorCompanyCommand(updatedSponsorCompany));
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
        public async Task<IActionResult> DeleteSponsorCompany()
        {
            var sponsorCompany = HttpContext.User?.Identity?.Name ?? string.Empty;
            var loggedInUser = await _userManager.FindByEmailAsync(sponsorCompany);

            if (loggedInUser is null)
            {
                return NotFound("User not found");
            }

            await _mediator.Send(new DeleteSponsorCompanyCommand(loggedInUser.Id));
            return NoContent();
        }
    }
}
