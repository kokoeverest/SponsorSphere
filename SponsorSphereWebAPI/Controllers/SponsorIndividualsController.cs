using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SponsorSphere.Application.App.SponsorIndividuals.Commands;
using SponsorSphere.Application.App.SponsorIndividuals.Queries;
using SponsorSphere.Application.App.SponsorIndividuals.Responses;
using SponsorSphere.Application.Interfaces;
using SponsorSphere.Domain.Enums;
using SponsorSphere.Domain.Models;
using SponsorSphereWebAPI.RequestModels.SponsorIndividuals;

namespace SponsorSphereWebAPI.Controllers
{
    [ApiController]
    [Route("users/sponsors")]
    public class SponsorIndividualsController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly IMediator _mediator;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public SponsorIndividualsController(UserManager<User> userManager, IMediator mediator, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _userManager = userManager;
            _mediator = mediator;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("individuals")]
        public async Task<IActionResult> GetAllSponsorIndividuals(int pageNumber, int pageSize)
        {
            var resultList = await _mediator.Send(new GetAllSponsorIndividualsQuery(pageNumber, pageSize));

            return Ok(resultList);
        }

        [HttpGet]
        [Route("individuals/{id}")]
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
        }

        [HttpGet]
        [Route("individuals/country")]
        public async Task<IActionResult> GetSponsorIndividualsByCountry(CountryEnum country)
        {
            var resultList = await _mediator.Send(new GetSponsorIndividualsByCountryQuery(country));

            return Ok(resultList);
        }

        [HttpGet]
        [Route("individuals/age")]
        public async Task<IActionResult> GetSponsorIndividualsByAge(int age)
        {
            var resultList = await _mediator.Send(new GetSponsorIndividualsByAgeQuery(age));

            return Ok(resultList);
        }

        [HttpPost]
        [Route("individuals/register")]
        public async Task<IActionResult> RegisterSponsorIndividual([FromForm] RegisterSponsorIndividualRequestModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Cannot create profile without required fields! Check your input!");
            }

            var sponsorIndividual = new SponsorIndividual
            {
                Name = model.Name,
                UserName = model.Email,
                LastName = model.LastName,
                Email = model.Email,
                Country = model.Country,
                PhoneNumber = model.PhoneNumber,
                BirthDate = DateTime.Parse(model.BirthDate).ToUniversalTime(),
            };

            try
            {
                await _unitOfWork.BeginTransactionAsync();
                // Phone validation not implemented

                await _userManager.CreateAsync(sponsorIndividual, model.Password);

                await _userManager.AddToRoleAsync(sponsorIndividual, RoleConstants.Sponsor);

                await _unitOfWork.CommitTransactionAsync();

                return Created();
            }

            catch (Exception ex)
            {
                await _unitOfWork.RollbackTransactionAsync();
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpPatch]
        [Route("individuals/update")]
        public async Task<IActionResult> UpdateSponsorIndividual([FromForm] SponsorIndividualDto updatedSponsorIndividual)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();

                var sponsorIndividual = HttpContext.User?.Identity?.Name ?? string.Empty;
                var loggedInUser = await _userManager.FindByEmailAsync(sponsorIndividual);

                if (sponsorIndividual is null || loggedInUser is null)
                {
                    throw new InvalidDataException("User not found");
                }

                var result = await _mediator.Send(new UpdateSponsorIndividualCommand(updatedSponsorIndividual));

                await _unitOfWork.CommitTransactionAsync();

                return Accepted(result);
            }

            catch (Exception ex)
            {
                await _unitOfWork.RollbackTransactionAsync();
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpDelete]
        [Route("individuals/delete")]
        public async Task<IActionResult> DeleteSponsorIndividual()
        {
            var sponsorIndividual = HttpContext.User?.Identity?.Name ?? string.Empty;
            var loggedInUser = await _userManager.FindByEmailAsync(sponsorIndividual);

            if (loggedInUser is null)
            {
                return NotFound();
            }

            await _mediator.Send(new DeleteSponsorIndividualCommand(loggedInUser.Id));
            return NoContent();
        }
    }
}
