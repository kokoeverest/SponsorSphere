using MediatR;
using SponsorSphere.Application.Interfaces;

namespace SponsorSphere.Application.App.SportEvents.Commands;

public record DeleteSportEventCommand(int SportEventId) : IRequest;
public class DeleteSportEventCommandHandler : IRequestHandler<DeleteSportEventCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteSportEventCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(DeleteSportEventCommand request, CancellationToken cancellationToken)
    {
        try
        {
            await _unitOfWork.BeginTransactionAsync();
            await _unitOfWork.SportEventsRepository.DeleteAsync(request.SportEventId);
            await _unitOfWork.CommitTransactionAsync();
        }

        catch (Exception) 
        {
            await _unitOfWork.RollbackTransactionAsync();
            throw;
        }
    } 
        
}

