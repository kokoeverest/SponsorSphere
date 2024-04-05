using MediatR;
using SponsorSphere.Application.Athletes.Responses;

namespace SponsorSphere.Application;

public record GetAthletesByAchievementss : IRequest<List<AthleteDto>>
{
}