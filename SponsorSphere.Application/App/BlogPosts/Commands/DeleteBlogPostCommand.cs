using MediatR;
using SponsorSphere.Application.Interfaces;

namespace SponsorSphere.Application.App.BlogPosts.Commands;

public record DeleteBlogPostCommand(int AuthorId) : IRequest<int>;
public class DeleteBlogPostCommandHandler : IRequestHandler<DeleteBlogPostCommand, int>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteBlogPostCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<int> Handle(DeleteBlogPostCommand request, CancellationToken cancellationToken)
    {
        return await _unitOfWork.BlogPostsRepository.DeleteAsync(request.AuthorId);
    }
}