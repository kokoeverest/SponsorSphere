using System.ComponentModel.DataAnnotations;

namespace SponsorSphere.Domain.Enums
{
    public enum SportsEnum
    {
        [Display(Name = "Football")]
        Football,

        [Display(Name = "Volleyball")]
        Volleyball,

        [Display(Name = "Basketball")]
        Basketball,

        [Display(Name = "Hockey")]
        Hockey,

        [Display(Name = "Golf")]
        Golf,

        [Display(Name = "Horse Riding")]
        HorseRiding,

        [Display(Name = "Triathlon")]
        Triathlon,

        [Display(Name = "Swimming")]
        Swimming,

        [Display(Name = "Free Diving")]
        FreeDiving,

        [Display(Name = "Road Cycling")]
        RoadCycling,

        [Display(Name = "Cross Country Cycling")]
        CrossCountryCycling,

        [Display(Name = "Downhill Mountain Biking")]
        DownhillMountainBiking,

        [Display(Name = "Ultramarathon Running")]
        UltramarathonRunning,

        [Display(Name = "Sky Running")]
        SkyRunning,

        [Display(Name = "Mountain Running")]
        MountainRunning,

        [Display(Name = "Road Running")]
        RoadRunning
    }
}