using MediatR;
using SponsorSphere.Application.App.Pictures.Responses;
using SponsorSphere.Application.App.SportEvents.Responses;
using SponsorSphere.Application.Interfaces;

namespace SponsorSphere.Application.App.Pictures.Commands;

public record UpdatePictureCommand(PictureDto PictureToUpdate) : IRequest<PictureDto>;
public class UpdatePictureCommandHandler : IRequestHandler<UpdatePictureCommand, PictureDto>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdatePictureCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<PictureDto> Handle(UpdatePictureCommand request, CancellationToken cancellationToken) => 
        
        await _unitOfWork.PicturesRepository.UpdateAsync(request.PictureToUpdate);
}
