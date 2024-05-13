using AutoMapper;
using MediatR;
using SponsorSphere.Application.App.SponsorCompanies.Dtos;
using SponsorSphere.Application.Interfaces;
using SponsorSphere.Domain.Enums;

namespace SponsorSphere.Application.App.SponsorCompanies.Queries;

public record GetSponsorCompaniesByCountryQuery(CountryEnum Country, int PageNumber, int PageSize) : IRequest<List<SponsorCompanyDto>>;

public class GetSponsorCompaniesByCountryQueryHandler : IRequestHandler<GetSponsorCompaniesByCountryQuery, List<SponsorCompanyDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetSponsorCompaniesByCountryQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<List<SponsorCompanyDto>> Handle(GetSponsorCompaniesByCountryQuery request, CancellationToken cancellationToken)
    {
        var sponsorCompanies = await _unitOfWork.SponsorCompaniesRepository.GetByCountryAsync(request.Country, request.PageNumber, request.PageSize);
        var mappedSponsorCompanies = _mapper.Map<List<SponsorCompanyDto>>(sponsorCompanies);

        return await Task.FromResult(mappedSponsorCompanies);
    }
}