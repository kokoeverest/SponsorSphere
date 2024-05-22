using AutoMapper;
using MediatR;
using SponsorSphere.Application.App.BlogPosts.Dtos;
using SponsorSphere.Application.Interfaces;

namespace SponsorSphere.Application.App.BlogPosts.Queries;

public record GetBlogPostByIdQuery(int BlogPostId) : IRequest<BlogPostDto?>;

public class GetBlogPostByIdQueryHandler : IRequestHandler<GetBlogPostByIdQuery, BlogPostDto?>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetBlogPostByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    public async Task<BlogPostDto?> Handle(GetBlogPostByIdQuery request, CancellationToken cancellationToken)
    {
        var blogBost = await _unitOfWork.BlogPostsRepository.GetByIdAsync(request.BlogPostId);
        var mappedBlogPost = _mapper.Map<BlogPostDto>(blogBost);

        return mappedBlogPost;
    }
}
