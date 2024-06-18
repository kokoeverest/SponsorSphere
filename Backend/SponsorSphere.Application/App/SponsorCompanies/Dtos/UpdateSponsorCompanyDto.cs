using SponsorSphere.Application.App.Users.Dtos;
using System.ComponentModel.DataAnnotations;

namespace SponsorSphere.Application.App.SponsorCompanies.Dtos
{
    public class UpdateSponsorCompanyDto : UpdateUserDto
    {
        [StringLength(34, MinimumLength = 15)]
        [Required]
        public string Iban { get; set; } = string.Empty;
    }
}
