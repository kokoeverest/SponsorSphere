using SponsorSphere.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace SponsorSphereWebAPI.RequestModels.Goals
{
    public class CreateGoalRequestModel
    {
        [Required]
        [StringLength(maximumLength: 200)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [EnumDataType(typeof(CountryEnum))]
        public CountryEnum Country {  get; set; }

        [Required]
        [StringLength(maximumLength: 100)]
        public string EventDate { get; set; } = string.Empty;

        [Required]
        [EnumDataType(typeof(EventsEnum))]
        public EventsEnum EventType {  get; set; }

        [Required]
        [EnumDataType(typeof(SportsEnum))]
        public SportsEnum Sport {  get; set; }

        [Required]
        [Range(minimum: 0, maximum: double.MaxValue)]
        public decimal AmountNeeded {  get; set; }
    }
}
