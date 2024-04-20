using AutoMapper;
using MediatR;
using SponsorSphere.Application.App.Athletes.Responses;
using SponsorSphere.Domain.Enums;
using SponsorSphere.Infrastructure.Interfaces;

namespace SponsorSphere.Application.App.Athletes.Queries;

public record GetAthletesBySportQuery(SportsEnum Sport) : IRequest<List<AthleteDto>>;

public class GetAthletesBySportHandler : IRequestHandler<GetAthletesBySportQuery, List<AthleteDto>>
{
    private readonly IAthleteRepository _athleteRepository;
    private readonly IMapper _mapper;

    public GetAthletesBySportHandler(IAthleteRepository athleteRepository, IMapper mapper)
    {
        _athleteRepository = athleteRepository;
        _mapper = mapper;
    }

    public async Task<List<AthleteDto>> Handle(GetAthletesBySportQuery request, CancellationToken cancellationToken)
    {
        var athletes = await _athleteRepository.GetBySportAsync(request.Sport);
        var mappedAthletes = _mapper.Map<List<AthleteDto>>(athletes);

        return await Task.FromResult(mappedAthletes);
    }
}