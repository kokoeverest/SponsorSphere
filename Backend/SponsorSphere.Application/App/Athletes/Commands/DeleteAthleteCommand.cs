using MediatR;
using Microsoft.Extensions.Logging;
using SponsorSphere.Application.Common.Constants;
using SponsorSphere.Application.Interfaces;

namespace SponsorSphere.Application.App.Athletes.Commands;

public record DeleteAthleteCommand(int AthleteId) : IRequest<int>;

public class DeleteAthleteCommandHandler : IRequestHandler<DeleteAthleteCommand, int>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<DeleteAthleteCommandHandler> _logger;
    public DeleteAthleteCommandHandler(IUnitOfWork unitOfWork, ILogger<DeleteAthleteCommandHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<int> Handle(DeleteAthleteCommand request, CancellationToken cancellationToken)
    {
        var start = DateTime.Now;
        _logger.LogInformation(LoggingConstants.logStartString, request.ToString());

        var result = await _unitOfWork.AthletesRepository.DeleteAsync(request.AthleteId);

        _logger.LogInformation(LoggingConstants.logEndString, request.ToString(), (DateTime.Now - start).TotalMilliseconds);
        return result;
    }
}