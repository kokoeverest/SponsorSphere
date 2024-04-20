using AutoMapper;
using MediatR;
using SponsorSphere.Application.App.Athletes.Responses;
using SponsorSphere.Infrastructure.Interfaces;

namespace SponsorSphere.Application.App.Athletes.Queries;

public record GetAllAthletesQuery : IRequest<List<AthleteDto>>;

public class GetAllAthletesHandler : IRequestHandler<GetAllAthletesQuery, List<AthleteDto>>
{
    private readonly IAthleteRepository _athleteRepository;
    private readonly IMapper _mapper;

    public GetAllAthletesHandler(IAthleteRepository athleteRepository, IMapper mapper)
    {
        _athleteRepository = athleteRepository;
        _mapper = mapper;
    }

    public async Task<List<AthleteDto>> Handle(GetAllAthletesQuery request, CancellationToken cancellationToken)
    {
        var athletes = await _athleteRepository.GetAllAsync();
        var mappedAthletes = _mapper.Map<List<AthleteDto>>(athletes);

        return await Task.FromResult(mappedAthletes);
    }
}