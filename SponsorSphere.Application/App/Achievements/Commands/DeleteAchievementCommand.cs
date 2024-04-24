using MediatR;
using SponsorSphere.Application.Interfaces;

namespace SponsorSphere.Application.App.Achievements.Commands;

public record DeleteAchievementCommand(int AthleteId, int SportEventId) : IRequest<int>;
public class DeleteAchievementCommandHandler : IRequestHandler<DeleteAchievementCommand, int>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteAchievementCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<int> Handle(DeleteAchievementCommand request, CancellationToken cancellationToken)
    {
        return await _unitOfWork.AchievementsRepository.DeleteAsync(request.SportEventId, request.AthleteId);
    }
}