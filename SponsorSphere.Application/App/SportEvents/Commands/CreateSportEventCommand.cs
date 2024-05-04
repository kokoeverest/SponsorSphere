using AutoMapper;
using MediatR;
using SponsorSphere.Application.App.SportEvents.Responses;
using SponsorSphere.Application.Interfaces;
using SponsorSphere.Domain.Models;

namespace SponsorSphere.Application.App.SportEvents.Commands;

public record CreateSportEventCommand(SportEvent SportEvent) : IRequest<SportEventDto>;
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
        request.SportEvent.Finished = true && request.SportEvent.EventDate < DateTime.UtcNow.Subtract(TimeSpan.FromDays(1));

        var newSportEvent = await _unitOfWork.SportEventsRepository.CreateAsync(request.SportEvent);
        var mappedSportEvent = _mapper.Map<SportEventDto>(newSportEvent);

        return await Task.FromResult(mappedSportEvent);
    }
}
