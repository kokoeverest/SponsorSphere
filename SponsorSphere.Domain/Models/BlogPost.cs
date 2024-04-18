using System.Diagnostics.CodeAnalysis;

namespace SponsorSphere.Domain.Models
{
    public class BlogPost
    {
        public int? Id { get; set; }
        public required DateTime Created { get; set; }
        public required string Content { get; set; }
        public required User Author { get; set; }
        public ICollection<string>? Pictures { get; set; }

        public BlogPost() { }

        [SetsRequiredMembers]
        public BlogPost(string date, string content, User author, ICollection<string>? pictures)
        {
            Created = DateTime.Parse(date);
            Content = content;
            Author = author;
            Pictures = pictures;
        }
    }
}
