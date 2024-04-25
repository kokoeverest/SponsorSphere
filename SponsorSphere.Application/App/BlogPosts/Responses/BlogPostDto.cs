using SponsorSphere.Application.App.Users.Responses;
using SponsorSphere.Domain.Models;

namespace SponsorSphere.Application.App.BlogPosts.Responses
{
    public class BlogPostDto
    {
        public int Id { get; set; }
        public DateTime Created { get; set; }
        public string Content { get; set; } = string.Empty;
        public required int AuthorId { get; set; }
        public ICollection<Picture> Pictures { get; set; } = [];
    }
}
