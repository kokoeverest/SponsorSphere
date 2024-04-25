﻿using SponsorSphere.Application.App.BlogPosts.Responses;
using SponsorSphere.Domain.Models;

namespace SponsorSphere.Application.Interfaces
{
    public interface IBlogPostRepository
    {
        Task<BlogPost> CreateAsync(BlogPost blogPost);
        Task<int> DeleteAsync(int blogPostId);
        Task<List<BlogPost>> GetBlogPostsByAuthorIdAsync(int authorId);
        Task<List<BlogPost>> GetLatestBlogPostsAsync();
        Task<List<BlogPost>> GetLatestBlogPostsByAuthorIdAsync(int authorId);
        Task<BlogPost> GetByIdAsync(int blogPostId);
        Task<BlogPostDto> UpdateAsync(BlogPostDto blogPostToUpdate);
    }
}