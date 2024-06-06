﻿using AutoMapper;
using SponsorSphere.Application.App.Pictures.Dtos;
using SponsorSphere.Domain.Models;

namespace SponsorSphere.Application.Profiles
{
    public class PictureProfile : Profile
    {
        public PictureProfile()
        {
            CreateMap<Picture, PictureDto>();
            CreateMap<CreatePictureDto, Picture>()
                .ForMember(p => p.Modified, opt => opt.NullSubstitute(DateTime.UtcNow));
                
        }
    }
}