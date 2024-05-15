using SponsorSphere.Domain.Models;
using System.ComponentModel.DataAnnotations;

namespace SponsorSphere.Application.App.BlogPosts.Dtos
{
    public class CreateBlogPostDto
    {
        [MinLength(50)]
        [Required]
        public string Content { get; set; } = string.Empty;

        public int AuthorId { get; set; }
        public ICollection<Picture>? Pictures { get; set; }
    }
}
