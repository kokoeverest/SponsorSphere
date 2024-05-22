using AutoMapper;
using MediatR;
using SponsorSphere.Application.App.BlogPosts.Dtos;
using SponsorSphere.Application.Interfaces;

namespace SponsorSphere.Application.App.BlogPosts.Queries;

public record GetLatestBlogPostsQuery : IRequest<List<BlogPostDto?>>;

public class GetLatestBlogPostsQueryHandler : IRequestHandler<GetLatestBlogPostsQuery, List<BlogPostDto?>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetLatestBlogPostsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    public async Task<List<BlogPostDto?>> Handle(GetLatestBlogPostsQuery request, CancellationToken cancellationToken)
    {
        var blogBosts = await _unitOfWork.BlogPostsRepository.GetLatestBlogPostsAsync();
        var mappedBlogPosts = _mapper.Map<List<BlogPostDto?>>(blogBosts);

        return mappedBlogPosts;
    }
}