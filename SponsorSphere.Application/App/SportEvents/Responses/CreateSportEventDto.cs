using SponsorSphere.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace SponsorSphere.Application.App.SportEvents.Responses
{
    public class CreateSportEventDto
    {
        [Display(Name = "Name of event")]
        [StringLength(200, MinimumLength = 2)]
        [Required]
        public string Name { get; set; } = string.Empty;

        [EnumDataType(typeof(CountryEnum))]
        [Required]
        public CountryEnum Country { get; set; }

        [Display(Name = "Date of event dd/mm/yyyy")]
        [Required]
        public string EventDate { get; set; } = string.Empty;

        [EnumDataType (typeof(EventsEnum))]
        [Required]
        public EventsEnum EventType { get; set; }

        [EnumDataType(typeof(SportsEnum))]
        [Required]
        public SportsEnum Sport { get; set; }
    }
}
