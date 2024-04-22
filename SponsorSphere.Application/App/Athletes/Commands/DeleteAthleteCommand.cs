using MediatR;
using SponsorSphere.Application.Interfaces;

namespace SponsorSphere.Application.App.Athletes.Commands;

public record DeleteAthleteCommand(int AthleteId) : IRequest<int>;

public class DeleteAthleteCommandHandler : IRequestHandler<DeleteAthleteCommand, int>
{
    private readonly IUnitOfWork _unitOfWork;
    public DeleteAthleteCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<int> Handle(DeleteAthleteCommand request, CancellationToken cancellationToken)
    {
        return await _unitOfWork.AthletesRepository.DeleteAsync(request.AthleteId);
    }
}