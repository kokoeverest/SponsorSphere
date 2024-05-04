using MediatR;
using SponsorSphere.Application.Interfaces;

namespace SponsorSphere.Application.App.Goals.Commands;

public record DeleteGoalCommand(int AthleteId, int SportEventId) : IRequest<int>;
public class DeleteGoalCommandHandler : IRequestHandler<DeleteGoalCommand, int>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteGoalCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<int> Handle(DeleteGoalCommand request, CancellationToken cancellationToken) => 
        
        await _unitOfWork.GoalsRepository.DeleteAsync(request.SportEventId, request.AthleteId);
}