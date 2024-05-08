using SponsorSphere.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace SponsorSphere.Application.App.SponsorCompanies.Responses
{
    public class RegisterSponsorCompanyDto
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

        [StringLength(34, MinimumLength = 15)]
        [Required]
        public string IBAN { get; set; } = string.Empty;
    }
}
