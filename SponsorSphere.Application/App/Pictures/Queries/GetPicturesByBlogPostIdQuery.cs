using AutoMapper;
using MediatR;
using SponsorSphere.Application.App.Pictures.Responses;
using SponsorSphere.Application.Interfaces;

namespace SponsorSphere.Application.App.Pictures.Queries;

public record GetPictureByBlogPostIdQuery(int PictureId) : IRequest<PictureDto?>;

public class GetPictureByIdQueryHandler : IRequestHandler<GetPictureByBlogPostIdQuery, PictureDto?>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetPictureByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    public async Task<PictureDto?> Handle(GetPictureByBlogPostIdQuery request, CancellationToken cancellationToken)
    {
        var picture = await _unitOfWork.PicturesRepository.GetByBlogPostIdAsync(request.PictureId);
        var mappedPicture = _mapper.Map<PictureDto>(picture);

        return mappedPicture;
    }
}
