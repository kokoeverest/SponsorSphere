using AutoMapper;
using MediatR;
using SponsorSphere.Application.App.SponsorCompanies.Responses;
using SponsorSphere.Application.Interfaces;

namespace SponsorSphere.Application.App.SponsorCompanies.Queries;

public record GetAllSponsorCompaniesQuery : IRequest<List<SponsorCompanyDto>>;

public class GetAllSponsorCompaniesQueryHandler : IRequestHandler<GetAllSponsorCompaniesQuery, List<SponsorCompanyDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetAllSponsorCompaniesQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<List<SponsorCompanyDto>> Handle(GetAllSponsorCompaniesQuery request, CancellationToken cancellationToken)
    {
        var sponsorCompanies = await _unitOfWork.SponsorCompanyRepository.GetAllAsync();
        var mappedSponsorCompanies = _mapper.Map<List<SponsorCompanyDto>>(sponsorCompanies);

        return await Task.FromResult(mappedSponsorCompanies);
    }
}