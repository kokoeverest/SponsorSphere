using AutoMapper;
using MediatR;
using SponsorSphere.Application.App.BlogPosts.Responses;
using SponsorSphere.Application.Interfaces;
using SponsorSphere.Domain.Models;

namespace SponsorSphere.Application.App.BlogPosts.Commands;

public record CreateBlogPostCommand(BlogPost BlogPost) : IRequest<BlogPostDto>;

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
        var newBlogPost = _unitOfWork.BlogPostsRepository.CreateAsync(request.BlogPost);
        var mappedBlogPost = _mapper.Map<BlogPostDto>(newBlogPost);

        return await Task.FromResult(mappedBlogPost);
    }
}
