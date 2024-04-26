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
        var updatedGoal = await _unitOfWork.GoalsRepository.UpdateAsync(request.GoalToUpdate);

        return await Task.FromResult(updatedGoal);
    }
}