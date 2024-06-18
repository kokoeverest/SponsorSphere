using SponsorSphere.Application.App.Users.Dtos;
using SponsorSphere.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace SponsorSphere.Application.App.Athletes.Dtos
{
    public class UpdateAthleteDto : UpdateUserDto
    {
        [StringLength(200, MinimumLength = 2)]
        [Required]
        public string LastName { get; set; } = string.Empty;

        [EnumDataType(typeof(SportsEnum))]
        [Required]
        public SportsEnum Sport { get; set; }

        [Display(Name = "Birth date dd/mm/yyyy")]
        public DateTime BirthDate { get; set; }
    }
}
