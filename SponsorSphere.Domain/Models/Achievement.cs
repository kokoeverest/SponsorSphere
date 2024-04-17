using SponsorSphere.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace SponsorSphere.Domain.Models
{
    public class Achievement
    {
        public SportsEnum Sport { get; set; }
        public required SportEvent EventType { get; set; }
        public ushort? PlaceFinished { get; set; }

        [Key]
        public int AthleteId { get; set; }

        [NotMapped]
        public required User Athlete { get; set; }

        public Achievement() { }

        [SetsRequiredMembers]
        public Achievement(User athlete, SportEvent sportEvent, ushort placeFinished)
        {
            Sport = sportEvent.Sport;
            EventType = sportEvent;
            PlaceFinished = placeFinished;
            Athlete = athlete;
        }
    }
}