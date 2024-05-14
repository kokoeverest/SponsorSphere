using MediatR;
using Microsoft.Extensions.Logging;
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
        _logger.LogInformation("Action: {Action}", request.ToString());

        try
        {
            await _unitOfWork.BeginTransactionAsync();
            await _unitOfWork.GoalsRepository.DeleteAsync(request.SportEventId, request.AthleteId);
            await _unitOfWork.CommitTransactionAsync();
            _logger.LogInformation("Action: {Action}, ({DT})ms", request.ToString(), (DateTime.Now - start).TotalMilliseconds);
        }

        catch (Exception)
        {
            await _unitOfWork.RollbackTransactionAsync();
            _logger.LogError("Action: {Action} failed", request.ToString());
            throw;
        }
    }
        
}