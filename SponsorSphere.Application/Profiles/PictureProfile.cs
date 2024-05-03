﻿using AutoMapper;
using SponsorSphere.Application.App.Pictures.Responses;
using SponsorSphere.Domain.Models;

namespace SponsorSphere.Application.Profiles
{
    public class PictureProfile : Profile
    {
        public PictureProfile()
        {
            CreateMap<Picture, PictureDto>();
        }
    }
}