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
    [Route("users/athletes/")]
    public class AthletesController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly IMediator _mediator;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<AthletesController> _logger;

        public AthletesController(UserManager<User> userManager, IMediator mediator, IUnitOfWork unitOfWork, ILogger<AthletesController> logger)
        {
            _userManager = userManager;
            _mediator = mediator;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetAllAthletes(int pageNumber, int pageSize)
        {
            var resultList = await _mediator.Send(new GetAllAthletesQuery(pageNumber, pageSize));

            return Ok(resultList);
        }

        [HttpGet]
        [Route("{id}")]
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
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        [Route("country")]
        public async Task<IActionResult> GetAthletesByCountry(CountryEnum country)
        {
            var resultList = await _mediator.Send(new GetAthletesByCountryQuery(country));

            return Ok(resultList);
        }

        [HttpGet]
        [Route("sport")]
        public async Task<IActionResult> GetAthletesBySport(SportsEnum sport)
        {
            var resultList = await _mediator.Send(new GetAthletesBySportQuery(sport));

            return Ok(resultList);
        }

        [HttpGet]
        [Route("age")]
        public async Task<IActionResult> GetAthletesByAge(int age)
        {
            var resultList = await _mediator.Send(new GetAthletesByAgeQuery(age));

            return Ok(resultList);
        }

        [HttpGet]
        [Route("amount")]
        public async Task<IActionResult> GetAthletesByAmount()
        {
            var resultList = await _mediator.Send(new GetAthletesByAmountSponsoredQuery());

            return Ok(resultList);
        }

        [HttpPost]
        [Route("register")]
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

        [Authorize(Roles = RoleConstants.Athlete)]
        [HttpPatch]
        [Route("update")]
        public async Task<IActionResult> UpdateAthlete([FromForm] AthleteDto updatedAthlete)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();

                var athlete = HttpContext.User?.Identity?.Name ?? string.Empty;
                var loggedInUser = await _userManager.FindByEmailAsync(athlete);

                if (athlete is null)
                {
                    return NotFound("User not found");
                }

                if (loggedInUser is null)
                {
                    return Unauthorized("You have to log in first!");
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

        [Authorize(Roles = RoleConstants.Athlete)]
        [HttpDelete]
        [Route("delete")]
        public async Task<IActionResult> DeleteAthlete()
        {
            var athlete = HttpContext.User?.Identity?.Name ?? string.Empty;
            var loggedInUser = await _userManager.FindByEmailAsync(athlete);

            if (athlete is null)
            {
                return NotFound("User not found");
            }

            if (loggedInUser is null)
            {
                return Unauthorized("You have to log in first!");
            }

            await _mediator.Send(new DeleteAthleteCommand(loggedInUser.Id));
            return NoContent();
        }
    }
}
