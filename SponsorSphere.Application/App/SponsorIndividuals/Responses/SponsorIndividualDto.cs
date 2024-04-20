using SponsorSphere.Application.App.Sponsors.Responses;

namespace SponsorSphere.Application.App.SponsorIndividuals.Responses
{
    public class SponsorIndividualDto : SponsorDto
    {
        public string LastName { get; set; } = string.Empty;
        public DateTime BirthDate { get; set; }
        public int Age { get; set; }
    }
}
