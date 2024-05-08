using System.ComponentModel.DataAnnotations;

namespace SponsorSphere.Application.App.Pictures.Responses
{
    public class CreatePictureDto
    {
        public int Id { get; set; }

        [Url]
        [Required]
        public string Url { get; set; } = string.Empty;
        public byte? Content { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime Modified { get; set; }
    }
}
