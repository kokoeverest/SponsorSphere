using MediatR;
using SponsorSphere.Application.Athletes.Responses;

namespace SponsorSphere.Application;

public record GetAthletesByUrgentNeed : IRequest<List<AthleteDto>>
{
}