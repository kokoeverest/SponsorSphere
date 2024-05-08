using SponsorSphere.Domain.Models;
using System.ComponentModel.DataAnnotations;

namespace SponsorSphere.Application.App.BlogPosts.Responses
{
    public class CreateBlogPostDto
    {
        public int Id { get; set; }

        [MinLength(50)]
        [Required]
        public string Content { get; set; } = string.Empty;

        public int AuthorId { get; set; }
        public User? Author { get; set; }
        public ICollection<Picture>? Pictures { get; set; }
    }
}
