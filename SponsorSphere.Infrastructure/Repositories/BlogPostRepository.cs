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
            throw new NotImplementedException();
        }

        public async Task<List<BlogPost>> GetBlogPostsByAuthorIdAsync(int authorId)
        {
            throw new NotImplementedException();
        }

        public async Task<BlogPost?> GetByIdAsync(int blogPostId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<BlogPost>> GetLatestBlogPostsAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<List<BlogPost>> GetLatestBlogPostsByAuthorIdAsync(int authorId)
        {
            throw new NotImplementedException();
        }

        public void Update(BlogPost blogPostToUpdate)
        {
            throw new NotImplementedException();
        }
    }
}
