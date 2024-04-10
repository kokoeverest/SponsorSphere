﻿using AutoMapper;
using SponsorSphere.Application.App.BlogPosts.Responses;
using SponsorSphere.Application.App.Sponsorships.Responses;
using SponsorSphere.Application.Interfaces;
using SponsorSphere.Domain.Models;

namespace SponsorSphere.Application.App.Users.Responses
{
    public class UserDto : IUserDto
    {
        public int? Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public DateTime Created { get; set; }
        public string PictureOrLogo { get; set; } = string.Empty;
        public string Website { get; set; } = string.Empty;
        public string FaceBookLink { get; set; } = string.Empty;
        public string InstagramLink { get; set; } = string.Empty;
        public string TwitterLink { get; set; } = string.Empty;
        public string StravaLink { get; set; } = string.Empty;
        public List<BlogPost> Posts { get; set; } = [];
        public List<Sponsorship> Sponsorships { get; set; } = [];

    }
}