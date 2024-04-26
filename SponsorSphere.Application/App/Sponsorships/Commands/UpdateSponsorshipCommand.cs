using MediatR;
using SponsorSphere.Application.App.Sponsorships.Responses;
using SponsorSphere.Application.Interfaces;

namespace SponsorSphere.Application.App.Sponsorships.Commands;

public record UpdateSponsorshipCommand(SponsorshipDto SponsorshipToUpdate) : IRequest<SponsorshipDto>;
public class UpdateSponsorshipCommandHandler : IRequestHandler<UpdateSponsorshipCommand, SponsorshipDto>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateSponsorshipCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<SponsorshipDto> Handle(UpdateSponsorshipCommand request, CancellationToken cancellationToken)
    {
        var updatedSponsorship = await _unitOfWork.SponsorshipsRepository.UpdateAsync(request.SponsorshipToUpdate);

        return await Task.FromResult(updatedSponsorship);
    }
}