using AutoMapper;
using MediatR;
using SponsorSphere.Application.App.SportEvents.Dtos;
using SponsorSphere.Application.Interfaces;
using SponsorSphere.Domain.Enums;

namespace SponsorSphere.Application.App.SportEvents.Queries;
public record GetSportEventsBySportQuery(SportsEnum Sport, int PageNumber, int PageSize) : IRequest<List<SportEventDto>>;

public class GetSportEventsBySportQueryHandler : IRequestHandler<GetSportEventsBySportQuery, List<SportEventDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetSportEventsBySportQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    public async Task<List<SportEventDto>> Handle(GetSportEventsBySportQuery request, CancellationToken cancellationToken)
    {
        var sportEvents = await _unitOfWork.SportEventsRepository.GetBySportAsync(request.Sport, request.PageNumber, request.PageSize);
        var mappedSportEvents = _mapper.Map<List<SportEventDto>>(sportEvents);

        return mappedSportEvents;
    }
}