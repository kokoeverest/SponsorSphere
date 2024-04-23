using MediatR;
using SponsorSphere.Application.Interfaces;

namespace SponsorSphere.Application.App.SponsorIndividuals.Commands;
public record DeleteSponsorIndividualCommand(int SponsorIndividualId) : IRequest<int>;

public class DeleteSponsorIndividualCommandHandler : IRequestHandler<DeleteSponsorIndividualCommand, int>
{
    private readonly IUnitOfWork _unitOfWork;
    public DeleteSponsorIndividualCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<int> Handle(DeleteSponsorIndividualCommand request, CancellationToken cancellationToken)
    {
        return await _unitOfWork.SponsorIndividualsRepository.DeleteAsync(request.SponsorIndividualId);
    }
}