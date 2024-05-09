using SponsorSphere.Application.App.Sponsors.Dtos;

namespace SponsorSphere.Application.App.SponsorCompanies.Dtos
{
    public class SponsorCompanyDto : SponsorDto
    {
        public string IBAN { get; set; } = string.Empty;
    }
}
