using SponsorSphere.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace SponsorSphereWebAPI.RequestModels.SportEvents
{
    public class CreateSportEventRequestModel
    {
        [Required]
        [StringLength(maximumLength: 200)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [EnumDataType(typeof(CountryEnum))]
        public CountryEnum Country { get; set; }

        [Required]
        public string EventDate { get; set; } = string.Empty;

        [Required]
        [EnumDataType (typeof(EventsEnum))]
        public EventsEnum EventType { get; set; }

        [Required]
        [EnumDataType(typeof(SportsEnum))]
        public SportsEnum Sport { get; set; }
    }
}
