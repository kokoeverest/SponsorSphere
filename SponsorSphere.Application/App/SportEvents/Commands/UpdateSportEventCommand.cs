using MediatR;
using SponsorSphere.Application.App.SportEvents.Responses;
using SponsorSphere.Application.Interfaces;

namespace SponsorSphere.Application.App.SportEvents.Commands;

public record UpdateSportEventCommand(SportEventDto SportEventToUpdate) : IRequest<SportEventDto>;
public class UpdateSportEventCommandHandler : IRequestHandler<UpdateSportEventCommand, SportEventDto>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateSportEventCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<SportEventDto> Handle(UpdateSportEventCommand request, CancellationToken cancellationToken)
    {
        request.SportEventToUpdate.Finished = true && request.SportEventToUpdate.EventDate < DateTime.UtcNow.Subtract(TimeSpan.FromDays(1));
        try
        {
            await _unitOfWork.BeginTransactionAsync();
            var updatedSportEvent = await _unitOfWork.SportEventsRepository.UpdateAsync(request.SportEventToUpdate);
            await _unitOfWork.CommitTransactionAsync();
            return await Task.FromResult(updatedSportEvent);
        }

        catch (Exception)
        {
            await _unitOfWork.RollbackTransactionAsync();
            throw;
        }
    }
}
