using AutoMapper;
using MediatR;
using SponsorSphere.Application.App.SportEvents.Responses;
using SponsorSphere.Application.Interfaces;
using SponsorSphere.Domain.Enums;
using SponsorSphere.Domain.Models;

namespace SponsorSphere.Application.App.SportEvents.Commands;

public record CreateSportEventCommand(
    string Name,
    CountryEnum Country,
    string EventDate,
    bool Finished,
    EventsEnum EventType,
    SportsEnum Sport
    ) : IRequest<SportEventDto>;
public class CreateSportEventCommandHandler : IRequestHandler<CreateSportEventCommand, SportEventDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public CreateSportEventCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    public async Task<SportEventDto> Handle(CreateSportEventCommand request, CancellationToken cancellationToken)
    {
        var sportEvent = new SportEvent
        {
            Name = request.Name,
            Country = request.Country,
            EventDate = DateTime.Parse(request.EventDate).ToUniversalTime(),
            Finished = request.Finished,
            EventType = request.EventType,
            Sport = request.Sport
        };

        var newSportEvent = await _unitOfWork.SportEventsRepository.CreateAsync(sportEvent);
        var mappedSportEvent = _mapper.Map<SportEventDto>(newSportEvent);

        return await Task.FromResult(mappedSportEvent);
    }
}
