using SponsorSphere.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace SponsorSphere.Application.App.Goals.Responses
{
    public class CreateGoalDto
    {
        [StringLength(200, MinimumLength = 10)]
        [Required]
        public string Name { get; set; } = string.Empty;

        [EnumDataType(typeof(CountryEnum))]
        [Required]
        public CountryEnum Country {  get; set; }

        [Display(Name = "Date of event dd/mm/yyyy")]
        [StringLength(100, MinimumLength = 9)]
        [Required]
        public string EventDate { get; set; } = string.Empty;

        [EnumDataType(typeof(EventsEnum))]
        [Required]
        public EventsEnum EventType {  get; set; }

        [EnumDataType(typeof(SportsEnum))]
        [Required]
        public SportsEnum Sport {  get; set; }

        [Range(minimum: 1, maximum: double.MaxValue)]
        public decimal AmountNeeded {  get; set; }
    }
}
