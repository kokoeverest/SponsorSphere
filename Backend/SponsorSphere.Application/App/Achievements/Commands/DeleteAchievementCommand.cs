using MediatR;
using Microsoft.Extensions.Logging;
using SponsorSphere.Application.Common.Constants;
using SponsorSphere.Application.Interfaces;

namespace SponsorSphere.Application.App.Achievements.Commands;

public record DeleteAchievementCommand(int SportEventId, int AthleteId) : IRequest;
public class DeleteAchievementCommandHandler : IRequestHandler<DeleteAchievementCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<DeleteAchievementCommandHandler> _logger;

    public DeleteAchievementCommandHandler(IUnitOfWork unitOfWork, ILogger<DeleteAchievementCommandHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task Handle(DeleteAchievementCommand request, CancellationToken cancellationToken)
    {
        var start = DateTime.Now;
        _logger.LogInformation(LoggingConstants.logStartString, request.ToString());

        try
        {
            await _unitOfWork.BeginTransactionAsync();
            await _unitOfWork.AchievementsRepository.DeleteAsync(request.SportEventId, request.AthleteId);
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