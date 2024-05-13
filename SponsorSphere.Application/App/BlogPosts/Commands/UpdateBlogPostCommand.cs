﻿using MediatR;
using Microsoft.Extensions.Logging;
using SponsorSphere.Application.App.BlogPosts.Dtos;
using SponsorSphere.Application.Interfaces;

namespace SponsorSphere.Application.App.BlogPosts.Commands;

public record UpdateBlogPostCommand(BlogPostDto BlogPostToUpdate) : IRequest<BlogPostDto>;
public class UpdateBlogPostCommandHandler : IRequestHandler<UpdateBlogPostCommand, BlogPostDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<UpdateBlogPostCommandHandler> _logger;

    public UpdateBlogPostCommandHandler(IUnitOfWork unitOfWork, ILogger<UpdateBlogPostCommandHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<BlogPostDto> Handle(UpdateBlogPostCommand request, CancellationToken cancellationToken)
    {
        var start = DateTime.Now;
        _logger.LogInformation("Action: {Action}", request.ToString());

        try
        {
            await _unitOfWork.BeginTransactionAsync();
            var result = await _unitOfWork.BlogPostsRepository.UpdateAsync(request.BlogPostToUpdate);
            await _unitOfWork.CommitTransactionAsync();

            _logger.LogInformation("Action: {Action}, ({DT})ms", request.ToString(), (DateTime.Now - start).TotalMilliseconds);
            return result;
        }

        catch (Exception)
        {
            await _unitOfWork.RollbackTransactionAsync();
            _logger.LogError("Action: {Action} failed", request.ToString());
            throw;
        }
    }

}