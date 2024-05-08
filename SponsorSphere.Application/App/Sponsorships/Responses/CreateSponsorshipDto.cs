using SponsorSphere.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace SponsorSphere.Application.App.Sponsorships.Responses
{
    public class CreateSponsorshipDto
    {
        [Required]
        [EnumDataType(typeof(SponsorshipLevel))]
        public SponsorshipLevel Level { get; set; }

        [Range(1, double.MaxValue)]
        [DataType(DataType.Currency)]
        public decimal Amount { get; set; }

        public int AthleteId {  get; set; }
    }
}
