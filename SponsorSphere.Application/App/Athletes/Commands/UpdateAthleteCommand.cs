using AutoMapper;
using MediatR;
using SponsorSphere.Application.App.Athletes.Responses;
using SponsorSphere.Application.Interfaces;
using SponsorSphere.Domain.Models;

namespace SponsorSphere.Application.App.Athletes.Commands;

// Add more of the properties which can be changed
public record UpdateAthleteCommand(
    int AthleteToUpdate,
    string? NewWebsite,
    string? NewFaceBookLink,
    string? NewStravaLink,
    string? NewTwitterLink,
    string? NewInstagramLink) : IRequest<AthleteDto>;

public class UpdateAthleteCommandHandler : IRequestHandler<UpdateAthleteCommand, AthleteDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateAthleteCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<AthleteDto> Handle(UpdateAthleteCommand request, CancellationToken cancellationToken)
    {
        var oldAthlete = request.AthleteToUpdate;

        request.AthleteToUpdate.Website = request.NewWebsite ?? oldAthlete.Website;
        request.AthleteToUpdate.FaceBookLink = request.NewFaceBookLink ?? oldAthlete.FaceBookLink;
        request.AthleteToUpdate.StravaLink = request.NewStravaLink ?? oldAthlete.StravaLink;
        request.AthleteToUpdate.InstagramLink = request.NewInstagramLink ?? oldAthlete.InstagramLink;
        request.AthleteToUpdate.TwitterLink = request.NewTwitterLink ?? oldAthlete.TwitterLink;

        _unitOfWork.AthletesRepository.Update(request.AthleteToUpdate);

        var updatedAthleteDto = _mapper.Map<AthleteDto>(request.AthleteToUpdate);

        return await Task.FromResult(updatedAthleteDto);
    }
}
