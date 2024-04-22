using AutoMapper;
using MediatR;
using SponsorSphere.Application.Interfaces;
using SponsorSphere.Domain.Models;

namespace SponsorSphere.Application.App.SponsorCompanies.Queries;

public record GetSponsorCompanyByIdQuery(int SponsorCompanyId) : IRequest<SponsorCompany?>;

public class GetSponsorCompanyByIdQueryHandler : IRequestHandler<GetSponsorCompanyByIdQuery, SponsorCompany?>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetSponsorCompanyByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<SponsorCompany?> Handle(GetSponsorCompanyByIdQuery request, CancellationToken cancellationToken)
    {
        return await _unitOfWork.SponsorCompanyRepository.GetByIdAsync(request.SponsorCompanyId);
    }
}