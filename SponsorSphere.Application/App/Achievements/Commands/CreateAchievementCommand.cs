using AutoMapper;
using MediatR;
using SponsorSphere.Application.App.Achievements.Responses;
using SponsorSphere.Application.Interfaces;
using SponsorSphere.Domain.Enums;
using SponsorSphere.Domain.Models;

namespace SponsorSphere.Application.App.Achievements.Commands;

public record CreateAchievementCommand(
        string Name,
        string Country,
        string EventDate,
        EventsEnum EventType,
        SportsEnum Sport,
        ushort? PlaceFinished,
        int AthleteId
    ) : IRequest<AchievementDto>;

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
        if (DateTime.UtcNow < DateTime.Parse(request.EventDate))
        {
            throw new InvalidDataException("You can't create an achievement in the future");
        }

        try
        {
            await _unitOfWork.BeginTransactionAsync();

            var sportEvent = await _unitOfWork.SportEventsRepository
                                        .CreateAsync(new SportEvent
                                        {
                                            Name = request.Name,
                                            Country = request.Country,
                                            EventDate = DateTime.Parse(request.EventDate),
                                            Finished = true,
                                            EventType = request.EventType,
                                            Sport = request.Sport
                                        });

            var newAchievement = await _unitOfWork.AchievementsRepository
                                            .CreateAsync(new Achievement
                                            {
                                                Sport = request.Sport,
                                                SportEventId = sportEvent.Id,
                                                AthleteId = request.AthleteId,
                                                PlaceFinished = request.PlaceFinished
                                            });

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
