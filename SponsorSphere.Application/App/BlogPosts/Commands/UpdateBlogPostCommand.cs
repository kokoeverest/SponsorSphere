using MediatR;
using SponsorSphere.Application.App.BlogPosts.Dtos;
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

    public async Task<BlogPostDto> Handle(UpdateBlogPostCommand request, CancellationToken cancellationToken)
    {
        try
        {
            await _unitOfWork.BeginTransactionAsync();
            var result = await _unitOfWork.BlogPostsRepository.UpdateAsync(request.BlogPostToUpdate);
            await _unitOfWork.CommitTransactionAsync();
            return result;
        }

        catch (Exception)
        {
            await _unitOfWork.RollbackTransactionAsync();
            throw;
        }
    }

}