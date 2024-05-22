using SponsorSphere.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace SponsorSphere.Application.App.Goals.Dtos
{
    public class CreateGoalDto
    {
        public int SportEventId { get; set; }

        [Range(minimum: 1, maximum: double.MaxValue)]
        public decimal AmountNeeded { get; set; }
    }
}
