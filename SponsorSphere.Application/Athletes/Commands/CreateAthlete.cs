using MediatR;
using SponsorSphere.Application.Athletes.Responses;
using SponsorSphere.Domain.Enums;
using SponsorSphere.Domain.Models;
using SponsorSphere.Infrastructure.Interfaces;

namespace SponsorSphere.Application;

public record CreateAthlete(
    string Name,
    string LastName,
    string Email,
    string Password,
    string Country,
    string Phone,
    string BirthDate,
    SportsEnum Sport
    ) : IRequest<AthleteDto>;

public class CreateAthleteHandler : IRequestHandler<CreateAthlete, AthleteDto>
{
    private readonly IAthleteRepository _userRepository;

    public CreateAthleteHandler(IAthleteRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public Task<AthleteDto> Handle(CreateAthlete request, CancellationToken cancellationToken)
    {
        IEnumerable<string> strings =
        [
            request.Name,
            request.LastName,
            request.Email,
            request.Password,
            request.Country,
            request.Phone,
            request.BirthDate,
            (string)request.Sport.ToString()
        ];

        if (strings.Any(str => string.IsNullOrEmpty(str)))
        {
            throw new ApplicationException("Cannot create athlete without required fields! Check your input!");
        }
        // Phone, Email and Password validations

        Athlete athlete = new(
            request.Name,
            request.LastName,
            request.Email,
            request.Password,
            request.Country,
            request.Phone,
            request.BirthDate,
            request.Sport)
        {
            Id = GetNextId()
        };

        var newAthlete = _userRepository.Create( athlete );

        return Task.FromResult(AthleteDto.FromAthlete( athlete ) );
    }

    private int GetNextId()
    {
        return _userRepository.GetLastId();
    }
}