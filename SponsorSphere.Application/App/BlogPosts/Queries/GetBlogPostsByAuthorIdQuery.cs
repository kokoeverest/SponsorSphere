using AutoMapper;
using MediatR;
using SponsorSphere.Application.App.BlogPosts.Responses;
using SponsorSphere.Application.Interfaces;

namespace SponsorSphere.Application.App.BlogPosts.Queries;

public record GetBlogPostByAuthorIdQuery(int PageNumber, int PageSize, int AuthorId) : IRequest<ICollection<BlogPostDto?>>;

public class GetBlogPostByAuthorIdQueryHandler : IRequestHandler<GetBlogPostByAuthorIdQuery, ICollection<BlogPostDto?>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetBlogPostByAuthorIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    public async Task<ICollection<BlogPostDto?>> Handle(GetBlogPostByAuthorIdQuery request, CancellationToken cancellationToken)
    {
        var blogBost = await _unitOfWork.BlogPostsRepository.GetBlogPostsByAuthorIdAsync(request.PageNumber, request.PageSize, request.AuthorId);
        var mappedBlogPost = _mapper.Map<ICollection<BlogPostDto?>>(blogBost);

        return mappedBlogPost;
    }
}