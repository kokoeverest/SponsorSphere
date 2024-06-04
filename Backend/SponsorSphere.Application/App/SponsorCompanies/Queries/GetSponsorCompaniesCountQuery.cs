using MediatR;
using SponsorSphere.Application.Interfaces;

namespace SponsorSphere.Application.App.Athletes.Queries;

public record GetSponsorCompaniesCountQuery : IRequest<int>;

public class GetSponsorCompaniesCountQueryHandler : IRequestHandler<GetSponsorCompaniesCountQuery, int>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetSponsorCompaniesCountQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<int> Handle(GetSponsorCompaniesCountQuery request, CancellationToken cancellationToken)
    {
        var count = await _unitOfWork.SponsorCompaniesRepository.GetSponsorCompaniesCount();

        return await Task.FromResult(count);
    }
}