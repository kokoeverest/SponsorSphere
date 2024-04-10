using AutoMapper;
using SponsorSphere.Application.App.BlogPosts.Responses;
using SponsorSphere.Domain.Models;

namespace SponsorSphere.Application.Profiles
{
    public class BlogPostProfile : Profile
    {
        public BlogPostProfile()
        {
            CreateMap<BlogPost, BlogPostDto>();
        }
    }
}
