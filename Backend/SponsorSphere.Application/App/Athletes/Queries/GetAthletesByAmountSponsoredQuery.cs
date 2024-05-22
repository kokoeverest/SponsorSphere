using AutoMapper;
using MediatR;
using SponsorSphere.Application.Interfaces;

namespace SponsorSphere.Application.App.Athletes.Queries;

public record GetAthletesByAmountSponsoredQuery(int PageNumber, int PageSize) : IRequest<List<object>?>;

public class GetAthletesByAmountSponsoredQueryHandler : IRequestHandler<GetAthletesByAmountSponsoredQuery, List<object>?>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetAthletesByAmountSponsoredQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<List<object>?> Handle(GetAthletesByAmountSponsoredQuery request, CancellationToken cancellationToken)
    {
        var athletes = await _unitOfWork.AthletesRepository.GetByAmountAsync(request.PageNumber, request.PageSize);

        return await Task.FromResult(athletes);
    }
}