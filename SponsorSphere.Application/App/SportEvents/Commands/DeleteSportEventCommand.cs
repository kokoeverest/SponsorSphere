using MediatR;
using SponsorSphere.Application.Interfaces;

namespace SponsorSphere.Application.App.SportEvents.Commands;

public record DeleteSportEventCommand(int SportEventId) : IRequest<int>;
public class DeleteSportEventCommandHandler : IRequestHandler<DeleteSportEventCommand, int>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteSportEventCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<int> Handle(DeleteSportEventCommand request, CancellationToken cancellationToken) => 
        
        await _unitOfWork.SportEventsRepository.DeleteAsync(request.SportEventId);
}

