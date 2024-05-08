using MediatR;
using SponsorSphere.Application.App.Pictures.Responses;
using SponsorSphere.Application.Interfaces;

namespace SponsorSphere.Application.App.Pictures.Commands;

public record DeletePictureCommand(PictureDto Picture) : IRequest;
public class DeletePictureCommandHandler : IRequestHandler<DeletePictureCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeletePictureCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(DeletePictureCommand request, CancellationToken cancellationToken)
    {
        try
        {
            await _unitOfWork.BeginTransactionAsync();
            await _unitOfWork.PicturesRepository.DeleteAsync(request.Picture);
            await _unitOfWork.CommitTransactionAsync();
        }

        catch (Exception)
        {
            await _unitOfWork.RollbackTransactionAsync();
            throw;
        }
    }
}

