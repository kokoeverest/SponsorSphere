﻿using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using SponsorSphere.Application.App.SponsorIndividuals.Dtos;
using SponsorSphere.Application.Common.Constants;
using SponsorSphere.Application.Interfaces;
using SponsorSphere.Domain.Models;

namespace SponsorSphere.Application.App.SponsorIndividuals.Commands;
public record CreateSponsorIndividualCommand(RegisterSponsorIndividualDto SponsorIndividual) : IRequest<SponsorIndividualDto>;

public class CreateSponsorIndividualCommandHandler : IRequestHandler<CreateSponsorIndividualCommand, SponsorIndividualDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly UserManager<User> _userManager;
    private readonly ILogger<CreateSponsorIndividualCommandHandler> _logger;

    public CreateSponsorIndividualCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, UserManager<User> userManager, ILogger<CreateSponsorIndividualCommandHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _userManager = userManager;
        _logger = logger;
    }

    public async Task<SponsorIndividualDto> Handle(CreateSponsorIndividualCommand request, CancellationToken cancellationToken)
    {
        var start = DateTime.Now;
        _logger.LogInformation(LoggingConstants.logStartString, request.ToString());

        var sponsorIndividual = _mapper.Map<SponsorIndividual>(request.SponsorIndividual);

        try
        {
            await _unitOfWork.BeginTransactionAsync();

            await _userManager.CreateAsync(sponsorIndividual, request.SponsorIndividual.Password);
            var result = await _userManager.AddToRoleAsync(sponsorIndividual, RoleConstants.Sponsor);

            if (!result.Succeeded)
            {
                throw new InvalidDataException(result.Errors.First().Description);
            }

            await _unitOfWork.CommitTransactionAsync();

            var sponsorIndividualDto = _mapper.Map<SponsorIndividualDto>(sponsorIndividual);
            _logger.LogInformation(LoggingConstants.logEndString, request.ToString(), (DateTime.Now - start).TotalMilliseconds);
            return await Task.FromResult(sponsorIndividualDto);
        }

        catch (Exception)
        {
            await _unitOfWork.RollbackTransactionAsync();
            throw;
        }
    }
}