﻿using Microsoft.AspNetCore.Identity;
using SponsorSphere.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SponsorSphere.Domain.Models
{
    public class User: IdentityUser<int>
    {
        //public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;
        //public string Email { get; set; }

        //[Required]
        //public  string Password { get; set; } = string.Empty;

        [Required]
        public  CountryEnum Country { get; set; }

        [Required]
        public override string? PhoneNumber { get; set; } = string.Empty;
        public DateTime Created { get; set; } = DateTime.UtcNow;
        public int? PictureId { get; set; }
        public string Website { get; set; } = string.Empty;
        public string FaceBookLink { get; set; } = string.Empty;
        public string InstagramLink { get; set; } = string.Empty;
        public string TwitterLink { get; set; } = string.Empty;
        public string StravaLink { get; set; } = string.Empty;
        public bool IsDeleted { get; set; }
        public DateTime DeletedOn {  get; set; }
        public ICollection<BlogPost> BlogPosts { get; set; } = [];

        [NotMapped]
        public ICollection<Sponsorship> Sponsorships { get; set; } = [];
    }
}
