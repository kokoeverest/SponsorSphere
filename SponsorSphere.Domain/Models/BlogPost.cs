namespace SponsorSphere.Domain.Models
{
    public class BlogPost(string date, string content, User author, List<string> pictures)
    {
        public int? Id { get; set; }
        public DateTime Created { get; set; } = DateTime.Parse(date);
        public string Content { get; set; } = content;
        public User Author { get; set; } = author;
        public List<string> Pictures { get; set; } = pictures;

    }
}
