﻿using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using SponsorSphere.Application.App.SponsorCompanies.Dtos;
using SponsorSphere.Application.Common.Constants;
using SponsorSphere.Application.Interfaces;
using SponsorSphere.Domain.Models;

namespace SponsorSphere.Application.App.SponsorCompanies.Commands;

public record CreateSponsorCompanyCommand(RegisterSponsorCompanyDto SponsorCompany) : IRequest<SponsorCompanyDto>;

public class CreateSponsorCompanyCommandHandler : IRequestHandler<CreateSponsorCompanyCommand, SponsorCompanyDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly UserManager<User> _userManager;
    private readonly ILogger<CreateSponsorCompanyCommandHandler> _logger;
    public CreateSponsorCompanyCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, UserManager<User> userManager, ILogger<CreateSponsorCompanyCommandHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _userManager = userManager;
        _logger = logger;
    }

    public async Task<SponsorCompanyDto> Handle(CreateSponsorCompanyCommand request, CancellationToken cancellationToken)
    {
        var start = DateTime.Now;
        _logger.LogInformation(LoggingConstants.logStartString, request.ToString());

        var sponsorCompany = _mapper.Map<SponsorCompany>(request.SponsorCompany);

        try
        {
            await _unitOfWork.BeginTransactionAsync();

            await _userManager.CreateAsync(sponsorCompany, request.SponsorCompany.Password);
            var result = await _userManager.AddToRoleAsync(sponsorCompany, RoleConstants.Sponsor);

            if (!result.Succeeded)
            {
                throw new InvalidDataException(result.Errors.First().Description);
            }

            await _unitOfWork.CommitTransactionAsync();

            var sponsorCompanyDto = _mapper.Map<SponsorCompanyDto>(sponsorCompany);
            _logger.LogInformation(LoggingConstants.logEndString, request.ToString(), (DateTime.Now - start).TotalMilliseconds);
            return await Task.FromResult(sponsorCompanyDto);
        }

        catch (Exception)
        {
            await _unitOfWork.RollbackTransactionAsync();
            throw;
        }
    }
}