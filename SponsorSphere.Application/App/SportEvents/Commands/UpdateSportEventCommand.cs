using AutoMapper;
using MediatR;
using SponsorSphere.Application.App.SportEvents.Responses;
using SponsorSphere.Application.Interfaces;
using SponsorSphere.Domain.Enums;

namespace SponsorSphere.Application.App.SportEvents.Commands;

public record UpdateSportEventCommand(SportEventDto SportEventToUpdate) : IRequest<SportEventDto>;
public class UpdateSportEventCommandHandler : IRequestHandler<UpdateSportEventCommand, SportEventDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateSportEventCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<SportEventDto> Handle(UpdateSportEventCommand request, CancellationToken cancellationToken)
    {
        var oldSportEvent = request.SportEventToUpdate;
        request.SportEventToUpdate.Finished = true && request.SportEventToUpdate.EventDate < DateTime.Now.Subtract(TimeSpan.FromDays(1));

        //request.SportEventToUpdate.Name = request.NewName ?? oldSportEvent.Name;
        //request.SportEventToUpdate.EventDate = DateTime.Parse(request.NewDate ?? oldSportEvent.EventDate.ToString());
        //request.SportEventToUpdate.Country = request.NewCountry ?? oldSportEvent.Country;
        //request.SportEventToUpdate.EventType = request.NewEventType | oldSportEvent.EventType;
        //request.SportEventToUpdate.Sport = request.NewSport | oldSportEvent.Sport;

        var updatedSportEvent = await _unitOfWork.SportEventsRepository.UpdateAsync(request.SportEventToUpdate);

        //var updatedSportEventDto = _mapper.Map<SportEventDto>(request.SportEventToUpdate);

        return await Task.FromResult(updatedSportEvent);
    }
}
