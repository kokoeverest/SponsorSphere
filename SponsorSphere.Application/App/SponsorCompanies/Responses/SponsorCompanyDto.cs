using SponsorSphere.Application.App.Sponsors.Responses;

namespace SponsorSphere.Application.App.SponsorCompanies.Responses
{
    public class SponsorCompanyDto : SponsorDto
    {
        public string IBAN { get; set; } = string.Empty;
    }
}
