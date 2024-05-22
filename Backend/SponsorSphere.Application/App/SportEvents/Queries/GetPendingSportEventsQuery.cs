using AutoMapper;
using MediatR;
using SponsorSphere.Application.App.SportEvents.Dtos;
using SponsorSphere.Application.Interfaces;
using SponsorSphere.Domain.Enums;

namespace SponsorSphere.Application.App.SportEvents.Queries;
public record GetPendingSportEventsQuery(int PageNumber, int PageSize) : IRequest<List<SportEventDto>>;

public class GetPendingSportEventsQueryHandler : IRequestHandler<GetPendingSportEventsQuery, List<SportEventDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetPendingSportEventsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    public async Task<List<SportEventDto>> Handle(GetPendingSportEventsQuery request, CancellationToken cancellationToken)
    {
        var sportEvents = await _unitOfWork.SportEventsRepository.GetPendingSportEventsAsync(request.PageNumber, request.PageSize);
        var mappedSportEvents = _mapper.Map<List<SportEventDto>>(sportEvents);

        return mappedSportEvents;
    }
}