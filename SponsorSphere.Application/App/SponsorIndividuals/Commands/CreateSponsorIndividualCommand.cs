using AutoMapper;
using MediatR;
using SponsorSphere.Application.App.SponsorIndividuals.Responses;
using SponsorSphere.Application.Interfaces;
using SponsorSphere.Domain.Enums;
using SponsorSphere.Domain.Models;

namespace SponsorSphere.Application.App.SponsorIndividuals.Commands;
public record CreateSponsorIndividualCommand(
    string Name,
    string Email,
    string Password,
    CountryEnum Country,
    string PhoneNumber,
    string LastName,
    DateTime BirthDate
    ) : IRequest<SponsorIndividualDto>;

public class CreateSponsorIndividualCommandHandler : IRequestHandler<CreateSponsorIndividualCommand, SponsorIndividualDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public CreateSponsorIndividualCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<SponsorIndividualDto> Handle(CreateSponsorIndividualCommand request, CancellationToken cancellationToken)
    {
        IEnumerable<string> strings =
        [
            request.Name,
            request.Email,
            request.Password,
            request.Country.ToString(),
            request.PhoneNumber,
            request.LastName,
            request.BirthDate.ToString()
        ];

        if (strings.Any(string.IsNullOrEmpty))
        {
            throw new InvalidDataException("Cannot create profile without required fields! Check your input!");
        }

        try
        {
            await _unitOfWork.BeginTransactionAsync();
            // Phone, Email and Password validations

            var sponsorIndividual = new SponsorIndividual
            {
                Name = request.Name,
                Email = request.Email,
                //Password = request.Password,
                Country = request.Country,
                PhoneNumber = request.PhoneNumber,
                LastName = request.LastName,
                BirthDate = request.BirthDate,
            };

            var newSponsorIndividual = await _unitOfWork.SponsorIndividualsRepository.CreateAsync(sponsorIndividual);
            await _unitOfWork.CommitTransactionAsync();

            var sponsorIndividualDto = _mapper.Map<SponsorIndividualDto>(newSponsorIndividual);
            return await Task.FromResult(sponsorIndividualDto);
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