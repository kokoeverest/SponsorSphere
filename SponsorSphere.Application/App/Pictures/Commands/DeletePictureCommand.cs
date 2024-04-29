using MediatR;
using SponsorSphere.Application.App.Pictures.Responses;
using SponsorSphere.Application.Interfaces;

namespace SponsorSphere.Application.App.Pictures.Commands;

public record DeletePictureCommand(PictureDto Picture) : IRequest<int>;
public class DeletePictureCommandHandler : IRequestHandler<DeletePictureCommand, int>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeletePictureCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<int> Handle(DeletePictureCommand request, CancellationToken cancellationToken)
    {
        return await _unitOfWork.PicturesRepository.DeleteAsync(request.Picture);
    }
}

