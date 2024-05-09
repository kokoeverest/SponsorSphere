using AutoMapper;
using MediatR;
using SponsorSphere.Application.App.Pictures.Dtos;
using SponsorSphere.Application.Interfaces;
using SponsorSphere.Domain.Models;

namespace SponsorSphere.Application.App.Pictures.Commands;

public record CreatePictureCommand(CreatePictureDto Picture) : IRequest<PictureDto>;
public class CreatePictureCommandHandler : IRequestHandler<CreatePictureCommand, PictureDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public CreatePictureCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    public async Task<PictureDto> Handle(CreatePictureCommand request, CancellationToken cancellationToken)
    {
        var picture = _mapper.Map<Picture>(request.Picture);

        await _unitOfWork.PicturesRepository.CreateAsync(picture);
        var mappedPicture = _mapper.Map<PictureDto>(picture);

        return await Task.FromResult(mappedPicture);
    }
}