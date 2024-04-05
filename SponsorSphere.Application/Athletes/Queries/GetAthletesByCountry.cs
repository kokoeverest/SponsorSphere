using MediatR;
using SponsorSphere.Application.Athletes.Responses;

namespace SponsorSphere.Application;

public record GetAthletesByCountry : IRequest<List<AthleteDto>>
{
}