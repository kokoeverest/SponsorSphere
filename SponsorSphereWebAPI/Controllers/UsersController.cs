using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SponsorSphere.Application.App.Athletes.Responses;
using SponsorSphere.Domain.Models;

namespace SponsorSphereWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly UserManager<User> userManager;


        public UsersController(UserManager<User> userManager)
        {
            this.userManager = userManager;
        }

        [Authorize(Roles = RoleConstants.Athlete)]
        [HttpGet(Name = "GetListOfStrings")]
        public IActionResult Get()
        {
            Console.WriteLine("Getting list of strings");
            ICollection<string> resultList = ["123", "4567"];

            return Ok(resultList);
        }

        [Authorize]
        [HttpPost]
        [Route("/register")]
        public async Task<IActionResult> RegisterAsAthlete()
        {

            //var test = this.userManager.CreateAsync();
            var athlete = this.HttpContext.User?.Identity?.Name ?? string.Empty;
            var loggedInUser = await this.userManager.FindByEmailAsync(athlete);

            if (loggedInUser == null)
            {
                return BadRequest();
            }
            var result = await this.userManager.AddToRoleAsync(loggedInUser, RoleConstants.Athlete);

            return Ok(result);
        }


        [Authorize]
        [HttpPatch]
        public async Task<IActionResult> UpdateAthlete(int id)
        {
            var athlete = this.HttpContext.User?.Identity?.Name ?? string.Empty;
            var loggedInUser = await this.userManager.FindByEmailAsync(athlete);

            if (!ModelState.IsValid || loggedInUser is null)
            {
                return BadRequest();
            }

            //loggedInUser.Country = athleteToUpdate.Country;
            var result = await this.userManager.UpdateAsync(loggedInUser);
            return Ok(result);
        }

        [Authorize]
        [HttpDelete]
        public async Task<IActionResult> DeleteAthlete(int id)
        {
            var athlete = this.HttpContext.User?.Identity?.Name ?? string.Empty;
            var loggedInUser = await this.userManager.FindByEmailAsync(athlete);

            if (loggedInUser == null)
            {
                return BadRequest();
            }

            return Ok();
        }
    }
}
