using MediatR;
using SponsorSphere.Application.Interfaces;

namespace SponsorSphere.Application.App.SponsorCompanies.Commands;
public record DeleteSponsorCompanyCommand(int SponsorCompanyId) : IRequest<int>;

public class DeleteSponsorCompanyCommandHandler : IRequestHandler<DeleteSponsorCompanyCommand, int>
{
    private readonly IUnitOfWork _unitOfWork;
    public DeleteSponsorCompanyCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<int> Handle(DeleteSponsorCompanyCommand request, CancellationToken cancellationToken)
    {
        return await _unitOfWork.SponsorCompaniesRepository.DeleteAsync(request.SponsorCompanyId);
    }
}