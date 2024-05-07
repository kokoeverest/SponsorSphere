using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SponsorSphere.Application.App.BlogPosts.Commands;
using SponsorSphere.Application.App.BlogPosts.Queries;
using SponsorSphere.Application.App.BlogPosts.Responses;
using SponsorSphere.Application.Interfaces;
using SponsorSphere.Domain.Models;
using SponsorSphereWebAPI.Filters;
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
        private readonly ILogger<BlogPostsController> _logger;


        public BlogPostsController(UserManager<User> userManager, IMediator mediator, IUnitOfWork unitOfWork, ILogger<BlogPostsController> logger)
        {
            _userManager = userManager;
            _mediator = mediator;
            _unitOfWork = unitOfWork;
            _logger = logger;
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
        [Route("author/{authorId}")]
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
        [Route("latest/{authorId}")]
        public async Task<IActionResult> GetLatestBlogPostsByAuthorId(int authorId)
        {
            var latestBlogPosts = await _mediator.Send(new GetLatestBlogPostsByAuthorIdQuery(authorId));

            return Ok(latestBlogPosts);
        }

        [Authorize]
        [HttpPost]
        [Route("create")]
        [ValidateModel]
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
                return Unauthorized("You have to log in first!");
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
        [ValidateModel]
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
                return Unauthorized("You have to log in first!");
            }

            if (loggedInUser.Id != blogPost.AuthorId)
            {
                return Unauthorized("You are not the author of this post!");
            }

            try
            {
                await _unitOfWork.BeginTransactionAsync();
                await _mediator.Send(new UpdateBlogPostCommand(blogPost));
                await _unitOfWork.CommitTransactionAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackTransactionAsync();
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
                return Unauthorized("You have to log in first!");
            }

            if (loggedInUser.Id != blogPost.AuthorId)
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
