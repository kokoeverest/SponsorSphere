using AutoMapper;
using MediatR;
using SponsorSphere.Application.App.Goals.Responses;
using SponsorSphere.Application.Interfaces;
using SponsorSphere.Domain.Models;

namespace SponsorSphere.Application.App.Goals.Commands;

public record CreateGoalCommand(SportEvent SportEvent, Goal Goal) : IRequest<GoalDto>;

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
        try
        {
            await _unitOfWork.BeginTransactionAsync();

            var sportEvent = await _unitOfWork.SportEventsRepository.CreateAsync(request.SportEvent);

            var newGoal = await _unitOfWork.GoalsRepository.CreateAsync(request.Goal);

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