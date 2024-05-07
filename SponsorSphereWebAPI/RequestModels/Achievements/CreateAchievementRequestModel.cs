﻿using SponsorSphere.Domain.Enums;
using SponsorSphere.Domain.Models;
using System.ComponentModel.DataAnnotations;

namespace SponsorSphereWebAPI.RequestModels.Achievements
{
    public class CreateAchievementRequestModel
    {
        [StringLength(200, MinimumLength = 2)]
        [Required]
        public string Name { get; set; } = string.Empty;

        [EnumDataType(typeof(CountryEnum))]
        [Required]
        public CountryEnum Country { get; set; }

        [Display(Name = "Date of event dd/mm/yyyy")]
        [StringLength(100, MinimumLength = 9)]
        [Required]
        public string EventDate { get; set; } = string.Empty;

        [EnumDataType(typeof(EventsEnum))]
        [Required]
        public EventsEnum EventType { get; set; }

        [EnumDataType(typeof(SportsEnum))]
        [Required]
        public SportsEnum Sport { get; set; }

        [Required]
        public ushort? PlaceFinished { get; set; }
    }
}
