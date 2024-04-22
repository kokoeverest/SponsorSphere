using AutoMapper;
using MediatR;
using SponsorSphere.Application.App.Achievements.Responses;
using SponsorSphere.Application.Interfaces;
using SponsorSphere.Domain.Enums;
using SponsorSphere.Infrastructure.Interfaces;

namespace SponsorSphere.Application.App.Athletes.Commands;

public record AddAchievementCommand(
    string Name,
    string LastName,
    string Email,
    string Password,
    string Country,
    string PhoneNumber,
    DateTime BirthDate,
    SportsEnum Sport
    ) : IRequest<AchievementDto>;

public class AddAchievementCommandHandler : IRequestHandler<AddAchievementCommand, AchievementDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;


    public AddAchievementCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<AchievementDto> Handle(AddAchievementCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}

