using AutoMapper;
using MediatR;
using SponsorSphere.Application.App.Athletes.Responses;
using SponsorSphere.Application.Interfaces;

namespace SponsorSphere.Application.App.Athletes.Queries;

public record GetAthletesByAmountSponsoredQuery : IRequest<List<object>?>;

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
        var athletes = await _unitOfWork.AthletesRepository.GetByAmountAsync();
        //var mappedAthletes = _mapper.Map<List<AthleteDto>>(athletes);

        return await Task.FromResult(athletes);
    }
}