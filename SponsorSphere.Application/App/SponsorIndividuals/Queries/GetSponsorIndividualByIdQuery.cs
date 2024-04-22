using AutoMapper;
using MediatR;
using SponsorSphere.Application.Interfaces;
using SponsorSphere.Domain.Models;

namespace SponsorSphere.Application.App.SponsorIndividuals.Queries;

public record GetSponsorIndividualByIdQuery(int SponosrIndividualId) : IRequest<SponsorIndividual?>;

public class GetSponsorIndividualByIdQueryHandler : IRequestHandler<GetSponsorIndividualByIdQuery, SponsorIndividual?>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetSponsorIndividualByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<SponsorIndividual?> Handle(GetSponsorIndividualByIdQuery request, CancellationToken cancellationToken)
    {
        return await _unitOfWork.SponsorIndividualRepository.GetByIdAsync(request.SponosrIndividualId);
    }
}