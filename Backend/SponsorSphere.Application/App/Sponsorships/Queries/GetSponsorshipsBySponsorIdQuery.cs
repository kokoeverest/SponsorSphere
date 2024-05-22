using AutoMapper;
using MediatR;
using SponsorSphere.Application.App.Sponsorships.Dtos;
using SponsorSphere.Application.Interfaces;

namespace SponsorSphere.Application.App.Sponsorships.Queries;

public record GetSponsorshipsBySponsorIdQuery(int SponsorId, int PageNumber, int PageSize) : IRequest<List<SponsorshipDto>>;

public class GetSponsorshipsBySponsorIdQueryHandler : IRequestHandler<GetSponsorshipsBySponsorIdQuery, List<SponsorshipDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetSponsorshipsBySponsorIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<List<SponsorshipDto>> Handle(GetSponsorshipsBySponsorIdQuery request, CancellationToken cancellationToken)
    {
        var sponsorships = await _unitOfWork.SponsorshipsRepository.GetBySponsorIdAsync(request.SponsorId, request.PageNumber, request.PageSize);
        var mappedSponsorships = _mapper.Map<List<SponsorshipDto>>(sponsorships);
        return mappedSponsorships;
    }
}