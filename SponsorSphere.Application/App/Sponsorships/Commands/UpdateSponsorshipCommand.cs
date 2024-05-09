using MediatR;
using SponsorSphere.Application.App.Sponsorships.Dtos;
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
        try
        {
            await _unitOfWork.BeginTransactionAsync();
            var updatedSponsorship = await _unitOfWork.SponsorshipsRepository.UpdateAsync(request.SponsorshipToUpdate);
            await _unitOfWork.CommitTransactionAsync();
            return await Task.FromResult(updatedSponsorship);
        }

        catch (Exception)
        {
            await _unitOfWork.RollbackTransactionAsync();
            throw;
        }

    }
}