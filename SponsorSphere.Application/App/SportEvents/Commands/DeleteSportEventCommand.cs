using MediatR;
using SponsorSphere.Application.Interfaces;
using SponsorSphere.Domain.Models;

namespace SponsorSphere.Application.App.SportEvents.Commands;

public record DeleteSportEventCommand(SportEvent SportEvent) : IRequest<int>;
public class DeleteSportEventCommandHandler : IRequestHandler<DeleteSportEventCommand, int>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteSportEventCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<int> Handle(DeleteSportEventCommand request, CancellationToken cancellationToken)
    {
        return await _unitOfWork.SportEventsRepository.DeleteAsync(request.SportEvent);
    }
}

