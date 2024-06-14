using AutoMapper;
using MediatR;
using SponsorSphere.Application.App.Sponsors.Dtos;
using SponsorSphere.Application.Interfaces;

namespace SponsorSphere.Application.App.Sponsors.Queries;

public record GetSponsorByIdQuery(int SponsorId) : IRequest<SponsorDto?>;

public class GetSponsorCompanyByIdQueryHandler : IRequestHandler<GetSponsorByIdQuery, SponsorDto?>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetSponsorCompanyByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<SponsorDto?> Handle(GetSponsorByIdQuery request, CancellationToken cancellationToken)
    {
        var sponsorCompany = await _unitOfWork.SponsorsRepository.GetByIdAsync(request.SponsorId);
        var mappedSponsorCompany = _mapper.Map<SponsorDto>(sponsorCompany);

        return await Task.FromResult(mappedSponsorCompany);

    }
}