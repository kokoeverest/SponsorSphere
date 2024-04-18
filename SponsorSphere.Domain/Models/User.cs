using SponsorSphere.Domain.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace SponsorSphere.Domain.Models
{
    public abstract class User
    {
        public int? Id { get; set; }
        public required string Name { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public required string Country { get; set; }
        public required string PhoneNumber { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
        public string PictureOrLogo { get; set; } = string.Empty;
        public string Website { get; set; } = string.Empty;
        public string FaceBookLink { get; set; } = string.Empty;
        public string InstagramLink { get; set; } = string.Empty;
        public string TwitterLink { get; set; } = string.Empty;
        public string StravaLink { get; set; } = string.Empty;
        public ICollection<BlogPost> Posts { get; set; } = [];
        

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
