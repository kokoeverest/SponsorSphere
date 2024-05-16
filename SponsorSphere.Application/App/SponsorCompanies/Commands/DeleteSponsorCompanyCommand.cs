using MediatR;
using Microsoft.Extensions.Logging;
using SponsorSphere.Application.Interfaces;

namespace SponsorSphere.Application.App.SponsorCompanies.Commands;
public record DeleteSponsorCompanyCommand(int SponsorCompanyId) : IRequest<int>;

public class DeleteSponsorCompanyCommandHandler : IRequestHandler<DeleteSponsorCompanyCommand, int>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<DeleteSponsorCompanyCommandHandler> _logger;
    public DeleteSponsorCompanyCommandHandler(IUnitOfWork unitOfWork, ILogger<DeleteSponsorCompanyCommandHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<int> Handle(DeleteSponsorCompanyCommand request, CancellationToken cancellationToken)
    {
        var start = DateTime.Now;
        _logger.LogInformation(LoggingConstants.logStartString, request.ToString());

        var result = await _unitOfWork.SponsorCompaniesRepository.DeleteAsync(request.SponsorCompanyId);

        _logger.LogInformation(LoggingConstants.logEndString, request.ToString(), (DateTime.Now - start).TotalMilliseconds);
        return result;
    }
}