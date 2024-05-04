using SponsorSphere.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace SponsorSphereWebAPI.RequestModels.Sponsorships
{
    public class CreateSponsorshipRequestModel
    {
        [Required]
        [EnumDataType(typeof(SponsorshipLevel))]
        public SponsorshipLevel Level { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [Required]
        [Range(minimum: 0, maximum: int.MaxValue)]
        public int AthleteId {  get; set; }
    }
}
