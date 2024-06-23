using SponsorSphere.Application.App.BlogPosts.Dtos;
using SponsorSphere.Application.App.Pictures.Dtos;
using SponsorSphere.Application.App.Sponsorships.Dtos;
using SponsorSphere.Domain.Enums;
using SponsorSphere.Domain.Models;

namespace SponsorSphere.Application.Interfaces
{
    public interface IUserDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public CountryEnum Country { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime Created { get; set; }
        //public int PictureId { get; set; }
        public Picture? Picture { get; set; }
        public string Website { get; set; }
        public string FaceBookLink { get; set; }
        public string InstagramLink { get; set; }
        public string TwitterLink { get; set; }
        public string StravaLink { get; set; }
        public ICollection<BlogPostDto> BlogPosts { get; set; }
        public ICollection<SponsorshipDto> Sponsorships { get; set; }
    }
}
