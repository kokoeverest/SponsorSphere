using AutoMapper;
using MediatR;
using SponsorSphere.Application.App.SponsorIndividuals.Dtos;
using SponsorSphere.Application.Interfaces;

namespace SponsorSphere.Application.App.SponsorIndividuals.Queries;

public record GetSponsorIndividualByIdQuery(int SponsorIndividualId) : IRequest<SponsorIndividualDto?>;

public class GetSponsorIndividualByIdQueryHandler : IRequestHandler<GetSponsorIndividualByIdQuery, SponsorIndividualDto?>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetSponsorIndividualByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<SponsorIndividualDto?> Handle(GetSponsorIndividualByIdQuery request, CancellationToken cancellationToken)
    {
        var sponsorIndividual = await _unitOfWork.SponsorIndividualsRepository.GetByIdAsync(request.SponsorIndividualId);
        var mappedAthlete = _mapper.Map<SponsorIndividualDto>(sponsorIndividual);

        return await Task.FromResult(mappedAthlete);
    }
}