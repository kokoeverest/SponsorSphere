using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using SponsorSphere.Application.App.SponsorIndividuals.Dtos;
using SponsorSphere.Application.Interfaces;
using SponsorSphere.Domain.Models;

namespace SponsorSphere.Application.App.SponsorIndividuals.Commands;
public record CreateSponsorIndividualCommand(RegisterSponsorIndividualDto SponsorIndividual) : IRequest<SponsorIndividualDto>;

public class CreateSponsorIndividualCommandHandler : IRequestHandler<CreateSponsorIndividualCommand, SponsorIndividualDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly UserManager<User> _userManager;

    public CreateSponsorIndividualCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, UserManager<User> userManager)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _userManager = userManager;
    }

    public async Task<SponsorIndividualDto> Handle(CreateSponsorIndividualCommand request, CancellationToken cancellationToken)
    {
        var sponsorIndividual = _mapper.Map<SponsorIndividual>(request.SponsorIndividual);

        try
        {
            await _unitOfWork.BeginTransactionAsync();

            await _userManager.CreateAsync(sponsorIndividual, request.SponsorIndividual.Password);
            await _userManager.AddToRoleAsync(sponsorIndividual, RoleConstants.Sponsor);

            await _unitOfWork.CommitTransactionAsync();

            var sponsorIndividualDto = _mapper.Map<SponsorIndividualDto>(sponsorIndividual);
            return await Task.FromResult(sponsorIndividualDto);
        }

        catch (Exception)
        {
            await _unitOfWork.RollbackTransactionAsync();
            throw;
        }
    }
}