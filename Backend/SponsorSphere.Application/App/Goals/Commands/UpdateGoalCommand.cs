using MediatR;
using Microsoft.Extensions.Logging;
using SponsorSphere.Application.App.Goals.Dtos;
using SponsorSphere.Application.Common.Constants;
using SponsorSphere.Application.Interfaces;

namespace SponsorSphere.Application.App.Goals.Commands;

public record UpdateGoalCommand(GoalDto GoalToUpdate) : IRequest<GoalDto>;
public class UpdateGoalCommandHandler : IRequestHandler<UpdateGoalCommand, GoalDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<UpdateGoalCommandHandler> _logger;

    public UpdateGoalCommandHandler(IUnitOfWork unitOfWork, ILogger<UpdateGoalCommandHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<GoalDto> Handle(UpdateGoalCommand request, CancellationToken cancellationToken)
    {
        if (DateTime.UtcNow > request.GoalToUpdate.Date.ToUniversalTime())
        {
            throw new InvalidDataException("You can't create a goal in the past");
        }

        var start = DateTime.Now;
        _logger.LogInformation(LoggingConstants.logStartString, request.ToString());

        try
        {
            await _unitOfWork.BeginTransactionAsync();
            var result = await _unitOfWork.GoalsRepository.UpdateAsync(request.GoalToUpdate);
            await _unitOfWork.CommitTransactionAsync();

            _logger.LogInformation(LoggingConstants.logEndString, request.ToString(), (DateTime.Now - start).TotalMilliseconds);
            return result;
        }

        catch (Exception)
        {
            await _unitOfWork.RollbackTransactionAsync();
            throw;
        }
    } 
}