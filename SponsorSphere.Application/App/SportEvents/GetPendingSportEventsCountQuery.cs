using AutoMapper;
using MediatR;
using SponsorSphere.Application.Interfaces;

namespace SponsorSphere.Application.App.SportEvents;

public record GetPendingSportEventsCountQuery : IRequest<int>;

public class GetPendingSportEventsCountQueryHandler : IRequestHandler<GetPendingSportEventsCountQuery, int>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetPendingSportEventsCountQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<int> Handle(GetPendingSportEventsCountQuery request, CancellationToken cancellationToken) =>
        
        await _unitOfWork.SportEventsRepository.GetPendingSportEventsCountAsync();
}