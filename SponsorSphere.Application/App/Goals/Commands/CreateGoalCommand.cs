using AutoMapper;
using MediatR;
using SponsorSphere.Application.App.Goals.Responses;
using SponsorSphere.Application.Interfaces;
using SponsorSphere.Domain.Enums;
using SponsorSphere.Domain.Models;

namespace SponsorSphere.Application.App.Goals.Commands;

public record CreateGoalCommand(
        string Name,
        CountryEnum Country,
        string EventDate,
        EventsEnum EventType,
        SportsEnum Sport,
        decimal AmountNeeded,
        int AthleteId
    ) : IRequest<GoalDto>;

public class CreateGoalCommandHandler : IRequestHandler<CreateGoalCommand, GoalDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateGoalCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<GoalDto> Handle(CreateGoalCommand request, CancellationToken cancellationToken)
    {
        if (DateTime.UtcNow > DateTime.Parse(request.EventDate).ToUniversalTime())
        {
            throw new InvalidDataException("You can't create a goal in the past");
        }

        try
        {
            await _unitOfWork.BeginTransactionAsync();

            var sportEvent = await _unitOfWork.SportEventsRepository
                                        .CreateAsync(new SportEvent
                                        {
                                            Name = request.Name,
                                            Country = request.Country,
                                            EventDate = DateTime.Parse(request.EventDate).ToUniversalTime(),
                                            Finished = false,
                                            EventType = request.EventType,
                                            Sport = request.Sport
                                        });

            var newGoal = await _unitOfWork.GoalsRepository
                                            .CreateAsync(new Goal
                                            {
                                                Date = sportEvent.EventDate,
                                                Sport = request.Sport,
                                                SportEventId = sportEvent.Id,
                                                AthleteId = request.AthleteId,
                                                AmountNeeded = request.AmountNeeded
                                            });

            await _unitOfWork.CommitTransactionAsync();
            var mappedGoal = _mapper.Map<GoalDto>(newGoal);

            return await Task.FromResult(mappedGoal);
        }

        catch (Exception)
        {
            await _unitOfWork.RollbackTransactionAsync();
            throw;
        }
    }
}