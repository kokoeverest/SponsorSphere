using AutoMapper;
using MediatR;
using SponsorSphere.Application.App.BlogPosts.Responses;
using SponsorSphere.Application.Interfaces;

namespace SponsorSphere.Application.App.BlogPosts.Queries;

public record GetBlogPostByAuthorIdQuery(int AuthorId) : IRequest<List<BlogPostDto?>>;

public class GetBlogPostByAuthorIdQueryHandler : IRequestHandler<GetBlogPostByAuthorIdQuery, List<BlogPostDto?>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetBlogPostByAuthorIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    public async Task<List<BlogPostDto?>> Handle(GetBlogPostByAuthorIdQuery request, CancellationToken cancellationToken)
    {
        var blogBost = await _unitOfWork.BlogPostsRepository.GetBlogPostsByAuthorIdAsync(request.AuthorId);
        var mappedBlogPost = _mapper.Map<List<BlogPostDto?>>(blogBost);

        return mappedBlogPost;
    }
}