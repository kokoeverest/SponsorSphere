using AutoMapper;
using MediatR;
using SponsorSphere.Application.App.Sponsors.Responses;
using SponsorSphere.Application.Interfaces;

namespace SponsorSphere.Application.App.Sponsors.Queries;

public record GetSponsorsByMostAthletesQuery() : IRequest<List<SponsorDto>>;

public class GetSponsorsByMostAthletesQueryHandler : IRequestHandler<GetSponsorsByMostAthletesQuery, List<SponsorDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetSponsorsByMostAthletesQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<List<SponsorDto>> Handle(GetSponsorsByMostAthletesQuery request, CancellationToken cancellationToken)
    {
        var sponsors = await _unitOfWork.SponsorsRepository.GetByMostAthletesAsync();
        var mappedSponsors = _mapper.Map<List<SponsorDto>>(sponsors);

        return await Task.FromResult(mappedSponsors);
    }
}