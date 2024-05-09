﻿using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SponsorSphere.Application.App.Athletes.Commands;
using SponsorSphere.Application.App.Athletes.Dtos;
using SponsorSphere.Application.App.Athletes.Queries;
using SponsorSphere.Domain.Enums;
using SponsorSphere.Domain.Models;

namespace SponsorSphereWebAPI.Controllers
{
    [ApiController]
    [Route("users/athletes/")]
    public class AthletesController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly IMediator _mediator;


        public AthletesController(UserManager<User> userManager, IMediator mediator)
        {
            _userManager = userManager;
            _mediator = mediator;
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetAllAthletes(int pageNumber = 1, int pageSize = 10)
        {
            var resultList = await _mediator.Send(new GetAllAthletesQuery(pageNumber, pageSize));
            return Ok(resultList);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetAthleteById(int id)
        {
            var result = await _mediator.Send(new GetAthleteByIdQuery(id));
            return Ok(result);
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
        [Route("achievements")]
        public async Task<IActionResult> GetAthletesByAchievements()
        {
            var resultList = await _mediator.Send(new GetAthletesByAchievementsQuery());
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
        public async Task<IActionResult> RegisterAthlete([FromForm] RegisterAthleteDto model)
        {
            var result = await _mediator.Send(new CreateAthleteCommand(model));
            return Created(string.Empty, result);
        }

        [Authorize(Roles = RoleConstants.Athlete)]
        [HttpPatch]
        [Route("update")]
        public async Task<IActionResult> UpdateAthlete([FromForm] AthleteDto updatedAthlete)
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

            var result = await _mediator.Send(new UpdateAthleteCommand(updatedAthlete));
            return Accepted(result);
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
