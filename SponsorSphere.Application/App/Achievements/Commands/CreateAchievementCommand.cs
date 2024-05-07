using AutoMapper;
using MediatR;
using SponsorSphere.Application.App.Achievements.Responses;
using SponsorSphere.Application.Interfaces;
using SponsorSphere.Domain.Enums;
using SponsorSphere.Domain.Models;

namespace SponsorSphere.Application.App.Achievements.Commands;

public record CreateAchievementCommand(SportEvent SportEvent, Achievement Achievement) : IRequest<AchievementDto>;

public class CreateAchievementCommandHandler : IRequestHandler<CreateAchievementCommand, AchievementDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateAchievementCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<AchievementDto> Handle(CreateAchievementCommand request, CancellationToken cancellationToken)
    {
        try
        {
            await _unitOfWork.BeginTransactionAsync();

            var sportEvent = await _unitOfWork.SportEventsRepository.CreateAsync(request.SportEvent);
            request.Achievement.SportEventId = sportEvent.Id;
            var newAchievement = await _unitOfWork.AchievementsRepository.CreateAsync(request.Achievement);

            await _unitOfWork.CommitTransactionAsync();
            var mappedAchievement = _mapper.Map<AchievementDto>(newAchievement);

            return await Task.FromResult(mappedAchievement);
        }

        catch (Exception)
        {
            await _unitOfWork.RollbackTransactionAsync();
            throw;
        }
    }
}
