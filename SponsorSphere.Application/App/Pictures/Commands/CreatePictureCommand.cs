using AutoMapper;
using MediatR;
using SponsorSphere.Application.App.Pictures.Responses;
using SponsorSphere.Application.Interfaces;
using SponsorSphere.Domain.Models;

namespace SponsorSphere.Application.App.Pictures.Commands;

public record CreatePictureCommand(Picture Picture) : IRequest<PictureDto>;
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
        var newPicture = await _unitOfWork.PicturesRepository.CreateAsync(request.Picture);
        var mappedPicture = _mapper.Map<PictureDto>(newPicture);

        return await Task.FromResult(mappedPicture);
    }
}