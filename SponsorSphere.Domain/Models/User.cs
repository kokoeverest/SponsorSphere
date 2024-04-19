namespace SponsorSphere.Domain.Models
{
    public abstract class User
    {
        public int? Id { get; set; }
        public required string Name { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public required string Country { get; set; }
        public required string PhoneNumber { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
        public string PictureOrLogo { get; set; } = string.Empty;
        public string Website { get; set; } = string.Empty;
        public string FaceBookLink { get; set; } = string.Empty;
        public string InstagramLink { get; set; } = string.Empty;
        public string TwitterLink { get; set; } = string.Empty;
        public string StravaLink { get; set; } = string.Empty;
        public ICollection<BlogPost> Posts { get; set; } = [];
    }
}
