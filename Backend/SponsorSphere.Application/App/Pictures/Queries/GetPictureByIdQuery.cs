using AutoMapper;
using MediatR;
using SponsorSphere.Application.App.Pictures.Dtos;
using SponsorSphere.Application.Interfaces;

namespace SponsorSphere.Application.App.Pictures.Queries;
public record GetPictureByIdQuery(int PictureId) : IRequest<PictureDto?>;

public class GetPictureByIdQueryHandler : IRequestHandler<GetPictureByIdQuery, PictureDto?>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetPictureByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    public async Task<PictureDto?> Handle(GetPictureByIdQuery request, CancellationToken cancellationToken)
    {
        var picture = await _unitOfWork.PicturesRepository.GetByIdAsync(request.PictureId);
        var mappedPicture = _mapper.Map<PictureDto>(picture);

        return mappedPicture;
    }
}