using AutoMapper;
using SponsorSphere.Application.App.BlogPosts.Dtos;
using SponsorSphere.Domain.Models;

namespace SponsorSphere.Application.Profiles
{
    public class BlogPostProfile : Profile
    {
        public BlogPostProfile()
        {
            CreateMap<BlogPost, BlogPostDto>();
            CreateMap<CreateBlogPostDto, BlogPost>();
        }
    }
}
