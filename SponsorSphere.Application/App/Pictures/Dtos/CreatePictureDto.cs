using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace SponsorSphere.Application.App.Pictures.Dtos
{
    public class CreatePictureDto
    {
        [DataType(DataType.DateTime)]
        public DateTime? Modified { get; set; }

        [Required]
        [Display(Name = "File")]
        public required IFormFile FormFile { get; set; }
    }
}
