using MediatR;
using Microsoft.Extensions.Logging;
using SponsorSphere.Application.Interfaces;

namespace SponsorSphere.Application.App.BlogPosts.Commands;

public record DeleteBlogPostCommand(int AuthorId) : IRequest<int>;
public class DeleteBlogPostCommandHandler : IRequestHandler<DeleteBlogPostCommand, int>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<DeleteBlogPostCommandHandler> _logger;

    public DeleteBlogPostCommandHandler(IUnitOfWork unitOfWork, ILogger<DeleteBlogPostCommandHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<int> Handle(DeleteBlogPostCommand request, CancellationToken cancellationToken)
    {
        var start = DateTime.Now;
        _logger.LogInformation("Action: {Action}", request.ToString());

        try
        {
            await _unitOfWork.BeginTransactionAsync();
            var result = await _unitOfWork.BlogPostsRepository.DeleteAsync(request.AuthorId);
            await _unitOfWork.CommitTransactionAsync();

            _logger.LogInformation("Action: {Action}, ({DT})ms", request.ToString(), (DateTime.Now - start).TotalMilliseconds);
            return result;
        }
        catch (Exception)
        {
            await _unitOfWork.RollbackTransactionAsync();
            throw;
        }
    }
}