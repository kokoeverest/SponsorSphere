using MediatR;
using SponsorSphere.Application.App.Achievements.Responses;
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
        var updatedAchievement = await _unitOfWork.AchievementsRepository.UpdateAsync(request.AchievementToUpdate);

        return await Task.FromResult(updatedAchievement);
    }
}