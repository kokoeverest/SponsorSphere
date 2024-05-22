using SponsorSphere.Application.App.BlogPosts.Dtos;
using SponsorSphere.Domain.Models;

namespace SponsorSphere.Application.Interfaces
{
    public interface IBlogPostRepository
    {
        /// <summary>
        /// Creates a new blog post asynchronously.
        /// </summary>
        /// <param name="blogPost">The blog post object to be created.</param>
        /// <returns>A task representing the asynchronous operation, returning the created blog post.</returns>
        Task<BlogPost> CreateAsync(BlogPost blogPost);

        /// <summary>
        /// Deletes a blog post by its ID.
        /// </summary>
        /// <param name="blogPostId">The ID of the blog post to be deleted.</param>
        /// <returns>Upon completion returns the number of affected rows (typically 1 if successful).</returns>
        Task<int> DeleteAsync(int blogPostId);

        /// <summary>
        ///  Retrieves a list of blog posts authored by a specific author, with pagination support.
        /// </summary>
        /// <param name="pageNumber">The page number of the results.</param>
        /// <param name="pageSize">The number of blog posts per page.</param>
        /// <param name="authorId">The ID of the author whose blog posts are to be retrieved.</param>
        /// <returns>Upon completion returns the list of blog posts authored by the specified author on the specified page.</returns>
        Task<List<BlogPost>> GetBlogPostsByAuthorIdAsync(int pageNumber, int pageSize, int authorId);

        /// <summary>
        /// Retrieves the latest three blog posts, sorted by creation date.
        /// </summary>
        /// <returns>Upon completion returns the list of latest blog posts.</returns>
        Task<List<BlogPost>> GetLatestBlogPostsAsync();

        /// <summary>
        ///  Retrieves a list with the latest (maximum three) blog posts authored by a specific author.
        /// </summary>
        /// <param name="authorId">The ID of the author whose latest blog posts are to be retrieved.</param>
        /// <returns>Upon completion returns the list of latest blog posts authored by the specified author.</returns>
        Task<List<BlogPost>> GetLatestBlogPostsByAuthorIdAsync(int authorId);

        /// <summary>
        ///  Retrieves a blog post by its ID.
        /// </summary>
        /// <param name="blogPostId">The ID of the blog post to be retrieved.</param>
        /// <returns>The specific blogPost or throws a NotNoundException if a blogPost with that id is not found</returns>
        Task<BlogPost> GetByIdAsync(int blogPostId);

        /// <summary>
        /// Asynchronously updates an existing blog post in the database with the data provided.
        /// </summary>
        /// <param name="blogPostToUpdate">The blog post object to be updated.</param>
        /// <returns>The updated blog post as a data transfer object (DTO)</returns>
        Task<BlogPostDto> UpdateAsync(BlogPostDto blogPostToUpdate);
    }
}
