using SponsorSphere.Domain.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace SponsorSphere.Domain.Models
{
    public abstract class User : IUser
    {
        public int? Id { get; set; }

        [MaxLength(200)]
        public required string Name { get; set; }

        [MaxLength(100)]
        public required string Email { get; set; }

        [MaxLength(200)]
        public required string Password { get; set; }

        [MaxLength(100)]
        public required string Country { get; set; }

        [MaxLength(16)]
        public required string PhoneNumber { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
        public string PictureOrLogo { get; set; } = string.Empty;

        [MaxLength(200)]
        public string Website { get; set; } = string.Empty;

        [MaxLength(200)]
        public string FaceBookLink { get; set; } = string.Empty;

        [MaxLength(200)]
        public string InstagramLink { get; set; } = string.Empty;

        [MaxLength(200)]
        public string TwitterLink { get; set; } = string.Empty;

        [MaxLength(200)]
        public string StravaLink { get; set; } = string.Empty;

        [NotMapped]
        public List<BlogPost> Posts { get; set; } = [];

        [NotMapped]
        public List<Sponsorship> Sponsorships { get; set; } = [];

        public User() { }

        [SetsRequiredMembers]
        public User(
            string name,
            string email,
            string password,
            string country,
            string phoneNumber
        )
        {
            Name = name;
            Email = email;
            Password = password;
            Country = country;
            PhoneNumber = phoneNumber;
        }

        public Task? Register()
        {
            return null;
        }
        public Task? Login()
        {
            return null;
        }
        public Task? Logout()
        {
            return null;
        }
        public Task? ResetPassword()
        {
            return null;
        }
        public Task? EditProfile()
        {
            return null;
        }
    }
}
