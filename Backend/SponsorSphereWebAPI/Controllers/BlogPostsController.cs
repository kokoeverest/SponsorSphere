using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SponsorSphere.Application.App.BlogPosts.Commands;
using SponsorSphere.Application.App.BlogPosts.Dtos;
using SponsorSphere.Application.App.BlogPosts.Queries;
using SponsorSphere.Domain.Models;

namespace SponsorSphereWebAPI.Controllers
{
    [ApiController]
    [Route("blogposts/")]
    public class BlogPostsController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly IMediator _mediator;

        public BlogPostsController(UserManager<User> userManager, IMediator mediator)
        {
            _userManager = userManager;
            _mediator = mediator;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetBlogPostById(int id)
        {
            var blogPost = await _mediator.Send(new GetBlogPostByIdQuery(id));
            return Ok(blogPost);
        }

        [HttpGet]
        [Route("author/{authorId}")]
        public async Task<IActionResult> GetBlogPostsByAuthorId(int authorId, int pageNumber = 1, int pageSize = 10)
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
        public async Task<IActionResult> CreateBlogPost([FromForm] CreateBlogPostDto model)
        {
            var result = await _mediator.Send(new CreateBlogPostCommand(model));
            return Created(string.Empty, result);
        }

        [Authorize]
        [HttpPatch]
        [Route("update")]
        public async Task<IActionResult> UpdateBlogPost([FromForm] BlogPostDto blogPost)
        {
            var user = HttpContext.User?.Identity?.Name ?? string.Empty;
            var loggedInUser = await _userManager.FindByEmailAsync(user);

            if (loggedInUser!.Id != blogPost.AuthorId)
            {
                return Forbid("You are not the author of this post!");
            }

            var result = await _mediator.Send(new UpdateBlogPostCommand(blogPost));
            return Ok(result);
        }

        [Authorize]
        [HttpDelete]
        [Route("delete")]
        public async Task<IActionResult> DeleteBlogPost(BlogPostDto blogPost)
        {
            var user = HttpContext.User?.Identity?.Name ?? string.Empty;
            var loggedInUser = await _userManager.FindByEmailAsync(user);

            if (loggedInUser!.Id != blogPost.AuthorId)
            {
                return Forbid("You are not the author of this post!");
            }

            await _mediator.Send(new DeleteBlogPostCommand(blogPost.Id));
            return NoContent();
        }
    }
}
