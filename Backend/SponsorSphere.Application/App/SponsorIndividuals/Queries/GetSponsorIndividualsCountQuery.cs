using MediatR;
using SponsorSphere.Application.Interfaces;

namespace SponsorSphere.Application.App.Athletes.Queries;

public record GetSponsorIndividualsCountQuery : IRequest<int>;

public class GetSponsorIndividualsCountQueryHandler : IRequestHandler<GetSponsorIndividualsCountQuery, int>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetSponsorIndividualsCountQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<int> Handle(GetSponsorIndividualsCountQuery request, CancellationToken cancellationToken)
    {
        var count = await _unitOfWork.SponsorIndividualsRepository.GetSponsorIndividualsCount();

        return await Task.FromResult(count);
    }
}