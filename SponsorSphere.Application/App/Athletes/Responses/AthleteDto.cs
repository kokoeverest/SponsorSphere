using SponsorSphere.Application.App.Users.Responses;
using SponsorSphere.Application.Interfaces;
using SponsorSphere.Domain.Enums;
using SponsorSphere.Domain.Models;

namespace SponsorSphere.Application.App.Athletes.Responses
{
    public class AthleteDto : UserDto, IUserDto
    {
        public string LastName { get; set; } = string.Empty;
        public int Age { get; set; }
        public SportsEnum Sport { get; set; }
        public DateTime BirthDate { get; set; }
        public List<Achievement> Achievements { get; set; } = [];
        public List<Goal> Goals { get; set; } = [];


        public static AthleteDto FromAthlete(Athlete athlete)
        {
            return new AthleteDto
            {
                Id = athlete.Id,
                Name = athlete.Name,
                LastName = athlete.LastName,
                Email = athlete.Email,
                Country = athlete.Country,
                Sport = athlete.Sport,
                Age = athlete.Age,
                BirthDate = athlete.BirthDate,
                Phone = athlete.Phone,
                Created = athlete.Created,
                PictureOrLogo = athlete.PictureOrLogo,
                Website = athlete.Website,
                FaceBookLink = athlete.FaceBookLink,
                InstagramLink = athlete.InstagramLink,
                TwitterLink = athlete.TwitterLink,
                StravaLink = athlete.StravaLink,
                Posts = athlete.Posts,
                Sponsorships = athlete.Sponsorships,
                Achievements = athlete.Achievements,
                Goals = athlete.Goals,
            };
        }
    }
}