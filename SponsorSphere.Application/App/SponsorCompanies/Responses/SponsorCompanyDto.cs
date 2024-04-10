using SponsorSphere.Application.App.Users.Responses;
using SponsorSphere.Application.Interfaces;

namespace SponsorSphere.Application.App.SponsorCompanies.Responses
{
    public class SponsorCompanyDto : UserDto, IUserDto
    {
        public string IBAN { get; set; } = string.Empty;
    }
}
