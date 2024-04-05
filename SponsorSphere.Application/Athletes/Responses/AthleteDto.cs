using SponsorSphere.Application.Interfaces;
using SponsorSphere.Domain.Enums;
using SponsorSphere.Domain.Models;

namespace SponsorSphere.Application.Athletes.Responses
{
    public class AthleteDto : IUserDto
    {
        public int? Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty; 
        public string Phone { get; set; } = string.Empty;
        public int Age { get; set; }
        public SportsEnum Sport { get; set; }
        public DateTime BirthDate { get; set; }
        public DateTime Created { get; set; }
        public string PictureOrLogo { get; set; } = string.Empty;
        public string Website { get; set; } = string.Empty;
        public string FaceBookLink { get; set; } = string.Empty;
        public string InstagramLink { get; set; } = string.Empty;
        public string TwitterLink { get; set; } = string.Empty;
        public string StravaLink { get; set; } = string.Empty;
        public List<BlogPost> Posts { get; set; } = [];
        public List<User> Sponsors { get; set; } = [];
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
                Phone = athlete.Phone,
                Sport = athlete.Sport,
                Age = athlete.Age,
                BirthDate = athlete.BirthDate,
                Created = athlete.Created,
                PictureOrLogo = athlete.PictureOrLogo,
                Website = athlete.Website,
                FaceBookLink = athlete.FaceBookLink,
                InstagramLink = athlete.InstagramLink,
                TwitterLink = athlete.TwitterLink,
                StravaLink = athlete.StravaLink,
                Posts = athlete.Posts,
                Sponsors = athlete.Sponsors,
                Achievements = athlete.Achievements,
                Goals = athlete.Goals,
            };
        }
    }
}