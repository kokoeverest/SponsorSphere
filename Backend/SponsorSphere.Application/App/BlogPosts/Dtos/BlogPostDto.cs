using SponsorSphere.Application.App.Pictures.Dtos;
using SponsorSphere.Domain.Models;

namespace SponsorSphere.Application.App.BlogPosts.Dtos
{
    public class BlogPostDto
    {
        public int Id { get; set; }
        public DateTime Created { get; set; }
        public string Content { get; set; } = string.Empty;
        public required int AuthorId { get; set; }
        public ICollection<Picture>? Pictures { get; set; }
    }
}
