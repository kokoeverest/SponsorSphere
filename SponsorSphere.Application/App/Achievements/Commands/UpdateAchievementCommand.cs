using MediatR;
using Microsoft.Extensions.Logging;
using SponsorSphere.Application.App.Achievements.Dtos;
using SponsorSphere.Application.App.SportEvents.Queries;
using SponsorSphere.Application.Common.Exceptions;
using SponsorSphere.Application.Interfaces;

namespace SponsorSphere.Application.App.Achievements.Commands;

public record UpdateAchievementCommand(AchievementDto AchievementToUpdate) : IRequest<AchievementDto>;
public class UpdateAchievementCommandHandler : IRequestHandler<UpdateAchievementCommand, AchievementDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<UpdateAchievementCommandHandler> _logger;

    public UpdateAchievementCommandHandler(IUnitOfWork unitOfWork, ILogger<UpdateAchievementCommandHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<AchievementDto> Handle(UpdateAchievementCommand request, CancellationToken cancellationToken)
    {
        var sportEvent = await _unitOfWork.SportEventsRepository.GetByIdAsync(request.AchievementToUpdate.SportEventId)
            ?? throw new InvalidDataException("Sport event not found. You should create it first.");
        
        if (DateTime.UtcNow < sportEvent.EventDate.ToUniversalTime())
        {
            throw new BadRequestException("The sport event can't be in the future");
        }

        try
        {
            var start = DateTime.Now;
            _logger.LogInformation("Action: {Action}", request.ToString());

            await _unitOfWork.BeginTransactionAsync();
            var result = await _unitOfWork.AchievementsRepository.UpdateAsync(request.AchievementToUpdate);
            await _unitOfWork.CommitTransactionAsync();
            
            _logger.LogInformation("Action: {Action}, ({DT})ms", request.ToString(), (DateTime.Now - start).TotalMilliseconds);
            return result;
        }
        catch (Exception)
        {
            await _unitOfWork.RollbackTransactionAsync();
            _logger.LogError("Action: {Action} failed", request.ToString());
            throw;
        }
    }
}