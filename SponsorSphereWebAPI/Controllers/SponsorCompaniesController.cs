using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SponsorSphere.Application.App.SponsorCompanies.Commands;
using SponsorSphere.Application.App.SponsorCompanies.Dtos;
using SponsorSphere.Application.App.SponsorCompanies.Queries;
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
        public async Task<IActionResult> GetSponsorsByMoneyProvided(int pageNumber = 1, int pageSize = 10)
        {
            var resultList = await _mediator.Send(new GetSponsorsByMoneyProvidedQuery(pageNumber, pageSize));
            return Ok(resultList);
        }

        [HttpGet]
        [Route("mostAthletes")]
        public async Task<IActionResult> GetSponsorsByMostAthletesSponsored(int pageNumber = 1, int pageSize = 10)
        {
            var resultList = await _mediator.Send(new GetSponsorsByMostAthletesQuery(pageNumber, pageSize));
            return Ok(resultList);
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetAllSponsorCompanies(int pageNumber = 1, int pageSize = 10)
        {
            var resultList = await _mediator.Send(new GetAllSponsorCompaniesQuery(pageNumber, pageSize));
            return Ok(resultList);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetSponsorCompanyById(int id)
        {
            var result = await _mediator.Send(new GetSponsorCompanyByIdQuery(id));
            return Ok(result);
        }

        [HttpGet]
        [Route("country")]
        public async Task<IActionResult> GetSponsorCompaniesByCountry(CountryEnum country, int pageNumber = 1, int pageSize = 10)
        {
            var resultList = await _mediator.Send(new GetSponsorCompaniesByCountryQuery(country, pageNumber, pageSize));
            return Ok(resultList);
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> RegisterSponsorCompany([FromForm] RegisterSponsorCompanyDto model)
        {
            var result = await _mediator.Send(new CreateSponsorCompanyCommand(model));
            return Created(string.Empty, result);
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

            var result = await _mediator.Send(new UpdateSponsorCompanyCommand(updatedSponsorCompany));
            return Accepted(result);
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
