using SponsorSphere.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace SponsorSphere.Application.App.Achievements.Dtos
{
    public class CreateAchievementDto
    {
        public int SportEventId { get; set; }
        public ushort? PlaceFinished { get; set; }

        [StringLength(2000)]
        public string? Description { get; set; }
    }
}
