using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using SponsorSphere.Application.App.Athletes.Dtos;
using SponsorSphere.Application.Common.Constants;
using SponsorSphere.Application.Common.Helpers;
using SponsorSphere.Application.Interfaces;
using SponsorSphere.Domain.Models;

namespace SponsorSphere.Application.App.Athletes.Commands;

public record UpdateAthleteCommand(UpdateAthleteDto AthleteToUpdate) : IRequest<AthleteDto>;

public class UpdateAthleteCommandHandler : IRequestHandler<UpdateAthleteCommand, AthleteDto>
{
    private readonly UserManager<User> _userManager;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ILogger<UpdateAthleteCommandHandler> _logger;

    public UpdateAthleteCommandHandler(ILogger<UpdateAthleteCommandHandler> logger, UserManager<User> userManager, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _userManager = userManager;
        _logger = logger;
    }

    public async Task<AthleteDto> Handle(UpdateAthleteCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var start = DateTime.Now;
            _logger.LogInformation(LoggingConstants.logStartString, request.ToString());

            await _unitOfWork.BeginTransactionAsync();

            var transformedPicture = request.AthleteToUpdate.PictureId is not null
                ? await PictureHelper.TransformFileToPicture(request.AthleteToUpdate.PictureId, cancellationToken)
                : null;

            if (transformedPicture != null)
            {
                transformedPicture = await _unitOfWork.PicturesRepository.CreateAsync(transformedPicture);
            }

            var updatedAthlete = _mapper.Map<AthleteDto>(request.AthleteToUpdate);
            updatedAthlete.PictureId = transformedPicture?.Id ?? 0;

            var result = await _unitOfWork.AthletesRepository.UpdateAsync(updatedAthlete);
            await _unitOfWork.CommitTransactionAsync();

            _logger.LogInformation(LoggingConstants.logEndString, request.ToString(), (DateTime.Now - start).TotalMilliseconds);
            return result;
        }

        catch (Exception)
        {
            await _unitOfWork.RollbackTransactionAsync();
            throw;
        }
    }
}
