using MediatR;
using Microsoft.Extensions.Logging;
using SponsorSphere.Application.App.SportEvents.Dtos;
using SponsorSphere.Application.Common.Constants;
using SponsorSphere.Application.Interfaces;

namespace SponsorSphere.Application.App.SportEvents.Commands;

public record UpdateSportEventCommand(SportEventDto SportEventToUpdate) : IRequest<SportEventDto>;
public class UpdateSportEventCommandHandler : IRequestHandler<UpdateSportEventCommand, SportEventDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<UpdateSportEventCommandHandler> _logger;

    public UpdateSportEventCommandHandler(IUnitOfWork unitOfWork, ILogger<UpdateSportEventCommandHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<SportEventDto> Handle(UpdateSportEventCommand request, CancellationToken cancellationToken)
    {
        var start = DateTime.Now;
        _logger.LogInformation(LoggingConstants.logStartString, request.ToString());

        request.SportEventToUpdate.Finished = true && request.SportEventToUpdate.EventDate < DateTime.UtcNow.Subtract(TimeSpan.FromDays(1));
        try
        {
            await _unitOfWork.BeginTransactionAsync();
            var updatedSportEvent = await _unitOfWork.SportEventsRepository.UpdateAsync(request.SportEventToUpdate);
            await _unitOfWork.CommitTransactionAsync();

            _logger.LogInformation(LoggingConstants.logEndString, request.ToString(), (DateTime.Now - start).TotalMilliseconds);
            return await Task.FromResult(updatedSportEvent);
        }

        catch (Exception)
        {
            await _unitOfWork.RollbackTransactionAsync();
            throw;
        }
    }
}
