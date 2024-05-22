using MediatR;
using Microsoft.Extensions.Logging;
using SponsorSphere.Application.Common.Constants;
using SponsorSphere.Application.Interfaces;

namespace SponsorSphere.Application.App.Goals.Commands;

public record DeleteGoalCommand(int AthleteId, int SportEventId) : IRequest;
public class DeleteGoalCommandHandler : IRequestHandler<DeleteGoalCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<DeleteGoalCommandHandler> _logger;

    public DeleteGoalCommandHandler(IUnitOfWork unitOfWork, ILogger<DeleteGoalCommandHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task Handle(DeleteGoalCommand request, CancellationToken cancellationToken)
    {
        var start = DateTime.Now;
        _logger.LogInformation(LoggingConstants.logStartString, request.ToString());

        try
        {
            await _unitOfWork.BeginTransactionAsync();
            await _unitOfWork.GoalsRepository.DeleteAsync(request.SportEventId, request.AthleteId);
            await _unitOfWork.CommitTransactionAsync();
            _logger.LogInformation(LoggingConstants.logEndString, request.ToString(), (DateTime.Now - start).TotalMilliseconds);
        }

        catch (Exception)
        {
            await _unitOfWork.RollbackTransactionAsync();
            throw;
        }
    }
        
}