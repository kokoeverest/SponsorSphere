using AutoMapper;
using MediatR;
using SponsorSphere.Application.App.Sponsors.Responses;
using SponsorSphere.Application.Interfaces;

namespace SponsorSphere.Application.App.Sponsors.Queries;

public record GetSponsorsByMoneyProvidedQuery() : IRequest<List<SponsorDto>>;

public class GetSponsorsByMoneyProvidedQueryHandler : IRequestHandler<GetSponsorsByMoneyProvidedQuery, List<SponsorDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetSponsorsByMoneyProvidedQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<List<SponsorDto>> Handle(GetSponsorsByMoneyProvidedQuery request, CancellationToken cancellationToken)
    {
        var sponsors = await _unitOfWork.SponsorRepository.GetByMoneyProvidedAsync();
        var mappedSponsors = _mapper.Map<List<SponsorDto>>(sponsors);

        return await Task.FromResult(mappedSponsors);
    }
}