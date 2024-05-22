using System.ComponentModel.DataAnnotations.Schema;

namespace SponsorSphere.Domain.Models
{
    public class BlogPostPicture
    {
        public required int BlogPostId { get; set; }
        public BlogPost? BlogPost { get; set; }
        public required int PictureId { get; set; }
        public Picture? Picture { get; set; }
    }
}