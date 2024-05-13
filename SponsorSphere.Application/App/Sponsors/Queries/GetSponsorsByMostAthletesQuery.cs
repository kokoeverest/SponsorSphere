using AutoMapper;
using MediatR;
using SponsorSphere.Application.Interfaces;

namespace SponsorSphere.Application.App.Sponsors.Queries;

public record GetSponsorsByMostAthletesQuery(int PageNumber, int PageSize) : IRequest<List<object>>;

public class GetSponsorsByMostAthletesQueryHandler : IRequestHandler<GetSponsorsByMostAthletesQuery, List<object>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetSponsorsByMostAthletesQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<List<object>> Handle(GetSponsorsByMostAthletesQuery request, CancellationToken cancellationToken)
    {
        var sponsors = await _unitOfWork.SponsorsRepository.GetByMostAthletesAsync(request.PageNumber, request.PageSize);

        return await Task.FromResult(sponsors);
    }
}