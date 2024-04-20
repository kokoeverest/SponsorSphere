using MediatR;
using SponsorSphere.Application.App.Athletes.Responses;

namespace SponsorSphere.Application.App.Athletes.Queries;

public record GetAthletesByUrgentNeedQuery : IRequest<List<AthleteDto>>
{
}