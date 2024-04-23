using SponsorSphere.Application.App.BlogPosts.Responses;
using SponsorSphere.Application.App.Sponsorships.Responses;

namespace SponsorSphere.Application.Interfaces
{
    public interface IUserDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Country { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime Created { get; set; }
        public string PictureOrLogo { get; set; }
        public string Website { get; set; }
        public string FaceBookLink { get; set; }
        public string InstagramLink { get; set; }
        public string TwitterLink { get; set; }
        public string StravaLink { get; set; }
        public ICollection<BlogPostDto> Posts { get; set; }
        public ICollection<SponsorshipDto> Sponsorships { get; set; }
    }
}
