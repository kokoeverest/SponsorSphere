using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using SponsorSphere.Application.App.Sponsorships.Dtos;
using SponsorSphere.Application.Common.Constants;
using SponsorSphere.Application.Interfaces;
using SponsorSphere.Domain.Enums;
using SponsorSphere.Domain.Models;
using System.Reflection;

namespace SponsorSphere.Application.App.Sponsorships.Commands;

public record CreateSponsorshipCommand(CreateSponsorshipDto Sponsorship) : IRequest<SponsorshipDto>;
public class CreateSponsorshipCommandHandler : IRequestHandler<CreateSponsorshipCommand, SponsorshipDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ILogger<CreateSponsorshipCommandHandler> _logger;

    public CreateSponsorshipCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<CreateSponsorshipCommandHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<SponsorshipDto> Handle(CreateSponsorshipCommand request, CancellationToken cancellationToken)
    {
        // Add a check - if the sponsorship level is Single payment => check if the athlete has a
        // Goal and if he has => reduce the AmountNeeded of the Goal with the sponsorship amount

        var start = DateTime.Now;
        _logger.LogInformation(LoggingConstants.logStartString, request.ToString());

        var sponsorship = _mapper.Map<Sponsorship>(request.Sponsorship);

        try
        {
            await _unitOfWork.BeginTransactionAsync();
            var newSponsorship = _unitOfWork.SponsorshipsRepository.CreateAsync(sponsorship);

            await _unitOfWork.CommitTransactionAsync();
            var mappedSponsorship = _mapper.Map<SponsorshipDto>( newSponsorship );

            _logger.LogInformation(LoggingConstants.logEndString, request.ToString(), (DateTime.Now - start).TotalMilliseconds);
            return await Task.FromResult(mappedSponsorship);
        }
        catch (Exception)
        {
            await _unitOfWork.RollbackTransactionAsync();
            throw;
        }
    }
}
