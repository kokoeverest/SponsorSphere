using AutoMapper;
using MediatR;
using SponsorSphere.Application.App.Achievements.Dtos;
using SponsorSphere.Application.Interfaces;

namespace SponsorSphere.Application.App.Achievements.Queries;

public record GetAchievementsByAthleteIdQuery(int AthleteId) : IRequest<List<AchievementDto>>;

public class GetAchievementsByAthleteIdQueryHandler : IRequestHandler<GetAchievementsByAthleteIdQuery, List<AchievementDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetAchievementsByAthleteIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<List<AchievementDto>> Handle(GetAchievementsByAthleteIdQuery request, CancellationToken cancellationToken)
    {
        var achievements = await _unitOfWork.AchievementsRepository.GetAllAsync(request.AthleteId);
        var mappedAchievements = _mapper.Map<List<AchievementDto>>(achievements);

        return mappedAchievements;
    }
}

