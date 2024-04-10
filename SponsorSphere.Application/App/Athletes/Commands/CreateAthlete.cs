using AutoMapper;
using MediatR;
using SponsorSphere.Application.App.Athletes.Responses;
using SponsorSphere.Domain.Enums;
using SponsorSphere.Domain.Models;
using SponsorSphere.Infrastructure.Interfaces;

namespace SponsorSphere.Application.App.Athletes.Commands;

public record CreateAthlete(
    string Name,
    string LastName,
    string Email,
    string Password,
    string Country,
    string PhoneNumber,
    string BirthDate,
    SportsEnum Sport
    ) : IRequest<AthleteDto>;

public class CreateAthleteHandler : IRequestHandler<CreateAthlete, AthleteDto>
{
    private readonly IAthleteRepository _userRepository;
    private readonly IMapper _mapper;
    public CreateAthleteHandler(IAthleteRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
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
            request.PhoneNumber,
            request.BirthDate,
            request.Sport.ToString()
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
            request.PhoneNumber,
            request.BirthDate,
            request.Sport)
        {
            Id = GetNextId()
        };

        var newAthlete = _userRepository.Create(athlete);
        var athleteDto = _mapper.Map<AthleteDto>(athlete);

        return Task.FromResult(athleteDto);
    }

    private int GetNextId()
    {
        return _userRepository.GetLastId();
    }
}