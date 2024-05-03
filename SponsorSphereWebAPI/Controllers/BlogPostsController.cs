using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SponsorSphere.Application.App.BlogPosts.Commands;
using SponsorSphere.Application.App.BlogPosts.Queries;
using SponsorSphere.Application.App.BlogPosts.Responses;
using SponsorSphere.Application.Interfaces;
using SponsorSphere.Domain.Models;
using SponsorSphereWebAPI.RequestModels.BlogPosts;

namespace SponsorSphereWebAPI.Controllers
{
    [ApiController]
    [Route("blogposts/")]
    public class BlogPostsController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly IMediator _mediator;
        private readonly IUnitOfWork _unitOfWork;


        public BlogPostsController(UserManager<User> userManager, IMediator mediator, IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _mediator = mediator;
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetBlogPostsById(int id)
        {
            try
            {
                var blogPost = await _mediator.Send(new GetBlogPostByIdQuery(id));
                return Ok(blogPost);
            }

            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet]
        [Route("/author/{authorId}")]
        public async Task<IActionResult> GetBlogPostsByAuthorId(int pageNumber, int pageSize, int authorId)
        {
            var blogPosts = await _mediator.Send(new GetBlogPostByAuthorIdQuery(pageNumber, pageSize, authorId));
            return Ok(blogPosts);
        }


        [HttpGet]
        [Route("latest")]
        public async Task<IActionResult> GetLatestBlogPosts()
        {
            var latestBlogPosts = await _mediator.Send(new GetLatestBlogPostsQuery());

            return Ok(latestBlogPosts);
        }

        [HttpGet]
        [Route("latest/{authorid}")]
        public async Task<IActionResult> GetLatestBlogPostsByAuthorId(int authorId)
        {
            var latestBlogPosts = await _mediator.Send(new GetLatestBlogPostsByAuthorIdQuery(authorId));

            return Ok(latestBlogPosts);
        }

        [Authorize]
        [HttpGet]
        [Route("create")]
        public async Task<IActionResult> CreateBlogPost([FromForm] CreateBlogPostRequestModel model)
        {
            var user = this.HttpContext.User?.Identity?.Name ?? string.Empty;
            var loggedInUser = await _userManager.FindByEmailAsync(user);

            if (user is null)
            {
                return NotFound("User not found!");
            }

            if (loggedInUser is null)
            {
                return Unauthorized("You are not authorised to do this!");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest("The content should be at least 50 symbols!");
            }

            var blogPost = new BlogPost
            {
                Content = model.Content,
                AuthorId = loggedInUser.Id,
                Author = loggedInUser
            };

            try
            {
                await _unitOfWork.BeginTransactionAsync();
                await _mediator.Send(new CreateBlogPostCommand(blogPost));
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }

        [Authorize]
        [HttpPatch]
        [Route("update")]
        public async Task<IActionResult> UpdateBlogPost([FromForm] BlogPostDto blogPost)
        {
            var user = this.HttpContext.User?.Identity?.Name ?? string.Empty;
            var loggedInUser = await _userManager.FindByEmailAsync(user);

            if (user is null)
            {
                return NotFound("User not found!");
            }

            if (loggedInUser is null)
            {
                return Unauthorized("You are not authorised to do this!");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest("The content should be at least 50 symbols!");
            }

            if (loggedInUser != blogPost.Author)
            {
                return Unauthorized("You are not the author of this post!");
            }

            try
            {
                await _unitOfWork.BeginTransactionAsync();
                await _mediator.Send(new UpdateBlogPostCommand(blogPost));
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [Authorize]
        [HttpDelete]
        [Route("delete")]
        public async Task<IActionResult> DeleteBlogPost(BlogPostDto blogPost)
        {
            var user = this.HttpContext.User?.Identity?.Name ?? string.Empty;
            var loggedInUser = await _userManager.FindByEmailAsync(user);

            if (user is null)
            {
                return NotFound("User not found!");
            }

            if (loggedInUser is null)
            {
                return Unauthorized("You are not authorised to do this!");
            }

            if (loggedInUser != blogPost.Author)
            {
                return Unauthorized("You are not the author of this post!");
            }

            try
            {
                await _unitOfWork.BeginTransactionAsync();
                await _mediator.Send(new DeleteBlogPostCommand(blogPost.Id));
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
