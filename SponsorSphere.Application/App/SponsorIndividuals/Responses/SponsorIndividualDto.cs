using SponsorSphere.Application.App.Users.Responses;
using SponsorSphere.Application.Interfaces;

namespace SponsorSphere.Application.App.SponsorIndividuals.Responses
{
    public class SponsorIndividualDto : UserDto, IUserDto
    {
        public string LastName { get; set; } = string.Empty;
        public DateTime BirthDate { get; set; }
        public int Age { get; set; }
    }
}
