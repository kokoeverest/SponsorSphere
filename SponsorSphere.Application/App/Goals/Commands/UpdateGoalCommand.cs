using MediatR;
using SponsorSphere.Application.App.Goals.Responses;
using SponsorSphere.Application.Interfaces;

namespace SponsorSphere.Application.App.Goals.Commands;

public record UpdateGoalCommand(GoalDto GoalToUpdate) : IRequest<GoalDto>;
public class UpdateGoalCommandHandler : IRequestHandler<UpdateGoalCommand, GoalDto>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateGoalCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<GoalDto> Handle(UpdateGoalCommand request, CancellationToken cancellationToken)
    {
        if (DateTime.UtcNow > request.GoalToUpdate.Date.ToUniversalTime())
        {
            throw new InvalidDataException("You can't create a goal in the past");
        }

        try
        {
            await _unitOfWork.BeginTransactionAsync();
            var result = await _unitOfWork.GoalsRepository.UpdateAsync(request.GoalToUpdate);
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