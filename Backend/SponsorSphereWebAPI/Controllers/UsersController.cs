using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SponsorSphere.Domain.Models;

namespace SponsorSphereWebAPI.Controllers
{

    [ApiController]
    [Authorize]
    [Route("users/")]
    public class UsersController : ControllerBase
    {
        private readonly UserManager<User> _userManager;

        public UsersController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        [Route("info")]
        public async Task<IActionResult> GetUserInfo()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user is null)
            {
                return Unauthorized();
            }

            var roles = await _userManager.GetRolesAsync(user);
            if (roles.Count == 0)
            {
                return NotFound("No roles found for this user");
            }
            var userType = user.GetType().ToString()[28..];

            return Ok(new
            {
                Id = user.Id,
                Role = roles.First(),
                UserName = user.UserName,
                UserType = userType,
            }
            );
        }

        [HttpDelete]
        [Route("logout")]
        public IActionResult DeleteCookieData()
        {
            Response.Cookies.Delete(".AspNetCore.Identity.Application");
            return NoContent();
        }
    }
}
