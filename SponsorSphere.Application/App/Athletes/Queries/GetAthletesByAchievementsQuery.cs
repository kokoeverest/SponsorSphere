using AutoMapper;
using MediatR;
using SponsorSphere.Application.App.Athletes.Responses;
using SponsorSphere.Application.Interfaces;

namespace SponsorSphere.Application.App.Athletes.Queries;

public record GetAthletesByAchievementsQuery : IRequest<List<AthleteDto>>;

public class GetAthletesByAchievementsQueryHandler : IRequestHandler<GetAthletesByAchievementsQuery, List<AthleteDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetAthletesByAchievementsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<List<AthleteDto>> Handle(GetAthletesByAchievementsQuery request, CancellationToken cancellationToken)
    {
        var athletes = await _unitOfWork.AthletesRepository.GetByAchievementsAsync();
        var mappedAthletes = _mapper.Map<List<AthleteDto>>(athletes);

        return await Task.FromResult(mappedAthletes);
    }
}