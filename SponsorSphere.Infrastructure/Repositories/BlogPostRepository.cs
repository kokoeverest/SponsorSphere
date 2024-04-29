using Microsoft.EntityFrameworkCore;
using SponsorSphere.Application.App.BlogPosts.Responses;
using SponsorSphere.Application.Interfaces;
using SponsorSphere.Domain.Models;

namespace SponsorSphere.Infrastructure.Repositories
{
    public class BlogPostRepository : IBlogPostRepository
    {
        private readonly SponsorSphereDbContext _context;

        public BlogPostRepository(SponsorSphereDbContext context)
        {
            _context = context;
        }

        public async Task<BlogPost> CreateAsync(BlogPost blogPost)
        {
            await _context.BlogPosts.AddAsync(blogPost);
            await _context.SaveChangesAsync();
            return blogPost;
        }

        public async Task<int> DeleteAsync(int blogPostId)
        {
            return await _context.BlogPosts
                .Where(se => se.Id.Equals(blogPostId))
                .ExecuteDeleteAsync();
        }

        public async Task<List<BlogPost>> GetBlogPostsByAuthorIdAsync(int authorId)
        {
            return await _context.BlogPosts
                .Include(bp => bp.Pictures)
                .Where(bp => bp.AuthorId == authorId)
                .ToListAsync();
        }

        public async Task<BlogPost> GetByIdAsync(int blogPostId)
        {
            var blogPost = await _context.BlogPosts.FirstOrDefaultAsync(bp => bp.Id == blogPostId);

            if (blogPost is not null)
            {
                return blogPost;
            }
            throw new ApplicationException($"Post with id {blogPostId} not found");
        }

        public async Task<List<BlogPost>> GetLatestBlogPostsAsync()
        {
            var latestPosts = await _context.BlogPosts
                .Include(bp => bp.Pictures)
                .OrderByDescending(bp => bp.Created)
                .Take(3).ToListAsync();

            return latestPosts;
        }

        public async Task<List<BlogPost>> GetLatestBlogPostsByAuthorIdAsync(int authorId)
        {
            var latestPosts = await _context.BlogPosts
                .Include(bp => bp.Pictures)
                 .Where(bp => bp.AuthorId == authorId)
                 .OrderByDescending(bp => bp.Created)
                 .Take(3).ToListAsync();

            return latestPosts;
        }

        public async Task<BlogPostDto> UpdateAsync(BlogPostDto blogPostToUpdate)
        {
            await _context.BlogPosts
                .Where(bp => bp.Id == blogPostToUpdate.Id)
                .ExecuteUpdateAsync(setters => setters
                .SetProperty(bp => bp.Content, blogPostToUpdate.Content)
                .SetProperty(bp => bp.Pictures, blogPostToUpdate.Pictures)
                );

            return blogPostToUpdate;
        }
    }
}
