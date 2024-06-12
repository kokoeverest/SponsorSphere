namespace SponsorSphere.Domain.Models
{
    public class BlogPost
    {
        public int Id { get; set; }
        public DateTime Created { get; set; } = DateTime.UtcNow;
        public required string Content { get; set; }
        public int AuthorId { get; set; }
        public User? Author { get; set; }
        public ICollection<Picture> Pictures { get; set; } = [];
    }
}
