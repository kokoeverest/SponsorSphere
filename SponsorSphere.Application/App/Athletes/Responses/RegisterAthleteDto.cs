using SponsorSphere.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace SponsorSphere.Application.App.Athletes.Responses
{
    public class RegisterAthleteDto
    {
        [StringLength(200, MinimumLength = 2)]
        [Required]
        public string Name { get; set; } = string.Empty;

        [EnumDataType(typeof(CountryEnum))]
        [Required]
        public CountryEnum Country { get; set; }

        [RegularExpression(@"^\+?[1-9]{1,3}\s?[\s\-/0-9]+$")]
        [StringLength(16, MinimumLength = 10)]
        [Required]
        public string PhoneNumber { get; set; } = string.Empty;

        [EmailAddress]
        [Required]
        public string Email { get; set; } = string.Empty;

        [StringLength(200, MinimumLength = 8)]
        [Required]
        public string Password { get; set; } = string.Empty;

        [StringLength(200, MinimumLength = 5)]
        [Required]
        public string LastName { get; set; } = string.Empty;

        [EnumDataType(typeof(SportsEnum))]
        [Required]
        public SportsEnum Sport { get; set; }

        [Display(Name = "Birth date dd/mm/yyyy")]
        [StringLength(100, MinimumLength = 9)]
        [Required]
        public string BirthDate { get; set; } = string.Empty;
    }
}
