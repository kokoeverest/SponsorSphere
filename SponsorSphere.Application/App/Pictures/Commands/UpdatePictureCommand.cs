using MediatR;
using SponsorSphere.Application.App.Pictures.Dtos;
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

    public async Task<PictureDto> Handle(UpdatePictureCommand request, CancellationToken cancellationToken)
    {
        try
        {
            await _unitOfWork.BeginTransactionAsync();
            var result = await _unitOfWork.PicturesRepository.UpdateAsync(request.PictureToUpdate);
            await _unitOfWork.CommitTransactionAsync();

            return result;
        }

        catch (Exception)
        {
            await _unitOfWork.RollbackTransactionAsync();
            throw;
        }
    }
}
