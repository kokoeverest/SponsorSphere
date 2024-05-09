using AutoMapper;
using MediatR;
using SponsorSphere.Application.App.SponsorIndividuals.Dtos;
using SponsorSphere.Application.Interfaces;

namespace SponsorSphere.Application.App.SponsorIndividuals.Queries;

public record GetSponsorIndividualsByAgeQuery(int Age) : IRequest<List<SponsorIndividualDto>>;

public class GetSponsorIndividualsByAgeQueryHandler : IRequestHandler<GetSponsorIndividualsByAgeQuery, List<SponsorIndividualDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetSponsorIndividualsByAgeQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<List<SponsorIndividualDto>> Handle(GetSponsorIndividualsByAgeQuery request, CancellationToken cancellationToken)
    {
        var sponsorIndividuals = await _unitOfWork.SponsorIndividualsRepository.GetByAgeAsync(request.Age);
        var mappedSponsorIndividuals = _mapper.Map<List<SponsorIndividualDto>>(sponsorIndividuals);

        return await Task.FromResult(mappedSponsorIndividuals);
    }
}