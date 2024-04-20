using MediatR;
using SponsorSphere.Application.App.Athletes.Responses;

namespace SponsorSphere.Application.App.Athletes.Commands;

public record UpdateAthleteCommand : IRequest<AthleteDto>
{
}