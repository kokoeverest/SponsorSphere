using AutoMapper;
using MediatR;
using SponsorSphere.Application.App.Sponsorships.Dtos;
using SponsorSphere.Application.Interfaces;

namespace SponsorSphere.Application.App.Sponsorships.Queries;

public record GetSponsorshipsByAthleteIdQuery(int AthleteId) : IRequest<List<SponsorshipDto>>;

public class GetSponsorshipsByAthleteIdQueryHandler : IRequestHandler<GetSponsorshipsByAthleteIdQuery, List<SponsorshipDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetSponsorshipsByAthleteIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<List<SponsorshipDto>> Handle(GetSponsorshipsByAthleteIdQuery request, CancellationToken cancellationToken)
    {
        var sponsorships = await _unitOfWork.SponsorshipsRepository.GetByAthleteIdAsync(request.AthleteId);
        var mappedSponsorships = _mapper.Map<List<SponsorshipDto>>(sponsorships);
        return mappedSponsorships;
    }
}
