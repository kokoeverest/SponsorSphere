using MediatR;
using SponsorSphere.Application.Athletes.Responses;

namespace SponsorSphere.Application;

public record DeleteAthlete : IRequest<AthleteDto>
{
}