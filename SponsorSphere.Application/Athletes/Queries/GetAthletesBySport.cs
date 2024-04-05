using MediatR;
using SponsorSphere.Application.Athletes.Responses;
using SponsorSphere.Domain.Enums;
using SponsorSphere.Infrastructure.Interfaces;

namespace SponsorSphere.Application;

public record GetAthletesBySport(SportsEnum Sport) : IRequest<List<AthleteDto>>;

public class GetAthletesBySportHandler : IRequestHandler<GetAthletesBySport, List<AthleteDto>>
{
    private readonly IAthleteRepository _athleteRepository;

    public GetAthletesBySportHandler(IAthleteRepository athleteRepository)
    {
        _athleteRepository = athleteRepository;
    }

    public Task<List<AthleteDto>> Handle(GetAthletesBySport request, CancellationToken cancellationToken)
    {
        var athletes = _athleteRepository.GetBySport(request.Sport);
        return Task.FromResult(athletes.Select(AthleteDto.FromAthlete).ToList());
    }
}