using MediatR;
using SponsorSphere.Application.App.Athletes.Dtos;

namespace SponsorSphere.Application.App.Athletes.Queries;

public record GetAthletesByUrgentNeedQuery : IRequest<List<AthleteDto>>
{
}