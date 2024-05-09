using AutoMapper;
using MediatR;
using SponsorSphere.Application.App.Goals.Dtos;
using SponsorSphere.Application.Interfaces;
using SponsorSphere.Domain.Models;
using System.Reflection;

namespace SponsorSphere.Application.App.Goals.Commands;

public record CreateGoalCommand(CreateGoalDto Model, int AthleteId) : IRequest<GoalDto>;

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
        if (DateTime.UtcNow > DateTime.Parse(request.Model.EventDate).ToUniversalTime())
        {
            throw new InvalidDataException("You can't create a goal in the past");
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

        try
        {
            await _unitOfWork.BeginTransactionAsync();
            await _unitOfWork.SportEventsRepository.CreateAsync(sportEvent);

            var goal = new Goal
            {
                Sport = request.Model.Sport,
                SportEventId = sportEvent.Id,
                AthleteId = request.AthleteId,
                Date = sportEvent.EventDate,
                AmountNeeded = request.Model.AmountNeeded
            };
            
            await _unitOfWork.GoalsRepository.CreateAsync(goal);
            await _unitOfWork.CommitTransactionAsync();

            var mappedGoal = _mapper.Map<GoalDto>(goal);

            return await Task.FromResult(mappedGoal);
        }

        catch (Exception)
        {
            await _unitOfWork.RollbackTransactionAsync();
            throw;
        }
    }
}