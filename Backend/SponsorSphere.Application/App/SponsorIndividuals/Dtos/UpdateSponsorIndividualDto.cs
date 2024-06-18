using SponsorSphere.Application.App.Users.Dtos;
using System.ComponentModel.DataAnnotations;

namespace SponsorSphere.Application.App.SponsorIndividuals.Dtos
{
    public class UpdateSponsorIndividualDto : UpdateUserDto
    {
        [StringLength(200, MinimumLength = 2)]
        [Required]
        public string LastName { get; set; } = string.Empty;

        [Display(Name = "Birth date dd/mm/yyyy")]
        public DateTime BirthDate { get; set; }
    }
}
