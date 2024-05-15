using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
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
    private readonly ILogger<CreateGoalCommandHandler> _logger;

    public CreateGoalCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<CreateGoalCommandHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<GoalDto> Handle(CreateGoalCommand request, CancellationToken cancellationToken)
    {
        var start = DateTime.Now;
        _logger.LogInformation("Action: {Action}", request.ToString());

        var sportEvent = await _unitOfWork.SportEventsRepository.GetByIdAsync(request.Model.SportEventId);
        
        if (DateTime.UtcNow > sportEvent.EventDate)
        {
            throw new InvalidDataException("You can't create a goal in the past");
        }

        var goal = new Goal
        {
            Sport = sportEvent.Sport,
            SportEventId = sportEvent.Id,
            AthleteId = request.AthleteId,
            Date = sportEvent.EventDate,
            AmountNeeded = request.Model.AmountNeeded
        };

        try
        {
            await _unitOfWork.BeginTransactionAsync();
            await _unitOfWork.GoalsRepository.CreateAsync(goal);
            await _unitOfWork.CommitTransactionAsync();

            var mappedGoal = _mapper.Map<GoalDto>(goal);

            _logger.LogInformation("Action: {Action}, ({DT})ms", request.ToString(), (DateTime.Now - start).TotalMilliseconds);
            return await Task.FromResult(mappedGoal);
        }

        catch (Exception)
        {
            await _unitOfWork.RollbackTransactionAsync();
            _logger.LogError("Action: {Action} failed", request.ToString());
            throw;
        }
    }
}