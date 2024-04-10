using AutoMapper;
using MediatR;
using SponsorSphere.Application.App.Athletes.Responses;
using SponsorSphere.Domain.Enums;
using SponsorSphere.Infrastructure.Interfaces;

namespace SponsorSphere.Application.App.Athletes.Queries;

public record GetAthletesBySport(SportsEnum Sport) : IRequest<List<AthleteDto>>;

public class GetAthletesBySportHandler : IRequestHandler<GetAthletesBySport, List<AthleteDto>>
{
    private readonly IAthleteRepository _athleteRepository;
    private readonly IMapper _mapper;

    public GetAthletesBySportHandler(IAthleteRepository athleteRepository, IMapper mapper)
    {
        _athleteRepository = athleteRepository;
        _mapper = mapper;
    }

    public Task<List<AthleteDto>> Handle(GetAthletesBySport request, CancellationToken cancellationToken)
    {
        var athletes = _athleteRepository.GetBySport(request.Sport);
        var mappedAthletes = _mapper.Map<List<AthleteDto>>(athletes);

        return Task.FromResult(mappedAthletes);
    }
}