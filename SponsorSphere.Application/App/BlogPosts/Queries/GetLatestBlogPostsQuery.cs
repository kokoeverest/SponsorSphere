using AutoMapper;
using MediatR;
using SponsorSphere.Application.App.BlogPosts.Responses;
using SponsorSphere.Application.Interfaces;

namespace SponsorSphere.Application.App.BlogPosts.Queries;

public record GetLatestBlogPostsQuery : IRequest<BlogPostDto?>;

public class GetLatestBlogPostsQueryHandler : IRequestHandler<GetLatestBlogPostsQuery, BlogPostDto?>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetLatestBlogPostsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    public async Task<BlogPostDto?> Handle(GetLatestBlogPostsQuery request, CancellationToken cancellationToken)
    {
        var blogBosts = await _unitOfWork.BlogPostsRepository.GetLatestBlogPostsAsync();
        var mappedBlogPosts = _mapper.Map<BlogPostDto>(blogBosts);

        return mappedBlogPosts;
    }
}