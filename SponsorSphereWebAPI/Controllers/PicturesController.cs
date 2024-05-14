using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SponsorSphere.Application.App.Pictures.Commands;
using SponsorSphere.Application.App.Pictures.Dtos;
using SponsorSphere.Domain.Models;

namespace SponsorSphereWebAPI.Controllers
{
    [ApiController]
    [Route("pictures/")]
    public class PicturesController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly IMediator _mediator;

        public PicturesController(UserManager<User> userManager, IMediator mediator)
        {
            _userManager = userManager;
            _mediator = mediator;
        }

        [Authorize]
        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreatePicture([FromForm] CreatePictureDto model)
        {
            var result = await _mediator.Send(new CreatePictureCommand(model));
            return Created(string.Empty, result);
        }

        [Authorize(Roles = RoleConstants.Admin)]
        [HttpDelete]
        [Route("delete")]
        public async Task<IActionResult> DeletePicture(PictureDto picture)
        {
            await _mediator.Send(new DeletePictureCommand(picture));
            return NoContent();
        }

        [Authorize]
        [HttpPatch]
        [Route("update")]
        public async Task<IActionResult> UpdatePicture([FromForm] PictureDto updatedPicture)
        {
            var result = await _mediator.Send(new UpdatePictureCommand(updatedPicture));
            return Ok(result);
        }
    }
}