using AutoMapper;
using MediatR;
using SponsorSphere.Application.App.BlogPosts.Dtos;
using SponsorSphere.Application.Interfaces;

namespace SponsorSphere.Application.App.BlogPosts.Queries;

public record GetLatestBlogPostsByAuthorIdQuery(int AuthorId) : IRequest<List<BlogPostDto?>>;

public class GetLatestBlogPostsByAuthorIdQueryHandler : IRequestHandler<GetLatestBlogPostsByAuthorIdQuery, List<BlogPostDto?>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetLatestBlogPostsByAuthorIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    public async Task<List<BlogPostDto?>> Handle(GetLatestBlogPostsByAuthorIdQuery request, CancellationToken cancellationToken)
    {
        var blogBosts = await _unitOfWork.BlogPostsRepository.GetLatestBlogPostsByAuthorIdAsync(request.AuthorId);
        var mappedBlogPosts = _mapper.Map<List<BlogPostDto?>>(blogBosts);

        return mappedBlogPosts;
    }
}