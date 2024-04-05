using MediatR;
using SponsorSphere.Application.Athletes.Responses;
using SponsorSphere.Infrastructure.Interfaces;

namespace SponsorSphere.Application;

public record GetAllAthletes : IRequest<List<AthleteDto>>;

public class GetAllAthletesHandler : IRequestHandler<GetAllAthletes, List<AthleteDto>>
{
    private readonly IAthleteRepository _athleteRepository;

    public GetAllAthletesHandler(IAthleteRepository athleteRepository)
    {
        _athleteRepository = athleteRepository;
    }

    public Task<List<AthleteDto>> Handle(GetAllAthletes request, CancellationToken cancellationToken)
    {
        var athletes = _athleteRepository.GetAll();
        return Task.FromResult(athletes.Select(AthleteDto.FromAthlete).ToList());
    }
}