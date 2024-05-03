using SponsorSphere.Domain.Enums;
using SponsorSphere.Domain.Models;
using System.ComponentModel.DataAnnotations;

namespace SponsorSphereWebAPI.RequestModels.Achievements
{
    public class CreateAchievementRequestModel
    {

        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public CountryEnum Country { get; set; }

        [Required]
        public string EventDate { get; set; } = string.Empty;

        [Required]
        public EventsEnum EventType { get; set; }

        [Required]
        public SportsEnum Sport { get; set; }

        [Required]
        public ushort? PlaceFinished { get; set; }
    }
}
