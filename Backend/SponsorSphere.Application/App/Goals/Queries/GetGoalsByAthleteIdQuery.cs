using AutoMapper;
using MediatR;
using SponsorSphere.Application.App.Goals.Dtos;
using SponsorSphere.Application.Interfaces;

namespace SponsorSphere.Application.App.Goals.Queries;

public record GetGoalsByAthleteIdQuery(int AthleteId, int PageNumber, int PageSize) : IRequest<ICollection<GoalDto>>;

public class GetGoalsByAthleteIdQueryHandler : IRequestHandler<GetGoalsByAthleteIdQuery, ICollection<GoalDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetGoalsByAthleteIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ICollection<GoalDto>> Handle(GetGoalsByAthleteIdQuery request, CancellationToken cancellationToken)
    {
        var achievements = await _unitOfWork.GoalsRepository.GetAllAsync(request.AthleteId, request.PageNumber, request.PageSize);
        var mappedAchievements = _mapper.Map<ICollection<GoalDto>>(achievements);

        return mappedAchievements;
    }
}