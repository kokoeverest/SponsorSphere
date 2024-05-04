using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SponsorSphere.Application.App.SponsorCompanies.Commands;
using SponsorSphere.Application.App.SponsorCompanies.Queries;
using SponsorSphere.Application.App.SponsorCompanies.Responses;
using SponsorSphere.Application.App.Sponsors.Queries;
using SponsorSphere.Application.Interfaces;
using SponsorSphere.Domain.Enums;
using SponsorSphere.Domain.Models;
using SponsorSphereWebAPI.RequestModels.SponsorCompanies;

namespace SponsorSphereWebAPI.Controllers
{
    [ApiController]
    [Route("users/sponsors/companies/")]
    public class SponsorCompaniesController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly IMediator _mediator;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<SponsorCompaniesController> _logger;

        public SponsorCompaniesController(UserManager<User> userManager, IMediator mediator, IUnitOfWork unitOfWork, ILogger<SponsorCompaniesController> logger)
        {
            _userManager = userManager;
            _mediator = mediator;
            _unitOfWork = unitOfWork;
            _logger = logger;
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
        public async Task<IActionResult> RegisterSponsorCompany([FromForm] RegisterSponsorCompanyRequestModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Cannot create profile without required fields! Check your input!");
            }

            var sponsorCompany = new SponsorCompany
            {
                Name = model.Name,
                UserName = model.Email,
                Email = model.Email,
                Country = model.Country,
                PhoneNumber = model.PhoneNumber,
                IBAN = model.IBAN
            };

            try
            {
                await _unitOfWork.BeginTransactionAsync();
                // Phone validation not implemented

                await _userManager.CreateAsync(sponsorCompany, model.Password);

                await _userManager.AddToRoleAsync(sponsorCompany, RoleConstants.Sponsor);

                await _unitOfWork.CommitTransactionAsync();

                return Created();
            }

            catch (Exception ex)
            {
                await _unitOfWork.RollbackTransactionAsync();
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = RoleConstants.Sponsor)]
        [HttpPatch]
        [Route("update")]
        public async Task<IActionResult> UpdateSponsorCompany([FromForm] SponsorCompanyDto updatedSponsorCompany)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();

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

                await _unitOfWork.CommitTransactionAsync();

                return Accepted(result);
            }

            catch (Exception ex)
            {
                await _unitOfWork.RollbackTransactionAsync();
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
