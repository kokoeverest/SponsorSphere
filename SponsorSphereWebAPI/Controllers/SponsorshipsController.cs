using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SponsorSphere.Domain.Models;

namespace SponsorSphereWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SponsorshipsController : ControllerBase
    {
        private readonly UserManager<User> userManager;


        public SponsorshipsController(UserManager<User> userManager)
        {
            this.userManager = userManager;
        }
    }
}
