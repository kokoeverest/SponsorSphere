using Azure.Core;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SponsorSphere.Application.App.Athletes.Commands;
using SponsorSphere.Application.App.Athletes.Responses;
using SponsorSphere.Domain.Enums;
using SponsorSphere.Domain.Models;
using System.Text;

namespace SponsorSphereWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly IMediator _mediator;

        public UsersController(UserManager<User> userManager, IMediator mediator)
        {
            this._userManager = userManager;
            this._mediator = mediator;
        }

        [Authorize(Roles = RoleConstants.Athlete)]
        [HttpGet(Name = "GetListOfStrings")]
        public IActionResult Get()
        {
            Console.WriteLine("Getting list of strings");
            ICollection<string> resultList = ["123", "4567"];

            return Ok(resultList);
        }

        //[Authorize]
        [HttpPost]
        [Route("athletes/register")]
        public async Task<IActionResult> RegisterAthlete(string name, CountryEnum country, string phoneNumber, string email, string password, string lastName, SportsEnum sport, string birthDate)
        {
            //var loggedInUser = await this._userManager.FindByEmailAsync(email);
            IEnumerable<string> strings =
            [
                name,
                lastName,
                email,
                password,
                country.ToString(),
                phoneNumber,
                birthDate,
                sport.ToString()
        ];

            if (strings.Any(string.IsNullOrEmpty))
            {
                return BadRequest("Cannot create profile without required fields! Check your input!");
            }

            // Phone, Email and Password validations

            var athlete = new Athlete
            {
                Name = name,
                UserName = name,
                LastName = lastName,
                Email = email,
                //Password = string.Empty,
                Country = country,
                PhoneNumber = phoneNumber,
                BirthDate = DateTime.Parse(birthDate).ToUniversalTime(),
                Sport = sport
            };
            var res = _userManager.CheckPasswordAsync(athlete, password);
            try
            {
                var createdAthlete = await _mediator.Send(new CreateAthleteCommand(athlete));
                var registeredUser = await _userManager.AddPasswordAsync(athlete, password);

                var result = await _userManager.AddToRoleAsync(athlete, RoleConstants.Athlete);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok();
        }


        [Authorize]
        [HttpPatch]
        public async Task<IActionResult> UpdateAthlete(int id)
        {
            var athlete = this.HttpContext.User?.Identity?.Name ?? string.Empty;
            var loggedInUser = await this._userManager.FindByEmailAsync(athlete);

            if (!ModelState.IsValid || loggedInUser is null)
            {
                return BadRequest();
            }

            //loggedInUser.Country = athleteToUpdate.Country;
            var result = await this._userManager.UpdateAsync(loggedInUser);
            return Ok(result);
        }

        [Authorize]
        [HttpDelete]
        public async Task<IActionResult> DeleteAthlete(int id)
        {
            var athlete = this.HttpContext.User?.Identity?.Name ?? string.Empty;
            var loggedInUser = await this._userManager.FindByEmailAsync(athlete);

            if (loggedInUser == null)
            {
                return BadRequest();
            }

            return Ok();
        }
    }
}
