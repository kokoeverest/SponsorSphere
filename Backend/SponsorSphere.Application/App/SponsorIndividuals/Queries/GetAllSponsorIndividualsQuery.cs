using AutoMapper;
using MediatR;
using SponsorSphere.Application.App.SponsorIndividuals.Dtos;
using SponsorSphere.Application.Interfaces;

namespace SponsorSphere.Application.App.SponsorIndividuals.Queries;

public record GetAllSponsorIndividualsQuery(int PageNumber, int PageSize) : IRequest<List<SponsorIndividualDto>>;

public class GetAllSponsorIndividualsQueryHandler : IRequestHandler<GetAllSponsorIndividualsQuery, List<SponsorIndividualDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetAllSponsorIndividualsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<List<SponsorIndividualDto>> Handle(GetAllSponsorIndividualsQuery request, CancellationToken cancellationToken)
    {
        var sponsorIndividuals = await _unitOfWork.SponsorIndividualsRepository.GetAllAsync(request.PageNumber, request.PageSize);
        var mappedSponsorIndividuals = _mapper.Map<List<SponsorIndividualDto>>(sponsorIndividuals);

        return await Task.FromResult(mappedSponsorIndividuals);
    }
}