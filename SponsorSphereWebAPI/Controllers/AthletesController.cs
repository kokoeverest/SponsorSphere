using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SponsorSphere.Application.App.Athletes.Commands;
using SponsorSphere.Application.App.Athletes.Queries;
using SponsorSphere.Application.App.Athletes.Responses;
using SponsorSphere.Application.Interfaces;
using SponsorSphere.Domain.Enums;
using SponsorSphere.Domain.Models;
using SponsorSphereWebAPI.RequestModels.Athletes;

namespace SponsorSphereWebAPI.Controllers
{
    [ApiController]
    [Route("users/")]
    public class AthletesController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly IMediator _mediator;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AthletesController(UserManager<User> userManager, IMediator mediator, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _userManager = userManager;
            _mediator = mediator;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("athletes")]
        public async Task<IActionResult> GetAllAthletes(int pageNumber, int pageSize)
        {
            var resultList = await _mediator.Send(new GetAllAthletesQuery(pageNumber, pageSize));

            return Ok(resultList);
        }

        [HttpGet]
        [Route("athletes/{id}")]
        public async Task<IActionResult> GetAthleteById(int id)
        {
            try
            {
                var result = await _mediator.Send(new GetAthleteByIdQuery(id));
                return Ok(result);
            }
            catch (ApplicationException ex)
            {
                return NotFound(ex.Message);
            }
            //catch (Exception)
            //{
            //    return HttpRequestException(HttpStatusCode.InternalServerError);
            //}
        }

        [HttpGet]
        [Route("athletes/country")]
        public async Task<IActionResult> GetAthletesByCountry(CountryEnum country)
        {
            var resultList = await _mediator.Send(new GetAthletesByCountryQuery(country));

            return Ok(resultList);
        }

        [HttpGet]
        [Route("athletes/sport")]
        public async Task<IActionResult> GetAthletesBySport(SportsEnum sport)
        {
            var resultList = await _mediator.Send(new GetAthletesBySportQuery(sport));

            return Ok(resultList);
        }

        [HttpGet]
        [Route("athletes/age")]
        public async Task<IActionResult> GetAthletesByAge(int age)
        {
            var resultList = await _mediator.Send(new GetAthletesByAgeQuery(age));

            return Ok(resultList);
        }

        [HttpGet]
        [Route("athletes/amount")]
        public async Task<IActionResult> GetAthletesByAmount()
        {
            var resultList = await _mediator.Send(new GetAthletesByAmountSponsoredQuery());

            return Ok(resultList);
        }

        [HttpPost]
        [Route("athletes/register")]
        public async Task<IActionResult> RegisterAthlete([FromForm] RegisterAthleteRequestModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Cannot create profile without required fields! Check your input!");
            }

            var athlete = new Athlete
            {
                Name = model.Name,
                UserName = model.Email,
                LastName = model.LastName,
                Email = model.Email,
                Country = model.Country,
                PhoneNumber = model.PhoneNumber,
                BirthDate = DateTime.Parse(model.BirthDate).ToUniversalTime(),
                Sport = model.Sport
            };

            try
            {
                await _unitOfWork.BeginTransactionAsync();
                // Phone validation not implemented

                var newAthlete = await _userManager.CreateAsync(athlete, model.Password);

                var result = await _userManager.AddToRoleAsync(athlete, RoleConstants.Athlete);

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
        [Route("athletes/update")]
        public async Task<IActionResult> UpdateAthlete([FromForm] AthleteDto updatedAthlete)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();

                var athlete = HttpContext.User?.Identity?.Name ?? string.Empty;
                var loggedInUser = await _userManager.FindByEmailAsync(athlete);

                if (athlete is null || loggedInUser is null)
                {
                    throw new InvalidDataException("User not found");
                }

                var result = await _mediator.Send(new UpdateAthleteCommand(updatedAthlete));

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
        [Route("athletes/delete")]
        public async Task<IActionResult> DeleteAthlete()
        {
            var athlete = HttpContext.User?.Identity?.Name ?? string.Empty;
            var loggedInUser = await _userManager.FindByEmailAsync(athlete);

            if (loggedInUser is null)
            {
                return NotFound();
            }

            await _mediator.Send(new DeleteAthleteCommand(loggedInUser.Id));
            return NoContent();
        }
    }
}
