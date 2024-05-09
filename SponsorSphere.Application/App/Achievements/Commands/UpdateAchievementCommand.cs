using MediatR;
using SponsorSphere.Application.App.Achievements.Dtos;
using SponsorSphere.Application.App.SportEvents.Queries;
using SponsorSphere.Application.Interfaces;

namespace SponsorSphere.Application.App.Achievements.Commands;

public record UpdateAchievementCommand(AchievementDto AchievementToUpdate) : IRequest<AchievementDto>;
public class UpdateAchievementCommandHandler : IRequestHandler<UpdateAchievementCommand, AchievementDto>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateAchievementCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<AchievementDto> Handle(UpdateAchievementCommand request, CancellationToken cancellationToken)
    {
        var sportEvent = await _unitOfWork.SportEventsRepository.GetByIdAsync(request.AchievementToUpdate.SportEventId);

        if (sportEvent is null)
        {
            throw new InvalidDataException("Sport event not found. You should create it first.");
        }

        if (DateTime.UtcNow < sportEvent.EventDate.ToUniversalTime())
        {
            throw new ApplicationException("The sport event can't be in the future");
        }
        try
        {
            await _unitOfWork.BeginTransactionAsync();
            var result = await _unitOfWork.AchievementsRepository.UpdateAsync(request.AchievementToUpdate);
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