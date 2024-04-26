using AutoMapper;
using MediatR;
using SponsorSphere.Application.App.SponsorCompanies.Responses;
using SponsorSphere.Application.Interfaces;
using SponsorSphere.Domain.Enums;
using SponsorSphere.Domain.Models;

namespace SponsorSphere.Application.App.SponsorCompanies.Commands;

public record CreateSponsorCompanyCommand(
    string Name,
    string Email,
    string Password,
    CountryEnum Country,
    string PhoneNumber,
    string Iban
    ) : IRequest<SponsorCompanyDto>;

public class CreateSponsorCompanyCommandHandler : IRequestHandler<CreateSponsorCompanyCommand, SponsorCompanyDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public CreateSponsorCompanyCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<SponsorCompanyDto> Handle(CreateSponsorCompanyCommand request, CancellationToken cancellationToken)
    {
        IEnumerable<string> strings =
        [
            request.Name,
            request.Email,
            request.Password,
            request.Country.ToString(),
            request.PhoneNumber,
            request.Iban
        ];

        if (strings.Any(string.IsNullOrEmpty))
        {
            throw new InvalidDataException("Cannot create profile without required fields! Check your input!");
        }

        try
        {
            await _unitOfWork.BeginTransactionAsync();
            // Phone, Email and Password validations

            var sponsorCompany = new SponsorCompany
            {
                Name = request.Name,
                Email = request.Email,
                Password = request.Password,
                Country = request.Country,
                PhoneNumber = request.PhoneNumber,
                IBAN = request.Iban
            };

            var newSponsorCompany = await _unitOfWork.SponsorCompaniesRepository.CreateAsync(sponsorCompany);
            await _unitOfWork.CommitTransactionAsync();

            var sponsorCompanyDto = _mapper.Map<SponsorCompanyDto>(newSponsorCompany);
            return await Task.FromResult(sponsorCompanyDto);
        }

        catch (InvalidDataException e)
        {
            await Console.Out.WriteLineAsync(e.Message);
            throw;
        }

        catch (Exception ex)
        {
            await Console.Out.WriteLineAsync(ex.Message);
            await _unitOfWork.RollbackTransactionAsync();
            throw;
        }
    }
}