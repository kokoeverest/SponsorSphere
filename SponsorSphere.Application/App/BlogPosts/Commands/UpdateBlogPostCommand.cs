using MediatR;
using SponsorSphere.Application.App.BlogPosts.Responses;
using SponsorSphere.Application.Interfaces;

namespace SponsorSphere.Application.App.BlogPosts.Commands;

public record UpdateBlogPostCommand(BlogPostDto BlogPostToUpdate) : IRequest<BlogPostDto>;
public class UpdateBlogPostCommandHandler : IRequestHandler<UpdateBlogPostCommand, BlogPostDto>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateBlogPostCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<BlogPostDto> Handle(UpdateBlogPostCommand request, CancellationToken cancellationToken) =>
 
        await _unitOfWork.BlogPostsRepository.UpdateAsync(request.BlogPostToUpdate);
}