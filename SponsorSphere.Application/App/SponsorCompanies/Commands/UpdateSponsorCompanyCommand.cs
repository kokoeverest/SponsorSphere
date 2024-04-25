using AutoMapper;
using MediatR;
using SponsorSphere.Application.App.SponsorCompanies.Responses;
using SponsorSphere.Application.Interfaces;

namespace SponsorSphere.Application.App.SponsorCompanies.Commands;

public record UpdateSponsorCompanyCommand(SponsorCompanyDto SponsorCompanyToUpdate) : IRequest<SponsorCompanyDto>;

public class UpdateSponsorCompanyCommandHandler : IRequestHandler<UpdateSponsorCompanyCommand, SponsorCompanyDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateSponsorCompanyCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<SponsorCompanyDto> Handle(UpdateSponsorCompanyCommand request, CancellationToken cancellationToken)
    {
        var updatedSponsorCompany = _unitOfWork.SponsorCompaniesRepository.UpdateAsync(request.SponsorCompanyToUpdate);

        var updatedSponsorCompanyDto = _mapper.Map<SponsorCompanyDto>(updatedSponsorCompany);

        return await Task.FromResult(updatedSponsorCompanyDto);
    }
}