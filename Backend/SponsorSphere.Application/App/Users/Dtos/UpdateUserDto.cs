using Microsoft.AspNetCore.Http;
using SponsorSphere.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace SponsorSphere.Application.App.Users.Dtos
{
    public class UpdateUserDto
    {
        public int Id { get; set; }

        [StringLength(200, MinimumLength = 2)]
        [Required]
        public string Name { get; set; } = string.Empty;

        [EmailAddress]
        [Required]
        public string Email { get; set; } = string.Empty;

        [Required]
        [EnumDataType(typeof(CountryEnum))]
        public CountryEnum Country { get; set; }

        [RegularExpression(@"^\+?[1-9]{1,3}\s?[\s\-/0-9]+$")]
        [StringLength(16, MinimumLength = 10)]
        [Required]
        public string PhoneNumber { get; set; } = string.Empty;
        public DateTime Created { get; set; }
        public IFormFile? Picture { get; set; }

        public string? Website { get; set; } = string.Empty;
        public string? FaceBookLink { get; set; } = string.Empty;
        public string? InstagramLink { get; set; } = string.Empty;
        public string? TwitterLink { get; set; } = string.Empty;
        public string? StravaLink { get; set; } = string.Empty;
    }
}
