using SponsorSphere.Domain.Models;

namespace SponsorSphere.Application.Interfaces
{
    public interface IBlogPostPictureRepository
    {
        
        Task<BlogPostPicture> CreateAsync(BlogPostPicture blogPostPicture);

        Task<BlogPostPicture> UpdateAsync(BlogPostPicture blogPostPicture);

        Task<ICollection<BlogPostPicture>> GetBlogPostPicturesAsync(int blogPostId, int pageNumber, int pageSize);
    }
}
