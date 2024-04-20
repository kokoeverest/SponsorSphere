using AutoMapper;
using MediatR;
using SponsorSphere.Application.App.Athletes.Responses;
using SponsorSphere.Domain.Enums;
using SponsorSphere.Domain.Models;
using SponsorSphere.Infrastructure.Interfaces;

namespace SponsorSphere.Application.App.Athletes.Commands;

public record CreateAthleteCommand(
    string Name,
    string LastName,
    string Email,
    string Password,
    string Country,
    string PhoneNumber,
    DateTime BirthDate,
    SportsEnum Sport
    ) : IRequest<AthleteDto>;

public class CreateAthleteHandler : IRequestHandler<CreateAthleteCommand, AthleteDto>
{
    private readonly IAthleteRepository _athleteRepository;
    private readonly IMapper _mapper;
    public CreateAthleteHandler(IAthleteRepository athleteRepository, IMapper mapper)
    {
        _athleteRepository = athleteRepository;
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
            request.Country,
            request.PhoneNumber,
            request.BirthDate.ToString(),
            request.Sport.ToString()
        ];

        if (strings.Any(string.IsNullOrEmpty))
        {
            throw new ApplicationException("Cannot create athlete without required fields! Check your input!");
        }
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
        var newAthlete = await _athleteRepository.AddAsync(athlete);
        var athleteDto = _mapper.Map<AthleteDto>(newAthlete);

        return await Task.FromResult(athleteDto);
    }
}