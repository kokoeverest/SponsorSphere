using MediatR;
using Microsoft.Extensions.Logging;
using SponsorSphere.Application.App.Sponsorships.Dtos;
using SponsorSphere.Application.Interfaces;

namespace SponsorSphere.Application.App.Sponsorships.Commands;

public record UpdateSponsorshipCommand(SponsorshipDto SponsorshipToUpdate) : IRequest<SponsorshipDto>;
public class UpdateSponsorshipCommandHandler : IRequestHandler<UpdateSponsorshipCommand, SponsorshipDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<UpdateSponsorshipCommandHandler> _logger;

    public UpdateSponsorshipCommandHandler(IUnitOfWork unitOfWork, ILogger<UpdateSponsorshipCommandHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<SponsorshipDto> Handle(UpdateSponsorshipCommand request, CancellationToken cancellationToken)
    {
        var start = DateTime.Now;
        _logger.LogInformation("Action: {Action}", request.ToString());

        try
        {
            await _unitOfWork.BeginTransactionAsync();
            var updatedSponsorship = await _unitOfWork.SponsorshipsRepository.UpdateAsync(request.SponsorshipToUpdate);
            await _unitOfWork.CommitTransactionAsync();

            _logger.LogInformation("Action: {Action}, ({DT})ms", request.ToString(), (DateTime.Now - start).TotalMilliseconds);
            return await Task.FromResult(updatedSponsorship);
        }
        catch (Exception)
        {
            await _unitOfWork.RollbackTransactionAsync();
            _logger.LogError("Action: {Action} failed", request.ToString());
            throw;
        }

    }
}