using AutoMapper;
using MediatR;
using SponsorSphere.Application.App.BlogPosts.Responses;
using SponsorSphere.Application.Interfaces;

namespace SponsorSphere.Application.App.BlogPosts.Queries;

public record GetLatestBlogPostsByAuthorIdQuery(int AuthorId) : IRequest<BlogPostDto?>;

public class GetLatestBlogPostsByAuthorIdQueryHandler : IRequestHandler<GetLatestBlogPostsByAuthorIdQuery, BlogPostDto?>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetLatestBlogPostsByAuthorIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    public async Task<BlogPostDto?> Handle(GetLatestBlogPostsByAuthorIdQuery request, CancellationToken cancellationToken)
    {
        var blogBosts = await _unitOfWork.BlogPostsRepository.GetLatestBlogPostsByAuthorIdAsync(request.AuthorId);
        var mappedBlogPosts = _mapper.Map<BlogPostDto>(blogBosts);

        return mappedBlogPosts;
    }
}