using AutoMapper;
using MediatR;
using SponsorSphere.Application.App.Athletes.Responses;
using SponsorSphere.Infrastructure.Interfaces;

namespace SponsorSphere.Application.App.Athletes.Queries;

public record GetAllAthletes : IRequest<List<AthleteDto>>;

public class GetAllAthletesHandler : IRequestHandler<GetAllAthletes, List<AthleteDto>>
{
    private readonly IAthleteRepository _athleteRepository;
    private readonly IMapper _mapper;

    public GetAllAthletesHandler(IAthleteRepository athleteRepository, IMapper mapper)
    {
        _athleteRepository = athleteRepository;
        _mapper = mapper;
    }

    public Task<List<AthleteDto>> Handle(GetAllAthletes request, CancellationToken cancellationToken)
    {
        var athletes = _athleteRepository.GetAll();
        var mappedAthletes = _mapper.Map<List<AthleteDto>>(athletes);

        return Task.FromResult(mappedAthletes);
    }
}