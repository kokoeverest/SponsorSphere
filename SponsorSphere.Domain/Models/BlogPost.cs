namespace SponsorSphere.Domain.Models
{
    public class BlogPost
    {
        public int Id { get; set; }
        public required DateTime Created { get; set; }
        public required string Content { get; set; }
        public int AuthorId { get; set; }
        public required User Author { get; set; }
        public ICollection<string>? Pictures { get; set; }
    }
}
