using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using SponsorSphere.Application.App.Achievements.Dtos;
using SponsorSphere.Application.Common.Constants;
using SponsorSphere.Application.Common.Exceptions;
using SponsorSphere.Application.Interfaces;
using SponsorSphere.Domain.Models;

namespace SponsorSphere.Application.App.Achievements.Commands;

public record CreateAchievementCommand(CreateAchievementDto Model, int AthleteId) : IRequest<AchievementDto>;

public class CreateAchievementCommandHandler : IRequestHandler<CreateAchievementCommand, AchievementDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ILogger<CreateAchievementCommandHandler> _logger;

    public CreateAchievementCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<CreateAchievementCommandHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<AchievementDto> Handle(CreateAchievementCommand request, CancellationToken cancellationToken)
    {
        var start = DateTime.Now;
        _logger.LogInformation(LoggingConstants.logStartString, request.ToString());

        var sportEvent = await _unitOfWork.SportEventsRepository.GetByIdAsync(request.Model.SportEventId);

        if (DateTime.UtcNow < sportEvent.EventDate)
        {
            throw new BadRequestException("You can't create an achievement in the future");
        }

        sportEvent.Finished = true;

        var achievement = new Achievement
        {
            Sport = sportEvent.Sport,
            SportEventId = sportEvent.Id,
            AthleteId = request.AthleteId,
            PlaceFinished = request.Model.PlaceFinished,
            Description = request.Model.Description
        };

        try
        {
            await _unitOfWork.BeginTransactionAsync();

            await _unitOfWork.AchievementsRepository.CreateAsync(achievement);

            await _unitOfWork.CommitTransactionAsync();
            var mappedAchievement = _mapper.Map<AchievementDto>(achievement);

            _logger.LogInformation(LoggingConstants.logEndString, request.ToString(), (DateTime.Now - start).TotalMilliseconds);
            return await Task.FromResult(mappedAchievement);
        }

        catch (Exception)
        {
            await _unitOfWork.RollbackTransactionAsync();
            throw;
        }
    }
}
