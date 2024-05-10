using AutoMapper;
using MediatR;
using SponsorSphere.Application.App.BlogPosts.Dtos;
using SponsorSphere.Application.Interfaces;
using SponsorSphere.Domain.Models;

namespace SponsorSphere.Application.App.BlogPosts.Commands;

public record CreateBlogPostCommand(CreateBlogPostDto BlogPost) : IRequest<BlogPostDto>;

public class CreateBlogPostCommandHandler : IRequestHandler<CreateBlogPostCommand, BlogPostDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateBlogPostCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<BlogPostDto> Handle(CreateBlogPostCommand request, CancellationToken cancellationToken)
    {
        var blogPost = _mapper.Map<BlogPost>(request.BlogPost);

        try
        {
            await _unitOfWork.BeginTransactionAsync();
            await _unitOfWork.BlogPostsRepository.CreateAsync(blogPost);
            await _unitOfWork.CommitTransactionAsync();

            var mappedBlogPost = _mapper.Map<BlogPostDto>(blogPost);
            return await Task.FromResult(mappedBlogPost);
        }

        catch (Exception)
        {
            await _unitOfWork.RollbackTransactionAsync();
            throw;
        }
    }
}
