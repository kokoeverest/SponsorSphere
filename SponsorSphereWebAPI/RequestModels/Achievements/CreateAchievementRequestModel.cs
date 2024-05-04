﻿using SponsorSphere.Domain.Enums;
using SponsorSphere.Domain.Models;
using System.ComponentModel.DataAnnotations;

namespace SponsorSphereWebAPI.RequestModels.Achievements
{
    public class CreateAchievementRequestModel
    {

        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        [EnumDataType(typeof(CountryEnum))]
        public CountryEnum Country { get; set; }

        [Required]
        [StringLength(maximumLength: 100)]
        public string EventDate { get; set; } = string.Empty;

        [Required]
        [EnumDataType(typeof(EventsEnum))]
        public EventsEnum EventType { get; set; }

        [Required]
        [EnumDataType(typeof(SportsEnum))]
        public SportsEnum Sport { get; set; }

        [Required]
        public ushort? PlaceFinished { get; set; }
    }
}
