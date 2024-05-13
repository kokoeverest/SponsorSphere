using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using SponsorSphere.Application.App.Achievements.Dtos;
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
        _logger.LogInformation("Action: {Action}", request.ToString());

        var eventDate = DateTime.Parse(request.Model.EventDate).ToUniversalTime();

        if (DateTime.UtcNow < eventDate)
        {
            throw new BadRequestException("You can't create an achievement in the future");
        }

        var sportEvent = _mapper.Map<SportEvent>(request.Model);
        sportEvent.Finished = true;

        var achievement = new Achievement
        {
            Sport = request.Model.Sport,
            SportEventId = sportEvent.Id,
            AthleteId = request.AthleteId,
            PlaceFinished = request.Model.PlaceFinished
        };

        try
        {
            await _unitOfWork.BeginTransactionAsync();

            sportEvent = await _unitOfWork.SportEventsRepository.CreateAsync(sportEvent);

            achievement.SportEventId = sportEvent.Id;
            var newAchievement = await _unitOfWork.AchievementsRepository.CreateAsync(achievement);

            await _unitOfWork.CommitTransactionAsync();
            var mappedAchievement = _mapper.Map<AchievementDto>(newAchievement);

            _logger.LogInformation("Action: {Action}, ({DT})ms", request.ToString(), (DateTime.Now - start).TotalMilliseconds);
            return await Task.FromResult(mappedAchievement);
        }

        catch (Exception)
        {
            await _unitOfWork.RollbackTransactionAsync();
            _logger.LogError("Action: {Action} failed", request.ToString());
            throw;
        }
    }
}
