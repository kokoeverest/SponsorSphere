using AutoMapper;
using MediatR;
using SponsorSphere.Application.App.SportEvents.Dtos;
using SponsorSphere.Application.Interfaces;

namespace SponsorSphere.Application.App.SportEvents.Queries;

public record GetSportEventByIdQuery(int SportEventId) : IRequest<SportEventDto?>;

public class GetSportEventByIdQueryHandler : IRequestHandler<GetSportEventByIdQuery, SportEventDto?>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetSportEventByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    public async Task<SportEventDto?> Handle(GetSportEventByIdQuery request, CancellationToken cancellationToken)
    {
        var sportEvent = await _unitOfWork.SportEventsRepository.GetByIdAsync(request.SportEventId);
        var mappedSportEvent = _mapper.Map<SportEventDto>(sportEvent);

        return mappedSportEvent;
    }
}
