namespace SponsorSphere.Domain.Models
{
    public class BlogPost(string date, string content, User author, List<string> pictures)
    {
        public DateTime Date { get; set; } = DateTime.Parse(date);
        public string Content { get; set; } = content;
        public User Author { get; set; } = author;
        public List<string> Pictures { get; set; } = pictures;

    }
}

// BlogPost or Article or Post class - date, body, author, picture gallery/video link