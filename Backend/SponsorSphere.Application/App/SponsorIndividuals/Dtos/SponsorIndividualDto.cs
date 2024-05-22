using SponsorSphere.Application.App.Sponsors.Dtos;

namespace SponsorSphere.Application.App.SponsorIndividuals.Dtos
{
    public class SponsorIndividualDto : SponsorDto
    {
        public string LastName { get; set; } = string.Empty;
        public DateTime BirthDate { get; set; }
        public int Age { get; set; }
    }
}
