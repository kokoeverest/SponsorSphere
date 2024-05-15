using MediatR;
using Microsoft.Extensions.Logging;
using SponsorSphere.Application.Interfaces;

namespace SponsorSphere.Application.App.Sponsorships.Commands;

public record DeleteSponsorshipCommand(int AthleteId, int SponsorId) : IRequest;
public class DeleteSponsorshipCommandHandler : IRequestHandler<DeleteSponsorshipCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<DeleteSponsorshipCommandHandler> _logger;

    public DeleteSponsorshipCommandHandler(IUnitOfWork unitOfWork, ILogger<DeleteSponsorshipCommandHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task Handle(DeleteSponsorshipCommand request, CancellationToken cancellationToken)
    {
        var start = DateTime.Now;
        _logger.LogInformation("Action: {Action}", request.ToString());

        try
        {
            await _unitOfWork.BeginTransactionAsync();
            await _unitOfWork.SponsorshipsRepository.DeleteAsync(request.AthleteId, request.SponsorId);
            await _unitOfWork.CommitTransactionAsync();
            _logger.LogInformation("Action: {Action}, ({DT})ms", request.ToString(), (DateTime.Now - start).TotalMilliseconds);
        }

        catch (Exception)
        {
            await _unitOfWork.RollbackTransactionAsync();
            throw;
        }
    }
}
