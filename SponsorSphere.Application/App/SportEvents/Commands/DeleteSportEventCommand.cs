using MediatR;
using Microsoft.Extensions.Logging;
using SponsorSphere.Application.Common.Constants;
using SponsorSphere.Application.Interfaces;

namespace SponsorSphere.Application.App.SportEvents.Commands;

public record DeleteSportEventCommand(int SportEventId) : IRequest;
public class DeleteSportEventCommandHandler : IRequestHandler<DeleteSportEventCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<DeleteSportEventCommandHandler> _logger;

    public DeleteSportEventCommandHandler(IUnitOfWork unitOfWork, ILogger<DeleteSportEventCommandHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task Handle(DeleteSportEventCommand request, CancellationToken cancellationToken)
    {
        var start = DateTime.Now;
        _logger.LogInformation(LoggingConstants.logStartString, request.ToString());

        try
        {
            await _unitOfWork.BeginTransactionAsync();
            await _unitOfWork.SportEventsRepository.DeleteAsync(request.SportEventId);
            await _unitOfWork.CommitTransactionAsync();
            _logger.LogInformation(LoggingConstants.logEndString, request.ToString(), (DateTime.Now - start).TotalMilliseconds);
        }

        catch (Exception) 
        {
            await _unitOfWork.RollbackTransactionAsync();
            throw;
        }
    } 
        
}

