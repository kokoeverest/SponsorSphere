using SponsorSphere.Domain.Models;

namespace SponsorSphere.Application.App.Pictures.Responses
{
    public class PictureDto
    {
        public int Id { get; set; }
        public string Url { get; set; } = string.Empty;
        public byte? Content { get; set; }
        public DateTime Modified { get; set; }
    }
}
