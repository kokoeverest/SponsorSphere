using AutoMapper;
using MediatR;
using SponsorSphere.Application.App.SponsorCompanies.Responses;
using SponsorSphere.Application.Interfaces;
using SponsorSphere.Domain.Models;

namespace SponsorSphere.Application.App.SponsorCompanies.Queries;

public record GetSponsorCompanyByIdQuery(int SponsorCompanyId) : IRequest<SponsorCompanyDto?>;

public class GetSponsorCompanyByIdQueryHandler : IRequestHandler<GetSponsorCompanyByIdQuery, SponsorCompanyDto?>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetSponsorCompanyByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<SponsorCompanyDto?> Handle(GetSponsorCompanyByIdQuery request, CancellationToken cancellationToken)
    {
        var sponsorCompany = await _unitOfWork.SponsorCompaniesRepository.GetByIdAsync(request.SponsorCompanyId);
        var mappedSponsorCompany = _mapper.Map<SponsorCompanyDto>(sponsorCompany);

        return await Task.FromResult(mappedSponsorCompany);

    }
}