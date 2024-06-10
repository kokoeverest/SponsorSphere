namespace SponsorSphere.Application.App.Pictures.Dtos
{
    public class PictureDto
    {
        public int Id { get; set; }
        public string? Url { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public DateTime Modified { get; set; }
    }
}
