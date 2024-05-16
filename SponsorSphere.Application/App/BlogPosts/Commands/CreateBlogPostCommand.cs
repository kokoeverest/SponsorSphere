using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using SponsorSphere.Application.App.BlogPosts.Dtos;
using SponsorSphere.Application.Common.Constants;
using SponsorSphere.Application.Interfaces;
using SponsorSphere.Domain.Models;

namespace SponsorSphere.Application.App.BlogPosts.Commands;

public record CreateBlogPostCommand(CreateBlogPostDto BlogPost) : IRequest<BlogPostDto>;

public class CreateBlogPostCommandHandler : IRequestHandler<CreateBlogPostCommand, BlogPostDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ILogger<CreateBlogPostCommandHandler> _logger;

    public CreateBlogPostCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<CreateBlogPostCommandHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<BlogPostDto> Handle(CreateBlogPostCommand request, CancellationToken cancellationToken)
    {
        var start = DateTime.Now;
        _logger.LogInformation(LoggingConstants.logStartString, request.ToString());

        var blogPost = _mapper.Map<BlogPost>(request.BlogPost);

        try
        {
            await _unitOfWork.BeginTransactionAsync();
            await _unitOfWork.BlogPostsRepository.CreateAsync(blogPost);
            await _unitOfWork.CommitTransactionAsync();

            var mappedBlogPost = _mapper.Map<BlogPostDto>(blogPost);

            _logger.LogInformation(LoggingConstants.logEndString, request.ToString(), (DateTime.Now - start).TotalMilliseconds);
            return await Task.FromResult(mappedBlogPost);
        }

        catch (Exception)
        {
            await _unitOfWork.RollbackTransactionAsync();
            throw;
        }
    }
}
