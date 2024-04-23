using AutoMapper;
using MediatR;
using SponsorSphere.Application.App.SponsorIndividuals.Responses;
using SponsorSphere.Application.Interfaces;
using SponsorSphere.Domain.Models;

namespace SponsorSphere.Application.App.SponsorIndividuals.Commands;
// Add more of the properties which can be changed
public record UpdateSponsorIndividualCommand(
    int SponsorIndividualToUpdate,
    string? NewWebsite,
    string? NewFaceBookLink,
    string? NewStravaLink,
    string? NewTwitterLink,
    string? NewInstagramLink) : IRequest<SponsorIndividualDto>;

public class UpdateSponsorIndividualCommandHandler : IRequestHandler<UpdateSponsorIndividualCommand, SponsorIndividualDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateSponsorIndividualCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<SponsorIndividualDto> Handle(UpdateSponsorIndividualCommand request, CancellationToken cancellationToken)
    {
        var oldSponsorIndividual = request.SponsorIndividualToUpdate;

        request.SponsorIndividualToUpdate.Website = request.NewWebsite ?? oldSponsorIndividual.Website;
        request.SponsorIndividualToUpdate.FaceBookLink = request.NewFaceBookLink ?? oldSponsorIndividual.FaceBookLink;
        request.SponsorIndividualToUpdate.StravaLink = request.NewStravaLink ?? oldSponsorIndividual.StravaLink;
        request.SponsorIndividualToUpdate.InstagramLink = request.NewInstagramLink ?? oldSponsorIndividual.InstagramLink;
        request.SponsorIndividualToUpdate.TwitterLink = request.NewTwitterLink ?? oldSponsorIndividual.TwitterLink;

        _unitOfWork.SponsorIndividualsRepository.Update(request.SponsorIndividualToUpdate);

        var updatedSponsorCompanyDto = _mapper.Map<SponsorIndividualDto>(request.SponsorIndividualToUpdate);

        return await Task.FromResult(updatedSponsorCompanyDto);
    }
}