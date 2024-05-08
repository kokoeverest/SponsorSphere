using MediatR;
using SponsorSphere.Application.Interfaces;

namespace SponsorSphere.Application.App.Goals.Commands;

public record DeleteGoalCommand(int AthleteId, int SportEventId) : IRequest;
public class DeleteGoalCommandHandler : IRequestHandler<DeleteGoalCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteGoalCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(DeleteGoalCommand request, CancellationToken cancellationToken)
    {
        try
        {
            await _unitOfWork.BeginTransactionAsync();
            await _unitOfWork.GoalsRepository.DeleteAsync(request.SportEventId, request.AthleteId);
            await _unitOfWork.CommitTransactionAsync();
        }

        catch (Exception)
        {
            await _unitOfWork.RollbackTransactionAsync();
            throw;
        }
    }
        
}