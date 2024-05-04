using MediatR;
using SponsorSphere.Application.Interfaces;

namespace SponsorSphere.Application.App.Achievements.Commands;

public record DeleteAchievementCommand(int SportEventId, int AthleteId) : IRequest<int>;
public class DeleteAchievementCommandHandler : IRequestHandler<DeleteAchievementCommand, int>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteAchievementCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<int> Handle(DeleteAchievementCommand request, CancellationToken cancellationToken) =>
        
        await _unitOfWork.AchievementsRepository.DeleteAsync(request.SportEventId, request.AthleteId);
}