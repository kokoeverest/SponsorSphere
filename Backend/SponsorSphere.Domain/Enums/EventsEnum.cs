using System.ComponentModel.DataAnnotations;

namespace SponsorSphere.Domain.Enums
{
    public enum EventsEnum
    {
        [Display(Name = "Race")]
        Race,

        [Display(Name = "Self Organized")]
        SelfOrganized,

        [Display(Name = "Training")]
        Training,

        [Display(Name = "Knockout")]
        Knockout
    }
}
