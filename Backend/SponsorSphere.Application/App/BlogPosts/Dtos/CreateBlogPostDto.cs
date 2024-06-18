using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace SponsorSphere.Application.App.BlogPosts.Dtos
{
    public class CreateBlogPostDto
    {
        [MinLength(50)]
        [Required]
        public string Content { get; set; } = string.Empty;

        public int AuthorId { get; set; }
        public ICollection<IFormFile> Pictures { get; set; } = [];
    }
}
