using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using SponsorSphere.Application.App.SponsorCompanies.Dtos;
using SponsorSphere.Application.Interfaces;
using SponsorSphere.Domain.Models;

namespace SponsorSphere.Application.App.SponsorCompanies.Commands;

public record CreateSponsorCompanyCommand(RegisterSponsorCompanyDto SponsorCompany) : IRequest<SponsorCompanyDto>;

public class CreateSponsorCompanyCommandHandler : IRequestHandler<CreateSponsorCompanyCommand, SponsorCompanyDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly UserManager<User> _userManager;
    public CreateSponsorCompanyCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, UserManager<User> userManager)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _userManager = userManager;
    }

    public async Task<SponsorCompanyDto> Handle(CreateSponsorCompanyCommand request, CancellationToken cancellationToken)
    {
        var sponsorCompany = new SponsorCompany
        {
            Name = request.SponsorCompany.Name,
            UserName = request.SponsorCompany.Email,
            Email = request.SponsorCompany.Email,
            Country = request.SponsorCompany.Country,
            PhoneNumber = request.SponsorCompany.PhoneNumber,
            IBAN = request.SponsorCompany.IBAN
        };

        try
        {
            await _unitOfWork.BeginTransactionAsync();

            await _userManager.CreateAsync(sponsorCompany, request.SponsorCompany.Password);
            await _userManager.AddToRoleAsync(sponsorCompany, RoleConstants.Sponsor);

            await _unitOfWork.CommitTransactionAsync();

            var sponsorCompanyDto = _mapper.Map<SponsorCompanyDto>(sponsorCompany);
            return await Task.FromResult(sponsorCompanyDto);
        }

        catch (Exception)
        {
            await _unitOfWork.RollbackTransactionAsync();
            throw;
        }
    }
}