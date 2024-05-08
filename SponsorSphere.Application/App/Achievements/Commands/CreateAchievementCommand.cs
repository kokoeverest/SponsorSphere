using AutoMapper;
using MediatR;
using SponsorSphere.Application.App.Achievements.Responses;
using SponsorSphere.Application.Interfaces;
using SponsorSphere.Domain.Models;

namespace SponsorSphere.Application.App.Achievements.Commands;

public record CreateAchievementCommand(CreateAchievementDto Model, int AthleteId) : IRequest<AchievementDto>;

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
        if (DateTime.UtcNow < DateTime.Parse(request.Model.EventDate).ToUniversalTime())
        {
            throw new ApplicationException("You can't create an achievement in the future");
        }

        var sportEvent = new SportEvent
        {
            Name = request.Model.Name,
            Country = request.Model.Country,
            EventDate = DateTime.Parse(request.Model.EventDate).ToUniversalTime(),
            Finished = true,
            EventType = request.Model.EventType,
            Sport = request.Model.Sport
        };

        var achievement = new Achievement
        {
            Sport = request.Model.Sport,
            SportEventId = sportEvent.Id,
            AthleteId = request.AthleteId,
            PlaceFinished = request.Model.PlaceFinished
        };


        try
        {
            await _unitOfWork.BeginTransactionAsync();

            await _unitOfWork.SportEventsRepository.CreateAsync(sportEvent);
            achievement.SportEventId = sportEvent.Id;
            var newAchievement = await _unitOfWork.AchievementsRepository.CreateAsync(achievement);

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
