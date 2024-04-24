using MediatR;
using SponsorSphere.Application.Interfaces;

namespace SponsorSphere.Application.App.Sponsorships.Commands;

public record DeleteSponsorshipCommand(int AthleteId, int SponsorId) : IRequest<int>;
public class DeleteSponsorshipCommandHandler : IRequestHandler<DeleteSponsorshipCommand, int>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteSponsorshipCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<int> Handle(DeleteSponsorshipCommand request, CancellationToken cancellationToken)
    {
        return await _unitOfWork.SponsorshipsRepository.DeleteAsync(request.AthleteId, request.SponsorId);
    }
}
