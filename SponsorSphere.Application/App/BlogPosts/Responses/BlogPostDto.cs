using SponsorSphere.Domain.Models;

namespace SponsorSphere.Application.App.BlogPosts.Responses
{
    internal class BlogPostDto
    {
        public int Id { get; set; }
        public DateTime Created { get; set; }
        public string Content { get; set; } = string.Empty;
        public required User Author { get; set; }
        public ICollection<string> Pictures { get; set; } = [];
    }
}
