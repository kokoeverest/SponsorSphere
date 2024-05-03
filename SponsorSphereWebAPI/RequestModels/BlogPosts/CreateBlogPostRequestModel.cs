using SponsorSphere.Domain.Models;
using System.ComponentModel.DataAnnotations;

namespace SponsorSphereWebAPI.RequestModels.BlogPosts
{
    public class CreateBlogPostRequestModel
    {
        public int Id { get; set; }

        [Required]
        [MinLength(50)]
        public string Content { get; set; } = string.Empty;

        [Required]
        public required int AuthorId { get; set; }
        public User? Author { get; set; }
        public ICollection<Picture>? Pictures { get; set; }
    }
}
