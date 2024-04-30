using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SponsorSphere.Domain.Models;

namespace SponsorSphereWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GoalsController : ControllerBase
    {
        private readonly UserManager<User> userManager;


        public GoalsController(UserManager<User> userManager)
        {
            this.userManager = userManager;
        }
    }
}
