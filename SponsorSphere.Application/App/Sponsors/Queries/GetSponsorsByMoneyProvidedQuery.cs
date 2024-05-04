using AutoMapper;
using MediatR;
using SponsorSphere.Application.App.Sponsors.Responses;
using SponsorSphere.Application.Interfaces;

namespace SponsorSphere.Application.App.Sponsors.Queries;

public record GetSponsorsByMoneyProvidedQuery() : IRequest<List<object>?>;

public class GetSponsorsByMoneyProvidedQueryHandler : IRequestHandler<GetSponsorsByMoneyProvidedQuery, List<object>?>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetSponsorsByMoneyProvidedQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<List<object>?> Handle(GetSponsorsByMoneyProvidedQuery request, CancellationToken cancellationToken)
    {
        var sponsors = await _unitOfWork.SponsorsRepository.GetByMoneyProvidedAsync();

        return await Task.FromResult(sponsors);
    }
}