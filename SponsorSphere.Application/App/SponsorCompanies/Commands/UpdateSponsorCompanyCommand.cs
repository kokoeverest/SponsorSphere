using AutoMapper;
using MediatR;
using SponsorSphere.Application.App.SponsorCompanies.Responses;
using SponsorSphere.Application.Interfaces;
using SponsorSphere.Domain.Models;

namespace SponsorSphere.Application.App.SponsorCompanies.Commands;
// Add more of the properties which can be changed
public record UpdateSponsorCompanyCommand(
    SponsorCompanyDto SponsorCompanyToUpdate,
    string? NewWebsite,
    string? NewFaceBookLink,
    string? NewStravaLink,
    string? NewTwitterLink,
    string? NewInstagramLink) : IRequest<SponsorCompanyDto>;

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
        var oldSponsorCompany = request.SponsorCompanyToUpdate;

        request.SponsorCompanyToUpdate.Website = request.NewWebsite ?? oldSponsorCompany.Website;
        request.SponsorCompanyToUpdate.FaceBookLink = request.NewFaceBookLink ?? oldSponsorCompany.FaceBookLink;
        request.SponsorCompanyToUpdate.StravaLink = request.NewStravaLink ?? oldSponsorCompany.StravaLink;
        request.SponsorCompanyToUpdate.InstagramLink = request.NewInstagramLink ?? oldSponsorCompany.InstagramLink;
        request.SponsorCompanyToUpdate.TwitterLink = request.NewTwitterLink ?? oldSponsorCompany.TwitterLink;

        _unitOfWork.SponsorCompaniesRepository.Update(request.SponsorCompanyToUpdate);

        var updatedSponsorCompanyDto = _mapper.Map<SponsorCompanyDto>(request.SponsorCompanyToUpdate);

        return await Task.FromResult(updatedSponsorCompanyDto);
    }
}