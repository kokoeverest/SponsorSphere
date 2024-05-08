using MediatR;
using SponsorSphere.Application.Interfaces;

namespace SponsorSphere.Application.App.Sponsorships.Commands;

public record DeleteSponsorshipCommand(int AthleteId, int SponsorId) : IRequest;
public class DeleteSponsorshipCommandHandler : IRequestHandler<DeleteSponsorshipCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteSponsorshipCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(DeleteSponsorshipCommand request, CancellationToken cancellationToken)
    {
        try
        {
            await _unitOfWork.BeginTransactionAsync();
            await _unitOfWork.SponsorshipsRepository.DeleteAsync(request.AthleteId, request.SponsorId);
            await _unitOfWork.CommitTransactionAsync();
        }

        catch (Exception)
        {
            await _unitOfWork.RollbackTransactionAsync();
            throw;
        }
    }
}
