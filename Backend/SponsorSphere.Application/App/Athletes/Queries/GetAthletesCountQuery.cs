using MediatR;
using SponsorSphere.Application.Interfaces;

namespace SponsorSphere.Application.App.Athletes.Queries;

public record GetAthletesCountQuery : IRequest<int>;

public class GetAthletesCountQueryHandler : IRequestHandler<GetAthletesCountQuery, int>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAthletesCountQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<int> Handle(GetAthletesCountQuery request, CancellationToken cancellationToken)
    {
        var count = await _unitOfWork.AthletesRepository.GetAthletesCount();

        return await Task.FromResult(count);
    }
}