using SponsorSphere.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace SponsorSphere.Domain.Models
{
    public class Achievement
    {
        public SportsEnum Sport { get; set; }
        public required SportEvent SportEvent { get; set; }
        public ushort? PlaceFinished { get; set; }
        public required Athlete AthleteAchievement { get; set; }

        public Achievement() { }

        [SetsRequiredMembers]
        public Achievement(Athlete athlete, SportEvent sportEvent, ushort placeFinished)
        {
            Sport = sportEvent.Sport;
            SportEvent = sportEvent;
            PlaceFinished = placeFinished;
            AthleteAchievement = athlete;
        }
    }
}