using MediatR;
using SponsorSphere.Application.Athletes.Responses;

namespace SponsorSphere.Application;

public record GetAthletesByAge : IRequest<List<AthleteDto>>
{
}