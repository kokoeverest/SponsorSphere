using MediatR;
using Microsoft.Extensions.Logging;
using SponsorSphere.Application.Interfaces;

namespace SponsorSphere.Application.App.SponsorIndividuals.Commands;
public record DeleteSponsorIndividualCommand(int SponsorIndividualId) : IRequest<int>;

public class DeleteSponsorIndividualCommandHandler : IRequestHandler<DeleteSponsorIndividualCommand, int>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<DeleteSponsorIndividualCommandHandler> _logger;
    public DeleteSponsorIndividualCommandHandler(IUnitOfWork unitOfWork, ILogger<DeleteSponsorIndividualCommandHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<int> Handle(DeleteSponsorIndividualCommand request, CancellationToken cancellationToken)
    {
        var start = DateTime.Now;
        _logger.LogInformation("Action: {Action}", request.ToString());

        var result = await _unitOfWork.SponsorIndividualsRepository.DeleteAsync(request.SponsorIndividualId);

        _logger.LogInformation("Action: {Action}, ({DT})ms", request.ToString(), (DateTime.Now - start).TotalMilliseconds);
        return result;
    }
}