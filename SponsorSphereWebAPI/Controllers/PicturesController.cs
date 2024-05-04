using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SponsorSphere.Application.App.Pictures.Commands;
using SponsorSphere.Application.App.Pictures.Responses;
using SponsorSphere.Application.Interfaces;
using SponsorSphere.Domain.Models;
using SponsorSphereWebAPI.RequestModels.Pictures;

namespace SponsorSphereWebAPI.Controllers
{
    [ApiController]
    [Route("pictures/")]
    public class PicturesController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly IMediator _mediator;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<PicturesController> _logger;

        public PicturesController(UserManager<User> userManager, IMediator mediator, IUnitOfWork unitOfWork, ILogger<PicturesController> logger)
        {
            _userManager = userManager;
            _mediator = mediator;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        [Authorize]
        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreatePicture([FromForm] CreatePictureRequestModel model)
        {
            var user = HttpContext.User?.Identity?.Name ?? string.Empty;
            var loggedInUser = await _userManager.FindByEmailAsync(user);

            if (user is null)
            {
                return NotFound("User not found!");
            }

            if (loggedInUser is null)
            {
                return Unauthorized("You have to log in first!");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest("Please enter required fields!");
            }

            var picture = new Picture
            {
                Url = model.Url,
                Content = model.Content,
                Modified = DateTime.UtcNow
            };

            try
            {
                await _mediator.Send(new CreatePictureCommand(picture));
                return Created();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [Authorize(Roles = RoleConstants.Admin)]
        [HttpDelete]
        [Route("delete")]
        public async Task<IActionResult> DeletePicture(PictureDto picture)
        {
            var user = HttpContext.User?.Identity?.Name ?? string.Empty;
            var loggedInUser = await _userManager.FindByEmailAsync(user);

            if (user is null)
            {
                return NotFound("User not found!");
            }

            if (loggedInUser is null)
            {
                return Unauthorized("You have to log in first!");
            }

            try
            {
                await _unitOfWork.BeginTransactionAsync();

                await _mediator.Send(new DeletePictureCommand(picture));

                await _unitOfWork.CommitTransactionAsync();
                return NoContent();
            }

            catch (Exception ex)
            {
                await _unitOfWork.RollbackTransactionAsync();
                return StatusCode(500, ex.Message);
            }
        }

        [Authorize]
        [HttpPatch]
        [Route("update")]
        public async Task<IActionResult> UpdatePicture([FromForm] PictureDto updatedPicture)
        {
            var user = HttpContext.User?.Identity?.Name ?? string.Empty;
            var loggedInUser = await _userManager.FindByEmailAsync(user);

            if (user is null)
            {
                return NotFound("User not found!");
            }

            if (loggedInUser is null)
            {
                return Unauthorized("You have to log in first!");
            }

            try
            {
                await _unitOfWork.BeginTransactionAsync();
                var result = await _mediator.Send(new UpdatePictureCommand(updatedPicture));
                await _unitOfWork.CommitTransactionAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackTransactionAsync();
                return StatusCode(500, ex.Message);
            }
        }
    }
}