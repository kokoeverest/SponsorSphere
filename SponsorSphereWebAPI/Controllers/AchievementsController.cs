using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SponsorSphere.Domain.Models;

namespace SponsorSphereWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AchievementsController : ControllerBase
    {
        private readonly UserManager<User> userManager;


        public AchievementsController(UserManager<User> userManager)
        {
            this.userManager = userManager;
        }
    }
}
