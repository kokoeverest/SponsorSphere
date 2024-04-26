using AutoMapper;
using MediatR;
using SponsorSphere.Application.App.Athletes.Responses;
using SponsorSphere.Application.Interfaces;
using SponsorSphere.Domain.Enums;
using SponsorSphere.Domain.Models;

namespace SponsorSphere.Application.App.Athletes.Commands;

public record CreateAthleteCommand(
    string Name,
    string LastName,
    string Email,
    string Password,
    CountryEnum Country,
    string PhoneNumber,
    DateTime BirthDate,
    SportsEnum Sport
    ) : IRequest<AthleteDto>;

public class CreateAthleteCommandHandler : IRequestHandler<CreateAthleteCommand, AthleteDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public CreateAthleteCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<AthleteDto> Handle(CreateAthleteCommand request, CancellationToken cancellationToken)
    {
        IEnumerable<string> strings =
        [
            request.Name,
            request.LastName,
            request.Email,
            request.Password,
            request.Country.ToString(),
            request.PhoneNumber,
            request.BirthDate.ToString(),
            request.Sport.ToString()
        ];

        if (strings.Any(string.IsNullOrEmpty))
        {
            throw new InvalidDataException("Cannot create profile without required fields! Check your input!");
        }

        try
        {
            await _unitOfWork.BeginTransactionAsync();
            // Phone, Email and Password validations

            var athlete = new Athlete
            {
                Name = request.Name,
                LastName = request.LastName,
                Email = request.Email,
                Password = request.Password,
                Country = request.Country,
                PhoneNumber = request.PhoneNumber,
                BirthDate = request.BirthDate,
                Sport = request.Sport
            };

            var newAthlete = await _unitOfWork.AthletesRepository.CreateAsync(athlete);
            await _unitOfWork.CommitTransactionAsync();

            var athleteDto = _mapper.Map<AthleteDto>(newAthlete);
            return await Task.FromResult(athleteDto);
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