using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using SponsorSphere.Application.App.SponsorCompanies.Dtos;
using SponsorSphere.Application.Interfaces;

namespace SponsorSphere.Application.App.SponsorCompanies.Commands;

public record UpdateSponsorCompanyCommand(SponsorCompanyDto SponsorCompanyToUpdate) : IRequest<SponsorCompanyDto>;

public class UpdateSponsorCompanyCommandHandler : IRequestHandler<UpdateSponsorCompanyCommand, SponsorCompanyDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<UpdateSponsorCompanyCommandHandler> _logger;

    public UpdateSponsorCompanyCommandHandler(IUnitOfWork unitOfWork, ILogger<UpdateSponsorCompanyCommandHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<SponsorCompanyDto> Handle(UpdateSponsorCompanyCommand request, CancellationToken cancellationToken)
    {
        var start = DateTime.Now;
        _logger.LogInformation("Action: {Action}", request.ToString());

        try
        {
            await _unitOfWork.BeginTransactionAsync();
            var result = await _unitOfWork.SponsorCompaniesRepository.UpdateAsync(request.SponsorCompanyToUpdate);
            await _unitOfWork.CommitTransactionAsync();

            _logger.LogInformation("Action: {Action}, ({DT})ms", request.ToString(), (DateTime.Now - start).TotalMilliseconds);
            return result;
        }

        catch (Exception)
        {
            await _unitOfWork.RollbackTransactionAsync();
            throw;
        }
    }
}